using System;
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
           return E[n-1];
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
    public class TombLista<T> : Lista<T>
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
        public int Elemszam => throw new NotImplementedException();

        public void Bejar(Action<T> muvelet)
        {
            throw new NotImplementedException();
        }

        public void Beszur(int index, T ertek)
        {
            
            if (index > E.Length || index<0)
            {
                throw new HibasIndexKivetel();
            }
            if (n >= E.Length)
            {
                T[] temp = new T[E.Length * 2];
                E = temp;
                //l:5 n:5 index: 3
                for (int i = 0; i < n-index; i++)
                {
                    T tmp = E[index + (n-index-1)];
                    E[index+i+1] = E[index+i];
                }
            }
            E[index] = ertek;
            
        }

        public void Hozzafuz(T ertek)
        {
            Beszur(n, ertek);
        }

        public T Kiolvas(int index)
        {
            return E[index];
        }

        public void Modosit(int index, T ertek)
        {
            E[index] = ertek;
        }

        public void Torol(T ertek)
        {
            throw new NotImplementedException();
        }
    }
}
