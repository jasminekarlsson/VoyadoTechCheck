namespace TechCheck.WebAPI.Utils
{
    public static class StringHelper
    {
        public static List<string> CutUpString(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return [];

            var words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            return words.ToList();
        }
    }
}
