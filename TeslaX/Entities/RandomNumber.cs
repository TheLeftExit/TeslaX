using System;

namespace TheLeftExit.TeslaX.Entities
{
    internal class RandomNumber
    {
        // "Pure" random number generator for all instances.
        private static Random random = new Random();

        // Min/max values to be passed in the generator, with evenly distributed probability.
        private double minIn;
        private double maxIn;
        // Function using a value between minIn, maxIn; allows creating new distribution rules.
        private Func<double, double> generator;

        private double last;
        public static implicit operator double(RandomNumber r)
        {
            return r.last;
        }
        public static implicit operator int(RandomNumber r)
        {
            return Convert.ToInt32(r.last);
        }

        public void Next()
        {
            double newIn = random.NextDouble() * (maxIn - minIn) + minIn;
            last = generator(newIn);
        }

        public RandomNumber(double min, double max, Func<double, double> foo)
        {
            minIn = min;
            maxIn = max;
            generator = foo;
            Next();
        }
    }
}
