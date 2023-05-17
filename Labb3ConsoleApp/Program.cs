using ClassLibraryL3;
using System;
using System.IO;
using System.Linq;

namespace Labb3ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || args.Contains("-help") || args.Contains("/?"))
            {
                PrintUsage();
                return;
            }

            string command = args[0].ToLower();

            try
            {
                switch (command)
                {
                    case "-lists":
                        ListGlossaries();
                        break;
                    case "-new":
                        if (args.Length < 4)
                        {
                            Console.WriteLine("Error: At least one list name and two languages must be provided.");
                            return;
                        }

                        CreateNewGlossary(args[1], args.Skip(2).ToArray());
                        break;
                    case "-add":
                        if (args.Length != 2)
                        {
                            Console.WriteLine("Error: Exactly one list name must be provided.");
                            return;
                        }

                        AddWords(args[1]);
                        break;
                    case "-remove":
                        if (args.Length < 4)
                        {
                            Console.WriteLine(
                                "Error: At least one list name, one language, and one word must be provided.");
                            return;
                        }

                        RemoveWords(args[1], int.Parse(args[2]), args.Skip(3).ToArray());
                        break;
                    case "-words":
                        if (args.Length < 2 || args.Length > 3)
                        {
                            Console.WriteLine(
                                "Error: Exactly one list name and optionally one sort by language must be provided.");
                            return;
                        }

                        ListWords(args[1], args.Length == 3 ? int.Parse(args[2]) : 0);
                        break;
                    case "-count":
                        if (args.Length != 2)
                        {
                            Console.WriteLine("Error: Exactly one list name must be provided.");
                            return;
                        }

                        CountWords(args[1]);
                        break;
                    case "-practice":
                        if (args.Length != 2)
                        {
                            Console.WriteLine("Error: Exactly one list name must be provided.");
                            return;
                        }

                        Practice(args[1]);
                        break;
                    default:
                        Console.WriteLine("Unknown command. Use '-help' for more information.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void PrintUsage()
        {
            Console.WriteLine("Use any of the following parameters:");
            Console.WriteLine("-lists");
            Console.WriteLine("-new <list name> <language 1> <language 2> .. <langauge n>");
            Console.WriteLine("-add <list name>");
            Console.WriteLine("-remove <list name> <language> <word 1> <word 2> .. <word n>");
            Console.WriteLine("-words <listname> <sortByLanguage>");
            Console.WriteLine("-count <listname>");
            Console.WriteLine("-practice <listname>");
        }

        static void ListGlossaries()
        {
            var glossaries = WordList.GetLists();

            if (glossaries.Length == 0)
            {
                Console.WriteLine("No lists found, create a new one!");
            }
            else
            {
                foreach (var glossary in glossaries)
                {
                    Console.WriteLine(glossary);
                }
            }
        }

        static void CreateNewGlossary(string name, string[] languages)
        {
            var newGlossary = new WordList(name, languages);
            newGlossary.Save();
        }

        static void AddWords(string listName)
        {
            var glossary = WordList.LoadList(listName);
            Console.WriteLine("Enter words to add. Press enter without input to stop.");
            while (true)
            {
                var translations = new List<string>();
                for (int i = 0; i < glossary.Languages.Length; i++)
                {
                    Console.WriteLine($"Enter a word in {glossary.Languages[i]}:");
                    var word = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(word))
                    {
                        glossary.Save();
                        return;

                    }
                    translations.Add(word);
                }
                glossary.Add(translations.ToArray());
                glossary.Save();
            }
        }

        static void RemoveWords(string listName, int languageIndex, string[] words)
        {
            var glossary = WordList.LoadList(listName);
            if (glossary == null)
            {
                Console.WriteLine("Failed to load the glossary.");
                return;
            }

            foreach (var word in words)
            {
                bool removed = glossary.Remove(languageIndex, word);
                if (removed)
                {
                    Console.WriteLine($"Successfully removed {word} from the glossary.");
                }
                else
                {
                    Console.WriteLine($"Failed to remove {word}. The word does not exist in the selected language.");
                }
            }
            glossary.Save();
        }

        static void ListWords(string listName, int sortByLanguage)
        {
            var glossary = WordList.LoadList(listName);
            glossary.List(sortByLanguage, translations =>
            {
                //list translations paired with respective lang
                var translationsWithLanguages = new List<(string Language, string Translation)>();
                for (int i = 0; i < glossary.Languages.Length; i++)
                {
                    translationsWithLanguages.Add((glossary.Languages[i], translations[i]));
                }

                //sort list by lang
                var sortedTranslations = translationsWithLanguages.OrderBy(t => t.Language == glossary.Languages[sortByLanguage]).ToList();
                foreach (var (language, translation) in sortedTranslations)
                {
                    Console.WriteLine($"{language}: {translation}");
                }

                Console.WriteLine();
            });
        }

        static void CountWords(string listName)
        {
            var glossary = WordList.LoadList(listName);
            Console.WriteLine($"Total words in {listName}: {glossary.Count()}");
        }

        static void Practice(string listName)
        {
            var glossary = WordList.LoadList(listName);
            int correctCount = 0;
            int totalCount = 0;

            while (true)
            {
                var word = glossary.GetWordToPractice();

                Console.WriteLine($"Translate the {glossary.Languages[word.FromLanguage]} word '{word.Translations[word.FromLanguage]}' to {glossary.Languages[word.ToLanguage]}:");
                var translation = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(translation))
                {
                    break;
                }

                if (translation.ToLower() == word.Translations[word.ToLanguage].ToLower())
                {
                    Console.WriteLine("Correct!");
                    correctCount++;
                }
                else
                {
                    Console.WriteLine($"Incorrect. The correct translation is '{word.Translations[word.ToLanguage]}'");
                }

                totalCount++;
            }

            Console.WriteLine($"You practiced {totalCount} words, and got {correctCount} correct.");
        }
    }
}