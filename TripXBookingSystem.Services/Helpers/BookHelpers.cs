using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripXBookingSystem.Models.Options;

namespace TripXBookingSystem.Services.Helpers
{
    internal class BookHelpers
    {
        internal static readonly Random random = new Random();

        internal static string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        internal static int GenerateSleepTime()
        {
            return random.Next(30, 61);
        }

        internal static bool IsOptionAvailable(Option selectedOption)
        {
            return selectedOption != null;
        }

        internal static void SetOptionCode(List<Option> availableOptions)
        {
            foreach (var option in availableOptions)
            {
                option.OptionCode = BookHelpers.GenerateRandomCode(7);
            }
        }
    }
}
