
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace ihsapi.Services
{
    public class PasswordGenerator
    {
        private static readonly Random Random = new Random();

        public static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
                "abcdefghijkmnopqrstuvwxyz",    // lowercase
                "0123456789",                   // digits
                "!@$?_-"                        // non-alphanumeric
            };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }

        // public static string createStrongPassword(int passwordLength)
        // {
        //     int seed = Random.Next(1, int.MaxValue);

        //     const string digits = "0123456789";
        //     const string UpperChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
        //     const string lowerChar = "abcdefghijkmnopqrstuvwxyz";
        //     const string specialCharacters = @"!#$%&'()*+,-./:;<=>?@[\]_";
        //     const string strongPass = digits + UpperChars + lowerChar + specialCharacters;

        //     var chars = new char[passwordLength];

        //     var rd = new Random(seed);
        //     //0-diglen,diglen-upperlen

        //     int[] len = { digits.Length - 1, UpperChars.Length - 1, lowerChar.Length - 1, specialCharacters.Length - 1 };
        //     int y = 0;
        //     int x = 0;
        //     for (var i = 0; i < passwordLength; i++)
        //     {
        //         chars[i] = strongPass[rd.Next(y, len[x])];
        //         y = len[x];
        //         x++;
        //         if (x == 3) { x = 0; }

        //         Console.WriteLine(x);

        //     }

        //     return new string(chars);
        // }
    }
}