using System.Diagnostics;

namespace TheLeftExit.TeslaX.Helpers
{
    public abstract class TimedManager
    {
        protected Stopwatch sw;
        protected bool down;
        protected int last;

        protected void toggle()
        {
            down = !down;
            last = (int)sw.ElapsedMilliseconds;
        }

        protected int elapsed
        {
            get { return (int)sw.ElapsedMilliseconds - last; }
        }
        public TimedManager()
        {
            sw = Stopwatch.StartNew();
            down = false;
            last = 0;
        }
    }
}
