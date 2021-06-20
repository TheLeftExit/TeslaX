using System.Diagnostics;

namespace TheLeftExit.TeslaX
{
    public abstract class TimedManager
    {
        protected Stopwatch sw;
        protected bool down;
        protected int last;

        public bool IsDown => down;

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
