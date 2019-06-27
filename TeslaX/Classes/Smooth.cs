using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TeslaX
{
    // Stores a value protected from brief large changes. Requires "outside of safety radius" predicate and lock duration.
    // Whenever a new value is outside of "safety radius", the old value is locked.
    // For the duration of the lock, any new value inside the "safety radius" disables the lock and is written normally.
    // If after duration of the lock no new values are within the "safety radius", next value is written in regardless.
    //
    // Example:
    // * Outside of safety radius: x => Math.Abs(x - StableValue) > 15
    // * Lock duration: 150
    // New is {set}, Stable is {get}
    //
    // Time  | New | Stable | Comments
    // ------|-----|--------|----------
    // 0     | 0   | 0      | First value is accepted regardless.
    // 100   | 5   | 5      |
    // 200   | 10  | 10     |
    // 300   | 15  | 15     |
    // 400   | 20  | 20     |
    // 500   | 100 | 20     | A spike is detected.
    // 600   | 30  | 30     | The spike is corrected.
    // 700   | 35  | 35     |
    // 800   | 200 | 35     | A spike is detected.
    // 900   | 205 | 35     | The spike is maintained, lock duration hasn't expired.
    // 1000  | 210 | 210    | The spike is maintained, lock duration has expired.
    // 1100  | 215 | 215    |

    public class Smooth<T>
    {
        // Protected value.
        private T svalue;
        // Milliseconds until spike is accepted.
        private int slength;
        // Condition for spike to occur.
        private Func<T,T,bool> scondition;

        // Whether there's a spike.
        private bool spike;
        // Timer.
        private Stopwatch swatch;
        // Whether the value has been set for the first time
        private bool initialized;

        public Smooth(int length, Func<T,T,bool> condition)
        {
            slength = length;
            scondition = condition;
            initialized = false;
            spike = false;
            swatch = new Stopwatch();
        }

        private void Update(T nvalue)
        {
            if (!initialized)
            {
                svalue = nvalue;
                initialized = true;
                return;
            }
            if (spike)
            {
                if (swatch.ElapsedMilliseconds >= slength || !scondition(nvalue,svalue))
                {
                    svalue = nvalue;
                    spike = false;
                    swatch.Stop();
                    return;
                }
            }
            else
            {
                if (scondition(nvalue, svalue))
                {
                    spike = true;
                    swatch.Restart();
                    return;
                }
                else
                    svalue = nvalue;
            }
        }

        public T Value
        {
            get { return svalue; }
            set { Update(value); }
        }

        public static implicit operator T(Smooth<T> s)
        {
            return s.Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
