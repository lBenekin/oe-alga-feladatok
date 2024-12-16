 using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Paradigmak
{
    public interface IVegrehajthato
    {
        void Vegrehajtas();
    }
    public interface IFuggo
    {
        public bool FuggosegTeljesul { get; }
    }
    public class FeladatTarolo<T> : IEnumerable<T> where T : IVegrehajthato
    {
        protected T[] tarolo;
        protected int n = 0; //number of elements

        public FeladatTarolo(int meret)
        {
            this.tarolo = new T[meret];
        }

        public void Felvesz(T elem)
        {
            if (n < tarolo.Length)
            {
                tarolo[n] = elem;
                n++;
            }
            else
            {
                throw new TaroloMegteltKivetel();
            }
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            FeladatTaroloBejaro<T> bejaro = new FeladatTaroloBejaro<T>(tarolo, n);
            return bejaro;
        }

        public virtual void MindentVegrehajt()
        {
            for (int i = 0; i < n; i++)
            {
                tarolo[i].Vegrehajtas();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class FeladatTaroloBejaro<T> : IEnumerator<T>
    {
        T[] tarolo;
        int n;
        int aktualisIndex = -1;

        public FeladatTaroloBejaro(T[] tarolo, int n)
        {
            this.tarolo = tarolo;
            this.n = n;
        }
        public T Current
        {
            get
            {
                return tarolo[aktualisIndex];
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {


        }

        public bool MoveNext()
        {
            if (aktualisIndex < n-1)
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
    public class FuggoFeladatTarolo<T> : FeladatTarolo<T> where T : IVegrehajthato, IFuggo
    {
        public FuggoFeladatTarolo(int meret) : base(meret) { }

        public override void MindentVegrehajt()
        {
            foreach (var item in tarolo)
            {
                if (item is IFuggo && item.FuggosegTeljesul)
                {
                    item.Vegrehajtas();
                }
            }
        }
    }

    public class TaroloMegteltKivetel : Exception
    {

    }
}
