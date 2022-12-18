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
        Wordlist wordlist = Wordlist.LoadList(userInput[1].ToString());

        if (wordlist != null)
        {
            Console.WriteLine("\n List with name already exists.\n");
            return false;
        }

        string[] language = new string[userInput.Count() - 3];

        for (int i = 0; i < language.Length; i++)
        {
            language[i] = userInput[2 + i].ToString();
        }
        wordlist = new Wordlist(userInput[2].ToString(), language);
        wordlist.Save();
        return true;
    }
    private static void OptionAdd(List<object> userInput)
    {
        Wordlist wordList = Wordlist.LoadList(userInput[1].ToString());
        if (wordList == null)
        {
            var lang = new string[userInput.Count() - 2];

            for (int j = 2; j < userInput.Count(); j++)
            {
                lang[j - 2] = userInput[j].ToString();
            }

            wordList = new Wordlist(userInput[1].ToString(), lang);
            wordList.Save();
            Console.WriteLine($"\nA new list named '{wordList.Name}' was created\n");
        }

        var userStrings = new List<string>();
        
        // Read user input until a blank line is entered
        while (true)
        {
            // Read the first word
            Console.WriteLine($"\nEnter word for language {wordList.Languages[0]}: ");
            string input = Console.ReadLine();

            // If the line is blank, break the loop
            if (string.IsNullOrEmpty(input))
            {
                break;
            }

            // If there are only two languages in the list, read the second word
            if (wordList.Languages.Length == 2)
            {
                Console.WriteLine($"\nEnter word for language {wordList.Languages[1]}: ");
                string input2 = Console.ReadLine();

            if (string.IsNullOrEmpty(input2))
                    {
                        break;
                    }
                userStrings.Add(input);
                userStrings.Add(input2 + ";");
                for (int i = 2; i < wordList.Languages.Length; i++)
                {
                    Console.WriteLine($"\nEnter word for language {wordList.Languages[i]}: ");
                    string inputN = Console.ReadLine();
                    if (string.IsNullOrEmpty(inputN))
                    {
                        break;
                    }
                    userStrings.Add(inputN + ";");
                }
            }

            if (userStrings.Count == wordList.Languages.Length)
            {
                wordList.Add(userStrings.ToArray());
                wordList.Save();
                userStrings.Clear();
            }
        }
        if (userStrings.Count > 0)
        {
            wordList.Add(userStrings.ToArray());
            wordList.Save();
        }
    }
    public static bool OptionRemove(List<object> userInput)
    {
        Wordlist wordList = Wordlist.LoadList(userInput[1].ToString());
        if (wordList != null)
        {
            int languageIndex = Array.IndexOf(wordList.Languages, userInput[2].ToString());

            if (languageIndex == -1)
            {
                Console.WriteLine($"\nThe language '{userInput[2]}' could not be found in the list '{wordList.Name}'.\n");
                return false;
            }
            else
            {
                bool removedAny = false;
                for (int j = 3; j < userInput.Count(); j++)
                {
                    // Try to remove the word from the list
                    bool removed = wordList.Remove(languageIndex, userInput[j].ToString());
                    if (removed)
                    {
                        Console.WriteLine($"\nThe word '{userInput[j]}' was removed from the language '{userInput[2]}' in the list '{wordList.Name}'.\n");
                        removedAny = true;
                    }
                }
                if (!removedAny)
                {
                    Console.WriteLine($"\nNone of the specified words were found in the language '{userInput[2]}' in the list '{wordList.Name}'.\n");
                }
                wordList.Save();
            }
        }
        else
        {
            Console.WriteLine("\nError: File not found.\n");
            return false;
        }

        return true;
    }
    private static void OptionWords(List<object> userInput)
    {
        Wordlist wordList = Wordlist.LoadList(userInput[1].ToString());
        if (wordList == null)
        {
            Console.WriteLine($"\nThere is no list with name: '{userInput[1]}'.\n");
            return;
        }

        // Get the index of the language to sort by
        if (userInput.Count > 2)
        {
            int sortByLanguageIndex;
            for (int i = 0; i < wordList.Languages.Length; i++)
            {
                // Check if the language matches the one specified in the userInput list
                if (wordList.Languages[i] != userInput[2].ToString()) continue;
                {
                    sortByLanguageIndex = i;
                    wordList.List(sortByLanguageIndex, WordPrinter);
                    wordList.Save();
                    break;
                }
        }
            
        }
        else
        {
            wordList.List(0, WordPrinter);
        }
    }
    private static void WordPrinter(string[] array)
    {
        string str = "";
        foreach (string s in array)
        {
            str += $"{s}\t";
        }
        Console.WriteLine(str);
    }
    private static void OptionCount(List<object> userInput)
    {
        Wordlist wordList = Wordlist.LoadList(userInput[1].ToString());
        if (wordList == null)
        {
            Console.WriteLine("\nThe list don't exist.\n");
            return;
        }
        Console.WriteLine(
            $"\nThere are {wordList.Count()*wordList.Languages.Length} words in list '{wordList.Name}'\n");
    }
    public static void OptionPractice(List<object> userInput)
    {
        // Get the listname from the user input
        string listname = userInput[1].ToString();

        Wordlist wordList = Wordlist.LoadList(listname);

        if (wordList == null)
        {
            Console.WriteLine($"\nThe list named '{listname}' doesn't exist.\n");
            return;
        }

        int practicedWords = 0;
        int correctAnswers = 0; 

        while (true)
        {
            Word wordToPractice = wordList.GetWordToPractice();

            if (wordToPractice == null)
            {
                break;
            }

            // Get the translation of the word in the "from" language
            string fromTranslation = wordToPractice.Translations[wordToPractice.FromLanguage];

            Console.WriteLine($"Translate '{fromTranslation}' to {wordList.Languages[wordToPractice.ToLanguage]}");

            string answer = Console.ReadLine();

            if (string.IsNullOrEmpty(answer))
            {
                break;
            }

            practicedWords++;

            if (answer == wordToPractice.Translations[wordToPractice.ToLanguage])
            {
                correctAnswers++;
                Console.WriteLine("Correct!");
            }
            else
            {
                Console.WriteLine("Incorrect.");
            }
        }

        // Calculate the percentage

        Console.WriteLine($"\nYou scored {correctAnswers} correct. Words practiced {practicedWords}. Percentage {((float)correctAnswers) /practicedWords*100:0.0}%.\n");
    }
    private enum userInputOption
    {
        Null = 0, Lists = 1, New = 2, Add = 3, Remove = 4, Words = 5, Count = 6, Practice = 7
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
        if (inputlist.Length == 0 || string.Join("", inputlist).Count(c => c == '-') == 0)
        {
            return new List<object>() { userInputOption.Null };
        }
        var regexDict = new Dictionary<userInputOption, string>
        {
            {userInputOption.Lists, @"-lists"},
            {userInputOption.New, @"(?<=-new\s+)\b[a-öA-Ö0-9]+\s+(?:[a-öA-Ö0-9]+\s*)+"},
            {userInputOption.Add, @"(?<=-add\s+)\b[a-öA-Ö0-9]+\s+[\S]+\b"},
            {userInputOption.Remove, @"(?<=-remove\s+)\b[a-öA-Ö0-9]+\s+\b[a-öA-Ö0-9]+\s+[\S]+(\s+[\S]+)*\b"},
            {userInputOption.Words, @"(?<=-words\s+)\b[a-öA-Ö0-9]+(\s+[\S]*)?\b"},
            {userInputOption.Count, @"(?<=-count\s+)\b[a-öA-Ö0-9]+\b"},
            {userInputOption.Practice, @"(?<=-practice\s+)(\b[a-öA-Ö0-9]+\b)"},
        };

        string joinedArgs = string.Join(" ", inputlist);
    
        var output = new List<object>();

        // Loop through regex in the dict
        foreach (KeyValuePair<userInputOption, string> keyValuePair in regexDict)
        {
            var rgx = new Regex(keyValuePair.Value, RegexOptions.IgnoreCase);
            
            if (!rgx.IsMatch(joinedArgs)) continue;

            // get matches from the input string
            List<string> matches =
                rgx.Matches(joinedArgs).Cast<Match>().Select(m => m.Value.ToLower().Trim()).ToList();

            output.Add(keyValuePair.Key);
            foreach (string match in matches)
            {
                output.AddRange(match.Split(' '));
            }
            return output; // 
        }
        return output;
    }
}