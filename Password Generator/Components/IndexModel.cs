using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Password_Generator.Components
{
    public class IndexModel : PageModel
    {
        public string Password { get; set; }

        public void OnGet()
        {
        }

        public void OnPostGeneratePassword()
        {
            var csvFilePath = "Components/words.csv";
            var lines = System.IO.File.ReadAllLines(csvFilePath);
            var options = lines.Select(line => line.Split(','))
                               .Where(parts => parts.Length > 1) // Ensure the line has at least two parts
                               .ToList();

            // Implement your password generation logic here
            Password = GeneratePassword(options);
        }

        private string GeneratePassword(List<string[]> options)
        {
            var random = new Random();

            // Filter words by category
            var verbsAndAdjectives = options.Where(parts => parts[0] == "Verb" || parts[0] == "Adjective")
                                            .Select(parts => parts[1].Trim())
                                            .ToList();
            var nouns = options.Where(parts => parts[0] == "Noun")
                               .Select(parts => parts[1].Trim())
                               .ToList();

            // Ensure the lists are not empty
            if (!verbsAndAdjectives.Any() || !nouns.Any())
            {
                throw new InvalidOperationException("The CSV file must contain at least one verb/adjective and one noun.");
            }

            string word1, word2;
            int combinedLength;
            do
            {
                // Select one random verb or adjective and one random noun
                word1 = verbsAndAdjectives[random.Next(verbsAndAdjectives.Count)];
                word2 = nouns[random.Next(nouns.Count)];
                combinedLength = word1.Length + word2.Length;
            } while (combinedLength < 11 || combinedLength > 13);

            word1 = char.ToUpper(word1[0]) + word1.Substring(1);

            // Calculate the number of digits needed to fill the gap
            int numberOfDigits = 15 - combinedLength; // 15 because we will add the special character at the end

            // Generate the required number of random numbers
            var numbers = new StringBuilder();
            for (int i = 0; i < numberOfDigits; i++)
            {
                numbers.Append(random.Next(0, 10).ToString());
            }

            // Generate a random special character
            var specialChar = "!@#$%&*?".ToCharArray()[random.Next(0, 7)];

            // Combine the words, numbers, and special character
            var password = new StringBuilder();
            password.Append(word1);
            password.Append(word2);
            password.Append(numbers);
            password.Append(specialChar);

            return password.ToString();
        }


    }
}
