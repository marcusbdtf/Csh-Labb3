using ClassLibraryL3;
using System.Linq;
using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] inputlist)
    {
        List<object> userInput = FetchInput(inputlist);
        var inputOption = (userInputOption)userInput[0];

        switch (inputOption)
        {
            case userInputOption.Null:
                PrintControls();
                break;
            case userInputOption.Lists:
                OptionLists();
                break;
            case userInputOption.New:
            case userInputOption.Add:
                if (userInput[1].ToString() == "-new")
                    if (!OptionNew(userInput))
                        return;
                OptionAdd(userInput);
                break;
            case userInputOption.Remove:
                OptionRemove(userInput);
                break;
            case userInputOption.Words:
                OptionWords(userInput);
                break;
            case userInputOption.Count:
                OptionCount(userInput);
                break;
            case userInputOption.Practice:
                OptionPractice(userInput);
                break;
        }
        Console.WriteLine();
    }
    private static void OptionLists()
    {
        string[] array = Wordlist.GetLists();
        foreach (string el in array)
        {
            Console.WriteLine(el);
        }
        Console.WriteLine();
    }
    private static bool OptionNew(List<object> userInput)
    {
        Wordlist wordlist = Wordlist.LoadList(userInput[2].ToString());

        if (wordlist != null)
        {
            Console.WriteLine("\n List with name already exists.\n");
            return false;
        }

        var language = new string[userInput.Count() - 3];

        for (int i = 3; i < userInput.Count(); i++)
        {
            language[i - 3] = userInput[i].ToString();
        }
        wordlist = new Wordlist(userInput[2].ToString(), language);
        wordlist.Save();
        return true;
    }

    private static void OptionAdd(List<object> userInput)
    {
        var listName = userInput[1];
        // Find the list with the given name
        Wordlist wordlist = Wordlist.LoadList(listName.ToString());
        if (wordlist == null)
        {
            Console.WriteLine($"Error: list '{listName}' not found.");
            return;
        }

        // Keep prompting for new words until the user cancels
        while (true)
        {
            Console.WriteLine("Enter a new word (or leave blank to cancel):");
            var firstWord = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(firstWord))
            {
                // User cancelled exit loop
                break;
            }

            // Prompt for translations in each language in the list
            var translations = new string[wordlist.Languages.Length];
            for (int i = 0; i < translations.Length; i++)
            {
                Console.WriteLine($"Enter translation for '{firstWord}' in {wordlist.Languages[i]}: ");
                translations[i] = Console.ReadLine();
            }

            // Add the new word to the list
            wordlist.Add(translations);
        }
    }
    public static bool OptionRemove(List<object> userInput)
    {
        // Find the list with the given name
        var listName = userInput[1];
        var language = userInput[2];
        string[] words = userInput.Skip(2).Cast<string>().ToArray();
        Wordlist wordlist = Wordlist.LoadList(listName.ToString());

        if (wordlist == null)
            return false; // return false if no wordlist with the given name exists

        int translationIndex = Array.IndexOf(wordlist.Languages, language); // get the index of the specified language in the wordlist's language array
        if (translationIndex < 0)
            return false; // return false if the language is not found in the wordlist

        bool success = true;
        foreach (string word in words)
        {
            bool removed = wordlist.Remove(translationIndex, word); // remove the word from the wordlist
            if (!removed)
                success = false; // if one of the words failed to be removed, set success to false
        }
        return success;
    }

    private static void OptionWords(List<object> userInput)
    {
        // Load the word list
        var words = Wordlist.LoadList(userInput[1].ToString());
        var sortByLanguage = userInput[2].ToString();

        if (!string.IsNullOrEmpty(sortByLanguage))
        {
            // sort words by the specified language
            words.List(Array.IndexOf(words.Languages, sortByLanguage), translations =>
            {
                // print out the translations for each word
                Console.WriteLine(string.Join(", ", translations));
            });
        }
        else
        {
            // sort words by the first language in the list
            words.List(0, translations =>
            {
                // print out the translations for each word
                Console.WriteLine(string.Join(", ", translations));
            });
        }
    }
    private static void OptionCount(List<object> userInput)
    {
        Wordlist wordList = Wordlist.LoadList(userInput[2].ToString());
        if (wordList == null)
        {
            Console.WriteLine("\nThe list don't exist.\n");
            return;
        }
        Console.WriteLine(
            $"\nThere are {wordList.Count()} words in list '{wordList.Name}'\n");
    }
    public static void OptionPractice(List<object> userInput)
    {
        string[] args = userInput.Cast<string>().ToArray();
        if (args.Length < 2 || args[0] != "-practice")
        {
            Console.WriteLine("Usage: Wordtrainer -practice <listname>");
            return;
        }

        string listname = args[1];
        Wordlist wordlist = Wordlist.LoadList(listname);
        if (wordlist == null)
        {
            Console.WriteLine($"Error: Could not load list '{listname}'");
            return;
        }

        Console.WriteLine($"Practicing words from list '{wordlist.Name}'");
        int total = 0;
        int correct = 0;

        while (true)
        {
            Word word = wordlist.GetWordToPractice();
            if (word == null)
            {
                Console.WriteLine("Error: The list is empty");
                return;
            }

            Console.WriteLine($"Translate '{word.Translations[word.FromLanguage]}' from {wordlist.Languages[word.FromLanguage]} to {wordlist.Languages[word.ToLanguage]}:");
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                break;
            }

            if (input.ToLower() == word.Translations[word.ToLanguage].ToLower())
            {
                Console.WriteLine("Correct!");
                correct++;
            }
            else
            {
                Console.WriteLine($"Incorrect. The correct translation is '{word.Translations[word.ToLanguage]}'");
            }
            total++;
        }

        Console.WriteLine($"You practiced {total} words, and got {correct} ({100.0 * correct / total:0.0}%) correct");
    }

    private enum userInputOption
    {
        Null = 0, Lists = 1, New = 2, Add = 3, Remove = 4, Words = 5, Count = 6, Practice = 7
    }
    private static bool ValidateUserInput(string[] inputs)
    {
        string[] commands = { "-lists", "-new", "-add", "-remove", "-words", "-count", "-practice" };
        string[] allowedCharacters = { "a-zA-Z0-9_", "-" };

        foreach (string input in inputs)
        {
            if (!commands.Contains(input))
            {
                Console.WriteLine("Error: Invalid command");
                Console.WriteLine($"Valid commands are: {PrintControls}");
                return false;
            }

            foreach (char c in input)
            {
                if (!allowedCharacters.Contains(c.ToString()))
                {
                    Console.WriteLine("Error: Invalid character in command");
                    return false;
                }
            }
        }
        return true;
    }
    private static void PrintControls()
    {
        Console.WriteLine("-lists");
        Console.WriteLine("-new <list name> <language 1> <language 2> .. <language n>");
        Console.WriteLine("-add <list name>");
        Console.WriteLine("-remove <list name> <language> <word 1> <word 2> .. <word n>");
        Console.WriteLine("-words <list name> <sortByLanguage>");
        Console.WriteLine("-count <list name>");
        Console.WriteLine("-practice <list name>\n");
    }
    private static List<object> FetchInput(string[] inputlist)
    {
        //if statement that checks userinput, if none: return a new list
        if (inputlist.Length == 0 || string.Join("", inputlist).Count(c => c == '-') > 1)
        {
            return new List<object>() { userInputOption.Null };
        }
        var regexDict = new Dictionary<userInputOption, string>
        {
            {userInputOption.Lists, @"(?<=-lists\s+)\b[a-öA-Ö0-9]+\b"},
            {userInputOption.New, @"(?<=-new\s+)\b[a-öA-Ö0-9]+\b"},
            {userInputOption.Add, @"(?<=-add\s+)\b[a-öA-Ö0-9]+\b"},
            {userInputOption.Remove, @"(?<=-remove\s+)\b[a-öA-Ö0-9]+\s+\b[a-öA-Ö0-9]+\s+[\S]+\b"},
            {userInputOption.Words, @"(?<=-words\s+)\b[a-öA-Ö0-9]+\b"},
            {userInputOption.Count, @"(?<=-count\s+)\b[a-öA-Ö0-9]+\b"},
            {userInputOption.Practice, @"(?<=-practice\s+)\b[a-öA-Ö0-9]+\b"},
         };

        string joinedArgs = string.Join(" ", inputlist);
        var output = new List<object>();

        foreach (KeyValuePair<userInputOption, string> keyValuePair in regexDict)
        {
            var rgx = new Regex(keyValuePair.Value, RegexOptions.IgnoreCase);
            if (!rgx.IsMatch(joinedArgs)) continue;

            List<string> matches =
                rgx.Matches(joinedArgs).Cast<Match>().Select(m => m.Value.ToLower().Trim()).ToList();

            output.Add(keyValuePair.Key);
            output.AddRange(matches);
            return output;
        }
        return new List<object> { userInputOption.Null };
    }
}