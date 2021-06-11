using System;

namespace CS062CitacUdalosti
{
    class Program
    {
        static void Main(string[] args)
        {
            Counter c = new Counter(new Random().Next(10)+3);

            // zaregistrovat metodu c_ThresholdReached pro udalost ThresholdReached objektu c
            c.ThresholdReached += c_ThresholdReached;
            c.Reseted += c_Reseted;

            Console.WriteLine("press 'a' key to increase total");
            while (Console.ReadKey(true).KeyChar == 'a')
            {
                Console.WriteLine("adding one");
                c.Add(1);
            }
        }

        //handler metoda pro objekt c udalosti ThresholdReached
        static void c_ThresholdReached(object sender, EventArgs e)
        {
            Console.WriteLine("The threshold was reached, counter will be cleared");
            //Environment.Exit(0);
            (sender as Counter).Reset();
        }

        static void c_Reseted(object sender, EventArgs e)
        {
            Console.WriteLine("Counter is cleared now");
        }
    }

    class Counter
    {
        private int threshold;             // maximalni pocet
        private int total;                 // aktualni pocet

        public Counter(int passedThreshold)      // konstruktor citace a predani maxima
        {
            threshold = passedThreshold;     
        }
        public void Reset()
        {
            total = 0;
            OnReset(EventArgs.Empty);
        }

        public void Add(int x)                            // zvysi aktualni pocet o zadane X
        {
            total += x;
            if (total >= threshold)                       // kdyz aktualni pocet presahne limit
            {
                OnThresholdReached(EventArgs.Empty);        // provedeni metody pro vyvolani udalosti bez parametru
            }
        }

        protected virtual void OnThresholdReached(EventArgs e)
        {
            EventHandler handler = ThresholdReached;
            if (handler != null)
            {
                handler(this, e);                         // provest zaregistrovane handlery udalosti
            }
        }

        protected virtual void OnReset(EventArgs e)
        {
            EventHandler handler = Reseted;
            if (handler != null)
            {
                handler(this, e);                         // provest zaregistrovane handlery udalosti
            }
        }

        public event EventHandler ThresholdReached;       // datove pole pro ulozeni handleru udalosti

        public event EventHandler Reseted;
    }
}
