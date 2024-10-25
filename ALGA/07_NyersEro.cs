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
            for (int i = 0; i < pakolas.Length; i++)
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
            for (int i = 0; i < pakolas.Length; i++)
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
            return OsszSuly(pakolas) <= n;
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
            T o = generator(1);
            for (int i = 1; i < m; i++)
            {
                T x = generator(i);
                if (josag(x) >= josag(o))
                {
                    o = x;
                    LepesSzam++;
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
                K[j] = (int)(num / 2^j-1) % 2 == 1;
            }
            return K;
        }
        public float Josag(bool[] pakolas)
        {
            return problema.Ervenyes(pakolas) ? problema.OsszErtek(pakolas) : -1;
        }
        public bool[] OptimalisMegoldas()
        {
            NyersEro<bool[]> nyersEro = new NyersEro<bool[]>(2^problema.n, Generator, Josag);
            bool[] opt = nyersEro.OptimalisMegoldas();
            LepesSzam = nyersEro.LepesSzam;
            return opt;
        }
        public float OptimalisErtek()
        {
            bool[] O = Generator(1);
            for (int i = 2; i < (2 ^ problema.n); i++)
            {
                bool[] K = Generator(i);
                if ((problema.OsszSuly(K) <= problema.Wmax) && (problema.OsszErtek(K) >= problema.OsszErtek(O)))
                {
                    O = K;
                }
            }
            return problema.OsszErtek(O);
        }
    }
}
