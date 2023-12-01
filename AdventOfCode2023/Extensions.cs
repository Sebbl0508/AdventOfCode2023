namespace AdventOfCode2023;

public static class Extensions
{
    public static List<int> AllIndicesOf(this string str, string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new ArgumentException("the string to find may not be empty", nameof(value));

        List<int> indices = new List<int>();
        for (int i = 0; ; i += value.Length)
        {
            i = str.IndexOf(value, i, StringComparison.Ordinal);
            if (i == -1)
                return indices;

            indices.Add(i);
        }
    }
}