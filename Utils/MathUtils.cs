namespace Formula9.AdventOfCode.Utils
{
    public static class MathUtils
    {
        public static int Gcd(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }
            return a | b;
        }

        public static long Gcd(long a, long b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }
            return a | b;
        }

        public static int Lcm(params int[] periods) => periods.Length == 2 ? 
            periods[0] * periods[1] / Gcd(periods[0], periods[1]) :
            Lcm(periods[0], Lcm(periods[1..]));

        public static long Lcm(params long[] periods) => periods.Length == 2 ? 
            periods[0] * periods[1] / Gcd(periods[0], periods[1]) :
            Lcm(periods[0], Lcm(periods[1..]));
    }
}