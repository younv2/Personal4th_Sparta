using System.Numerics;

public static class BigIntegerFormatter
{
    public static string FormatBigNumber(BigInteger number)
    {
        if (number < 1000)
        {
            return number.ToString();
        }

        string[] units = { "", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
        int unitIndex = 0;
        BigInteger divisor = 1000;

        while (number >= divisor && unitIndex < units.Length - 1)
        {
            number /= divisor;
            unitIndex++;
        }

        string formatString = unitIndex == 0 ? "{0}" : "{0:0.#}{1}";
        return string.Format(formatString, number, units[unitIndex]);
    }
}