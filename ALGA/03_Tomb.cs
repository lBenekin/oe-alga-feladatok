using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{
    public class TombVerem<T> : Verem<T>
    {
        protected T[] E;
        protected int n = 0;

        public TombVerem(int n)
        {
            E = new T[n];
        }

        public bool Ures
        {
            get
            {
                return n == 0;
            }
        }


        public T Felso()
        {
            if (Ures)
            {
                throw new NincsElemKivetel();
            }
            return E[n - 1];

        }

        public void Verembe(T ertek)
        {
            if (n >= E.Length)
            {
                throw new NincsHelyKivetel();
            }
            E[n] = ertek;
            n++;
        }

        public T Verembol()
        {
            if (Ures)
            {
                throw new NincsElemKivetel();
            }
            T temp = E[--n];
            E[n] = default;
            return temp;
        }
    }

    public class TombSor<T> : Sor<T>
    {
        protected T[] E;
        protected int n = 0;
        protected int u = -1;
        protected int e = 0;

        public TombSor(int n)
        {
            E = new T[n];
        }
        public bool Ures
        {
            get
            {
                return n == 0;
            }
        }

        public T Elso()
        {
            if (Ures)
            {
                throw new NincsElemKivetel();
            }
            e = e % E.Length;
            return E[e];
        }

        public void Sorba(T ertek)
        {
            if (n >= E.Length)
            {
                throw new NincsHelyKivetel();
            }
            u = ++u % E.Length;
            E[u] = ertek;
            n++;

        }

        public T Sorbol()
        {
            if (Ures)
            {
                throw new NincsElemKivetel();
            }
            T temp = Elso();
            E[e%E.Length] = default;
            e++;
            n--;
            return temp;
        }
    }
    public class TombLista<T> : Lista<T>, IEnumerable<T>
    {
        protected T[] E;
        protected int n = 0;

        public TombLista()
        {
            E = new T[1];
        }

        public TombLista(int n)
        {
            E = new T[n];
        }
        public int Elemszam
        {
            get
            {
                return n;
            }
        }

        public void Bejar(Action<T> muvelet)
        {
            foreach (T item in E)
            {
                muvelet?.Invoke(item);
            }
        }

        public void Beszur(int index, T ertek)
        {
            //n: 0 index: 0 ertek: 1
            if (index > n || index < 0)
            {
                throw new HibasIndexKivetel();
            }
            if (++n >= E.Length)
            {
                T[] temp = new T[E.Length * 2];
                for (int i = 0; i < E.Length; i++)
                {
                    temp[i] = E[i];
                }
                E = temp;
                
            }
            for (int i = n; i > index; i--)
            {
                E[i] = E[i - 1];
            }
            E[index] = ertek;

            //n++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Hozzafuz(T ertek)
        {
            Beszur(n, ertek);
        }

        public T Kiolvas(int index)
        {
            if (index > n || index < 0)
            {
                throw new HibasIndexKivetel();
            }
            return E[index];
        }

        public void Modosit(int index, T ertek)
        {
            if (index > n || index < 0)
            {
                throw new HibasIndexKivetel();
            }
            E[index] = ertek;
        }

        public void Torol(T ertek)
        {
            int db = 0;
            int count = 0;
            for (int i = 0; i < n; i++)
            {
                E[i - db] = E[i];
                if (E[i].Equals(ertek))
                {
                    db++;
                    count++;
                }
            }
            n -=count;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            TombListaBejaro<T> bejaro = new TombListaBejaro<T>(E, n);
            return bejaro;
        }
    }

    public class TombListaBejaro<T> : IEnumerator<T>
    {
        T[] E;
        int n;
        int aktualisIndex = -1;
        public T Current
        {
            get
            {
                return E[aktualisIndex];
            }
        }

        public TombListaBejaro(T[] E, int n)
        {
            this.E = E;
            this.n = n;
        }

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            if (aktualisIndex < n)
            {
                aktualisIndex++;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            aktualisIndex = -1;
        }
    }
}
