namespace PublicAPI.Utiities
{
    public class SubQueries
    {
        public SubQueries() { }

        public IConfiguration Configuration { get; set; }
        public SubQueries(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static bool IsPrimeNumber(int number)
        {
            if (number <= 1)
                return false;

            // 2 is the only even prime number
            if (number == 2)
                return true;

            // All other even numbers are not prime
            if (number % 2 == 0)
                return false;

            // Check odd divisors from 3 up to the square root of the number
            int boundary = (int)Math.Sqrt(number);
            for (int i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0)
                    return false;
            }

            return true;

        }

        public static int IsPerfect(int number)
        {
            // Perfect numbers are positive and greater than 1
            if (number <= 1)
                return number;

            int sum = 1; // 1 is always a proper divisor
            int sqrt = (int)Math.Sqrt(number);

            // Check divisors from 2 up to the square root of the number
            for (int i = 2; i <= sqrt; i++)
            {
                if (number % i == 0)
                {
                    sum += i;
                    int otherDivisor = number / i;

                    // Avoid adding the square root twice for perfect squares
                    if (otherDivisor != i)
                        sum += otherDivisor;
                }
            }

            return sum;
        }

        public static bool IsArmstrong(int number)
        {
            // Convert the number to a string to determine the number of digits.
            string numStr = number.ToString();
            int numDigits = numStr.Length;
            int sum = 0;
            int temp = number;

            // Process each digit
            while (temp > 0)
            {
                int digit = temp % 10;
                // Raise the digit to the power of numDigits and add to sum
                sum += (int)Math.Pow(digit, numDigits);
                temp /= 10;
            }

            return sum == number;
        }

    }
}
