namespace SimpleMVC.App.Utilities
{
    public static class HyperlinkConverter
    {
        public static string ApplyHyperlinks(this string shout)
        {
            string newShout = string.Empty;
            var words = shout.Split();
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].StartsWith("http://") ||
                    words[i].StartsWith("https://")
                    )
                {
                    words[i] = $"<a href=\"{words[i]}\">{words[i]}</a>";
                }
                else if (words[i].StartsWith("www."))
                {
                    words[i] = $"<a href=\"http://{words[i]}\">{words[i]}</a>";
                }
            }
            newShout = string.Join(" ", words);
            return newShout;
        }
    }
}
