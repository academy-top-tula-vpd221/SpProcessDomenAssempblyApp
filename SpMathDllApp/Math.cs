namespace SpMathDllApp
{
    public class Math
    {
        public static double Power(double a, int n)
        {
            double power = 1;
            for(int i = 0; i < n; i++)
                power *= a;
            return power;
        }
    }
}