namespace ClassOne
{
    internal class Program
    {
          
            static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter a number or a word to convert:");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int number))
                {
                    string words = NumberToWords(number);
                    Console.WriteLine($"Words: {words}");
                }
                else
                {
                    int? numberFromWords = WordsToNumber(input);
                    if (numberFromWords.HasValue)
                    {
                        Console.WriteLine($"Number: {numberFromWords}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number or word.");
                    }
                }

                Console.WriteLine("Do you want to convert another number or word? (yes/no)");
                string choice = Console.ReadLine();
                if (choice.ToLower() != "yes")
                    break;
            }
        }

        static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                string[] unitsArray = {
                "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine",
                "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen"
            };
                string[] tensArray = {
                "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"
            };

                if (number < 20)
                    words += unitsArray[number];
                else
                {
                    words += tensArray[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsArray[number % 10];
                }
            }

            return words;
        }

        static int? WordsToNumber(string words)
        {
            string[] unitsArray = {
            "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine",
            "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen"
        };
            string[] tensArray = {
            "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"
        };

            string[] wordsArray = words.Split(new char[] { ' ', '-' }, StringSplitOptions.RemoveEmptyEntries);

            int result = 0;
            int tempResult = 0;
            foreach (string word in wordsArray)
            {
                int index = Array.IndexOf(unitsArray, word);
                if (index != -1)
                {
                    tempResult += index;
                }
                else
                {
                    index = Array.IndexOf(tensArray, word);
                    if (index != -1)
                    {
                        tempResult += index * 10;
                    }
                    else if (word == "hundred")
                    {
                        tempResult *= 100;
                    }
                    else if (word == "thousand")
                    {
                        result += tempResult * 1000;
                        tempResult = 0;
                    }
                    else if (word == "million")
                    {
                        result += tempResult * 1000000;
                        tempResult = 0;
                    }
                    else if (word == "minus")
                    {
                        result *= -1;
                    }
                    else
                    {
                        return null; // Invalid word
                    }
                }
            }
            return result + tempResult;
        }
    }

}
  
