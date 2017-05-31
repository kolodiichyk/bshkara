using System.Collections.Generic;
using System.Threading;

namespace Bshkara.Core.Helper
{
    /// <summary>
    /// Culture helper
    /// </summary>
    internal static class CultureHelper
    {
        /// <summary>
        /// Include ONLY cultures you are implementing
        /// </summary>
        private static readonly List<string> Cultures = new List<string>
        {
            "en",
            "ar"
        };

        /// <summary>
        /// Returns <see langword="true" /> if the language is a right-to-left
        /// language. Otherwise, false.
        /// </summary>
        public static bool IsRightToLeft()
        {
            return Thread.CurrentThread.CurrentCulture.TextInfo.IsRightToLeft;
        }

        /// <summary>
        /// Returns default culture name which is the first name declared (e.g.
        /// en-US)
        /// </summary>
        public static string GetDefaultCulture()
        {
            return Cultures[0]; // return Default culture
        }

        /// <summary>
        /// Returns default culture name which is the first name declared (e.g.
        /// en-US)
        /// </summary>
        public static string GetCurrentCulture()
        {
            return Thread.CurrentThread.CurrentCulture.Name;
        }

        /// <summary>
        /// Returns default culture name which is the first name declared (e.g.
        /// en-US)
        /// </summary>
        public static string GetCurrentNeutralCulture()
        {
            return GetNeutralCulture(Thread.CurrentThread.CurrentCulture.Name);
        }

        /// <summary>
        /// Returns default culture <paramref name="name" /> which is the first
        /// <paramref name="name" /> declared (e.g. en-US)
        /// </summary>
        public static string GetNeutralCulture(string name)
        {
            if (!name.Contains("-")) return name;

            return name.Split('-')[0]; // Read first part only. E.g. "en", "ar"
        }
    }
}