using System.IO.Enumeration;
using System.Xml.Linq;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using System;
using System.Security.Cryptography.X509Certificates;

namespace ClassLibraryL3
{
    public class Wordlist
    {
        private static List<Word> Words { get; set; } = new List<Word>();
        public string Name { get; } // name of language list

        public string[] Languages { get; }
        public Wordlist(string name, params string[] languages)//takes in the name of the wordlist, languages
        {
            Name = name;
            Languages = languages;
        }
        public static string[] GetLists()//gets all the wordlists in the wordlists folder
        {

            string dataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            string namePath = $"\\{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}";

            string fullPath = dataPath + namePath;
            // gets the path to the wordlists folder


            string[] listNamesWithoutExtension = Directory.GetFiles(fullPath).Select(n => Path.GetFileNameWithoutExtension(n)).ToArray();
            // gets all files in the path and removes the extension
            return listNamesWithoutExtension;
            // returns lists from local app folder and removes extension
        }
        public static Wordlist LoadList(string name)
        {
            // Get the path to the wordlists folder
            string dataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string namePath = $"\\{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}";
            string fullPath = dataPath + namePath;
            string fullPathWithExtension = fullPath + "\\" + name + ".dat";
            if (!File.Exists(fullPathWithExtension))
            {
                return null;
            }

            List<string> lines = File.ReadAllLines(fullPathWithExtension).ToList();
            if (lines.Count == 0)
            {
                return null;
            }

            string[] languages = null;
            if (lines.Count > 1)
            {
                languages = lines[0]?.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            }

            Words.Clear();
            for (int i = 1; i < lines.Count; i++)
            {
                Words.Add(new Word(lines[i]?.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)));
            }

            return new Wordlist(name, languages);
        }

        public void Save()
        {
            // Get the path to the wordlists folder
            string dataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string namePath = $"\\{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}";
            string fullPath = dataPath + namePath;
            string fullPathWithExtension = fullPath + "\\" + Name + ".dat";

            // Create the wordlists folder if it doesn't already exist
            Directory.CreateDirectory(fullPath);

            // streamwriter to write to the file
            using (var sr = new StreamWriter(fullPathWithExtension, false))
            {
                // Write langs to the first line of the file
                sr.WriteLine(string.Join(";", Languages));

                if (Words == null)
                {
                    return;
                }
                if (Words.Count == 0)
                {
                    return;
                }

                foreach (var word in Words)
                {
                    sr.WriteLine(string.Join(";", word.Translations));
                }
            }
        }

        public void Add(params string[] translations)
        {
            // takes only non null vals
            translations = translations.Where(p => p != null).Select(p => p.ToLower()).ToArray();
            Words.Add(new Word(translations));
        }
        
        public void Clear()
        {
            Words.Clear();
        }

        public bool Remove(int languageIndex, string word)
        {
            if (languageIndex < 0 || languageIndex >= Languages.Length)
            {
                return false;
            }

            for (int i = 0; i < Words.Count; i++)
            {
                if (Words[i].Translations[languageIndex] == word)
                {
                    Words.RemoveAt(i);
                    Save();
                    return true;
                }
            }
            return false;
        }
        public int Count()
        {
            return Words.Count();
        }
        public void List(int sortByTranslation, Action<string[]> showTranslations)
        {
            // iterate through the sorted list and invoke the showTranslations callback for each word
            Words = IndexSwap(sortByTranslation);

            foreach (Word w in Words)
            {
                showTranslations(w.Translations);
            }
        }
        public List<Word> IndexSwap(int i)
        {
            List<Word> sortedWords = new List<Word> { };
            foreach (var word in Words)
            {
                if (word != null && word.Translations.Length > i)
                {
                    // Create a new string array to store the swapped translations
                    string[] tempArray = new string[word.Translations.Length];

                    // Swap the values of the translations
                    for (int j = 0; j < word.Translations.Length; j++)
                    {
                        if (j == i)
                        {
                            tempArray[j] = word.Translations[0];
                        }
                        else if (j == 0)
                        {
                            tempArray[j] = word.Translations[i];
                        }
                        else
                        {
                            tempArray[j] = word.Translations[j];
                        }
                    }

                    // Add the swapped translations to the sortedWords list
                    sortedWords.Add(new Word(tempArray));
                }
            }
            return sortedWords;
        }

        public Word GetWordToPractice()
        {
            var rand = new Random();
            int randWord = rand.Next(0, Words.Count());
            int randFromLang = rand.Next(Languages.Length);
            int randToLang = rand.Next(Languages.Length);

            while (randFromLang == randToLang)
            {
                randToLang = rand.Next(Languages.Length);
            }

            if (Words.Count == 0) return null;

            var word = new Word(randFromLang, randToLang, Words[randWord].Translations);

            return word;
        }
    }
}
        