using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Optimalizalas
{
    public class DinamikusHatizsakPakolas
    {
        HatizsakProblema problema;
        public int LepesSzam { get; protected set; }

        public DinamikusHatizsakPakolas(HatizsakProblema problem)
        {
            this.problema = problem;
        }
        public float[,] TablazatFeltotes()
        {
            float[,] F = new float[problema.n + 1, problema.Wmax + 1];

            for (int i = 0; i < problema.n; i++)
            {
                F[i, 0] = 0;
            }
            for (int i = 1; i < problema.Wmax; i++)
            {
                F[0, i] = 0;
            }
            for (int i = 1; i <= problema.n; i++)
            {
                for (int j = 1; j <= problema.Wmax; j++)
                {
                    this.LepesSzam++;

                    if (j >= problema.w[i - 1])
                    {
                        F[i, j] = Math.Max(F[i - 1, j], (F[i - 1, j - problema.w[i - 1]]) + problema.p[i - 1]);
                    }
                    else
                    {
                        F[i, j] = F[i - 1, j];
                    }
                }
            }
            return F;
        }
        public float OptimalisErtek()
        {
            return TablazatFeltotes()[problema.n, problema.Wmax];
        }
        public bool[] OptimalisMegoldas()
        {
            float[,] temp = TablazatFeltotes();
            int t = problema.n;
            int h = problema.Wmax;
            bool[] O = new bool[t];

            while (t>0 && h > 0)
            {
                if (temp[t,h] != temp[t-1,h])
                {
                    O[t-1] = true;
                    h -= problema.w[t-1];
                }
                t--;
            }
            return O;
        }
    }
}
