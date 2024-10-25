using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Optimalizalas
{
    public class HatizsakProblema
    {
        public int n { get; } 
        public int Wmax { get; } //backpack size

        public int[] w { get; }
        public float[] p { get; }

        public HatizsakProblema(int n, int wmax, int[] w, float[] p)
        {
            this.n = n;
            this.Wmax = wmax;
            this.w = w;
            this.p = p;
        }

        public int OsszSuly(bool[] pakolas)
        {
            int sum = 0;
            for (int i = 0; i < n; i++)
            {
                if (pakolas[i])
                {
                    sum += w[i];
                }
            }
            return sum;
        }
        public float OsszErtek(bool[] pakolas)
        {
            float sum = 0;
            for (int i = 0; i < n; i++)
            {
                if (pakolas[i])
                {
                    sum += p[i];
                }
            }
            return sum;
        }

        public bool Ervenyes(bool[] pakolas)
        {
            return OsszSuly(pakolas) <= Wmax;
        }
    }
    public class NyersEro<T>
    {
        int m; //number of possible solutions

        Func<int, T> generator;
        Func<T, float> josag;
        public int LepesSzam { get; set; } = 0;

        public NyersEro(int m, Func<int, T> generator, Func<T, float> josag)
        {
            this.m = m;
            this.generator = generator;
            this.josag = josag;
        }

        public T OptimalisMegoldas()
        {
            T o = default;
            float max = float.MinValue;
            for (int i = 1; i <= m; i++)
            {
                LepesSzam++;
                T x = generator(i);
                float josagValue = josag(x);
                if (josagValue > max)
                {
                    max = josagValue;
                    o = x;
                    
                }
            }
            return o;
        }
    }
    public class NyersEroHatizsakPakolas
    {
        public int LepesSzam { get; private set; }
        HatizsakProblema problema;

        public NyersEroHatizsakPakolas(HatizsakProblema problema)
        {
            this.problema = problema;
        }
        public bool[] Generator(int i)
        {
            int num = i - 1;
            bool[] K = new bool[problema.n];
            for (int j = 0; j < problema.n; j++)
            {
                K[j] = (int)(num / Math.Pow(2,j-1)) % 2 == 1;
            }
            return K;
        }
        public float Josag(bool[] pakolas)
        {
            return problema.Ervenyes(pakolas) ? problema.OsszErtek(pakolas) : -1;
        }
        public bool[] OptimalisMegoldas()
        {
            NyersEro<bool[]> nyersEro = new NyersEro<bool[]>((int)Math.Pow(2, problema.n), Generator, Josag);
            bool[] opt = nyersEro.OptimalisMegoldas();
            LepesSzam = nyersEro.LepesSzam;
            return opt;
        }
        public float OptimalisErtek()
        {
            bool[] opt = OptimalisMegoldas();
            return problema.OsszErtek(opt);
        }
    }
}
