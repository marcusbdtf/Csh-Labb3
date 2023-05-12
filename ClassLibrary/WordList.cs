using System.IO.Enumeration;
using System.Xml.Linq;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using System;
using System.Security.Cryptography.X509Certificates;

namespace ClassLibraryL3
{
    public class WordList
    {
        private List<Word> _words;
        public string Name { get; }
        public string[] Languages { get; }

        private static readonly string _dataPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "GlossaryApp");

        public WordList(string name, params string[] languages)
        {
            Name = name.ToLower();
            Languages = languages.Select(l => l.ToLower()).ToArray();
            _words = new List<Word>();
        }

        public static string[] GetLists()
        {
            Directory.CreateDirectory(_dataPath); 
            return Directory.GetFiles(_dataPath, "*.dat").Select(Path.GetFileNameWithoutExtension).ToArray();
        }

        public static WordList LoadList(string name)
        {
            var path = Path.Combine(_dataPath, $"{name.ToLower()}.dat");
            if (!File.Exists(path))
                return null;

            try
            {
                using (var reader = new StreamReader(path))
                {
                    var languages = reader.ReadLine().Split(';');

                    languages = languages.Where(language => !string.IsNullOrWhiteSpace(language)).ToArray();

                    var wordList = new WordList(name, languages);
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var translations = line.Split(';');

                        translations = translations.Where(translation => !string.IsNullOrWhiteSpace(translation)).ToArray();

                        wordList._words.Add(new Word(translations));
                    }

                    return wordList;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load list: {ex.Message}");
                return null;
            }
        }

        public void Save()
        {
            Directory.CreateDirectory(_dataPath);
            var path = Path.Combine(_dataPath, $"{Name}.dat");
            try
            {
                using (var writer = new StreamWriter(path))
                {
                    writer.WriteLine(string.Join(';', Languages) + ';');
                    foreach (var word in _words)
                    {
                        writer.WriteLine(string.Join(';', word.Translations) + ';');
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save list: {ex.Message}");
            }
        }

        public void Add(params string[] translations)
        {
            if (translations.Length != Languages.Length)
                throw new ArgumentException("Incorrect number of translations.");

            translations = translations.Select(t => t.ToLower()).ToArray();
            if (_words.Any(w => w.Translations.SequenceEqual(translations)))
                throw new ArgumentException("Word already exists in the list.");

            _words.Add(new Word(translations));
        }
        public bool WordExists(string word, int languageIndex)
        {
            return _words.Exists(w => w.Translations[languageIndex].Equals(word, StringComparison.InvariantCultureIgnoreCase));
        }
        public bool Remove(int translation, string word)
        {
            var wordToRemove = _words.FirstOrDefault(w =>
                w != null && w.Translations[translation].Equals(word.ToLower(), StringComparison.OrdinalIgnoreCase));
            if (wordToRemove != null)
            {
                _words.Remove(wordToRemove);
                return true;
            }

            return false;
        }

        public int Count() => _words.Count;

        public void List(int sortByTranslation, Action<string[]> showTranslations)
        {
            foreach (var word in _words.OrderBy(w => w.Translations[sortByTranslation]))
            {
                showTranslations(word.Translations);
            }
        }
        public void Clear()
        {
            _words.Clear();
        }

        public Word GetWordToPractice()
        {
            var random = new Random();
            var wordToPractice = _words[random.Next(_words.Count)];

            int fromLanguageIndex;
            int toLanguageIndex;
            do
            {
                fromLanguageIndex = random.Next(Languages.Length);
                toLanguageIndex = random.Next(Languages.Length);
            } while (fromLanguageIndex == toLanguageIndex);

            return new Word(fromLanguageIndex, toLanguageIndex, wordToPractice.Translations);
        }
    }
}
