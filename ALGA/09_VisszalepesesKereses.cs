using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Optimalizalas
{
    public class VisszalepesesOptimalizacio<T>
    {
        protected int n; //depth
        protected int[] M;
        protected T[,] R;
        protected Func<int,T,bool> ft;
        protected Func<int,T,T[],bool> fk;
        protected Func<T[], float> josag;
        public int LepesSzam { get; set; }

        public VisszalepesesOptimalizacio(int n, int[] m, T[,] r, Func<int, T, bool> ft, Func<int, T, T[], bool>  fk, Func<T[], float> josag)
        {
            this.n = n;
            M = m;
            R = r;
            this.ft = ft;
            this.fk = fk;
            this.josag = josag;
        }

        protected virtual void Backtrack(int level, ref T[] E, ref bool van, ref T[] O)
        {
            int i = -1;
            while (i < M[level] - 1)
            {
                i++;

                if (ft(level, R[level, i]) && fk(level, R[level, i], E))
                {
                    E[level] = R[level, i];

                    if (level == n - 1)
                    {
                        if (!van || josag(E) > josag(O))
                        {
                            for (int j = 0; j < E.Length; j++)
                            {
                                O[j] = E[j];
                            }
                            van = true;
                        }
                    }
                    else
                    {
                        Backtrack(level + 1, ref E, ref van, ref O);
                    }
                }
            }
        }

        public T[] OptimalisMegoldas()
        {
            T[] E = new T[n];
            T[] O = new T[n];
            bool van = false;
            Backtrack(0, ref E, ref van, ref O);
            return O;
        }
    }
    public class VisszalepesesHatizsakPakolas
    {
        protected HatizsakProblema problema;
        public int LepesSzam { get; private set; }

        public VisszalepesesHatizsakPakolas(HatizsakProblema problema)
        {
            this.problema = problema;
        }
        public virtual bool[] OptimalisMegoldas()
        {
            int n = problema.n;
            int[] M = new int[n];
            bool[,] R = new bool[n,2];
            for (int i = 0; i < M.Length; i++)
            {
                M[i] = 2;
            }
            for (int i = 0; i < n; i++)
            {
                R[i, 0] = true;
                R[i, 1] = false;
            }

            VisszalepesesOptimalizacio<bool> visszalepeses = new VisszalepesesOptimalizacio<bool>(
                n, M, R,
                (level, r) => !r || problema.w[level] <= problema.Wmax,
                (level, r, E) => problema.OsszSuly(E) <= problema.Wmax && (!r || problema.OsszSuly(E) + problema.w[level] <= problema.Wmax),
                (E) => (int)problema.OsszErtek(E));


            return visszalepeses.OptimalisMegoldas();
            
        }

        public float OptimalisErtek()
        {
            return problema.OsszErtek(OptimalisMegoldas());
        }
    }

    public class SzetvalasztasEsKorlatozasOptimalizacio<T> : VisszalepesesOptimalizacio<T>
    {
        protected Func<int, T[], float> fb;
        public SzetvalasztasEsKorlatozasOptimalizacio(int n, int[] m, T[,] r, Func<int, T, bool> ft, Func<int, T, T[], bool> fk, Func<int, T[], float> fb, Func<T[], float> josag) : base(n, m, r, ft, fk, josag)
        {
            this.fb = fb;
        }
        private void CopyArray(ref T[] A, ref T[] B)
        {
            for (int i = 0; i < B.Length; i++)
            {
                A[i] = B[i];
            }
        }
        protected override void Backtrack(int level, ref T[] E, ref bool van, ref T[] O)
        {
            for (int i = 0; i < M[level]; i++)
            {
                if (ft(level, R[level, i]) && fk(level, R[level, i], E))
                {
                    E[level] = R[level, i];
                    if (level == n - 1)
                    {
                        if (!van || josag(E) > josag(O))
                        {
                            for (int j = 0; j < E.Length; j++)
                            {
                                O[j] = E[j];
                            }
                            van = true;
                        }
                    }
                    else
                    {
                        if (josag(E) + fb(level, E) > josag(O))
                        {
                            Backtrack(level + 1, ref E, ref van, ref O);
                        }
                    }
                }
            }
        }
    }

    public class SzetvalasztasEsKorlatozasHatizsakPakolas : VisszalepesesHatizsakPakolas
    {
        public SzetvalasztasEsKorlatozasHatizsakPakolas(HatizsakProblema problema) : base(problema) {}

        public override bool[] OptimalisMegoldas()
        {
            int n = problema.n;
            int[] M = new int[n];
            bool[,] R = new bool[n, 2];
            for (int i = 0; i < M.Length; i++)
            {
                M[i] = 2;
            }
            for (int i = 0; i < n; i++)
            {
                R[i, 0] = true;
                R[i, 1] = false;
            }
            SzetvalasztasEsKorlatozasOptimalizacio<bool> opt = new SzetvalasztasEsKorlatozasOptimalizacio<bool>(
                n, M, R,
                (level, r) => !r || problema.w[level] <= problema.Wmax, 
                (level, r, E) => {return problema.OsszSuly(E) <= problema.Wmax && (!r || problema.OsszSuly(E) + problema.w[level] <= problema.Wmax);},
                (level, E) => {
                    float temp = 0;
                    for (int i = level + 1; i < n; i++)
                    {
                        if (problema.OsszSuly(E) + problema.w[i] <= problema.Wmax)
                        {
                            temp += problema.p[i];
                        }
                    }
                    return temp;
                }, 
                (E) => (int)problema.OsszErtek(E));


            bool[] optimal = opt.OptimalisMegoldas();
            return optimal;
        }
    }
}
