using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Petroteks.Bll.Helpers
{
    public static class FriendlyUrlHelper
    {
        public static string GetFriendlyTitle(string incomingText)
        {
            if (incomingText != null)
            {
                _ = incomingText.Replace("ş", "s");
                _ = incomingText.Replace("Ş", "s");
                _ = incomingText.Replace("İ", "i");
                _ = incomingText.Replace("I", "i");
                _ = incomingText.Replace("ı", "i");
                _ = incomingText.Replace("ö", "o");
                _ = incomingText.Replace("Ö", "o");
                _ = incomingText.Replace("ü", "u");
                _ = incomingText.Replace("Ü", "u");
                _ = incomingText.Replace("Ç", "c");
                _ = incomingText.Replace("ç", "c");
                _ = incomingText.Replace("ğ", "g");
                _ = incomingText.Replace("Ğ", "g");
                _ = incomingText.Replace(" ", "-");
                _ = incomingText.Replace("---", "-");
                _ = incomingText.Replace("?", "");
                _ = incomingText.Replace("/", "");
                _ = incomingText.Replace(".", "");
                _ = incomingText.Replace("'", "");
                _ = incomingText.Replace("#", "");
                _ = incomingText.Replace("%", "");
                _ = incomingText.Replace("&", "");
                _ = incomingText.Replace("*", "");
                _ = incomingText.Replace("!", "");
                _ = incomingText.Replace("@", "");
                _ = incomingText.Replace("+", "");
                _ = incomingText.ToLower();
                _ = incomingText.Trim();
                string encodedUrl = (incomingText ?? "").ToLower();
                encodedUrl = Regex.Replace(encodedUrl, @"\&+", "and");
                encodedUrl = encodedUrl.Replace("'", "");
                encodedUrl = Regex.Replace(encodedUrl, @"[^a-z0-9]", "-");
                encodedUrl = Regex.Replace(encodedUrl, @"-+", "-");
                encodedUrl = encodedUrl.Trim('-');
                return encodedUrl;
            }
            else
                return "";
        }
        public static string CleanFileName(string fileName) =>
             Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty));
    }
}
