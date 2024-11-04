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


            //int i = 0;
            //while (i < M[level])
            //{
            //    i++;
            //    if (ft(level, R[level-1,i]))
            //    {
            //        E[level-1] = R[level-1, i];
            //        if (level == n)
            //        {
            //            if (!van || josag(E) > josag(O))
            //            {
            //                // O = E; // itt copy kell mert ref nem gud
            //                Array.Copy(O, E, O.Length);
            //            }
            //            van = true;
            //        }
            //        else
            //        {
            //            Backtrack(level + 1, ref E, ref van, ref O);
            //        }
            //    }
            //}
        }

        public T[] OptimalisMegoldas()
        {
            T[] E = new T[n];
            T[] O = new T[n];
            bool van = false;
            Backtrack(1, ref E, ref van, ref O);
            return O;
        }
    }
    public class VisszalepesesHatizsakPakolas
    {
        protected HatizsakProblema problema;
        public int LepesSzam { private set; get; }

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
                (szint,r) => true,
                (szint,E,r) => true,
                (R) => 1
                );


            bool[] optimal = visszalepeses.OptimalisMegoldas();
            this.LepesSzam = visszalepeses.LepesSzam;
            return optimal;
            
        }

        public float OptimalisErtek()
        {
            bool[] opt = OptimalisMegoldas();
            return problema.OsszErtek(opt);
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
            int i = 0;
            while (i < M[level])
            {
                i++;
                if (ft(level, R[level-1, i]))
                {
                    E[level-1] = R[level-1, i];
                    if (level == n)
                    {
                        if (!van || josag(E) > josag(O))
                        {
                            //O = E; // itt copy kell mert ref nem gud
                            //Array.Copy(O, E, O.Length);
                            CopyArray(ref O, ref E);
                        }
                        van = true;
                    }
                    else if(josag(E) + fb(level,E) > josag(O))
                    {
                        Backtrack(level + 1, ref E, ref van, ref O);
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
                (szint,r) => true,
                (szint,E,r) => true,
                (szint,E) => 1.0f,
                (R) => 1
                );


            bool[] optimal = opt.OptimalisMegoldas();
            return optimal;
        }
    }
}
