using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{
    public class SzotarElem<K,T>
    {
        public K kulcs;
        public T tart;

        public SzotarElem(K key, T value)
        {
            this.kulcs = key;
            this.tart = value;
        }
    }

    public class HasitoSzotarTulcsordulasiTerulettel<K, T> : Szotar<K, T>
    {
        private SzotarElem<K, T>[] E;
        private Func<K, int> h;
        private LancoltLista<SzotarElem<K, T>> U;
        public HasitoSzotarTulcsordulasiTerulettel(int meret, Func<K,int> hasitoFuggveny)
        {
            this.E = new SzotarElem<K, T>[meret];
            this.h = (K) =>
            {
                return hasitoFuggveny(K) % meret;
            };
            U = new LancoltLista<SzotarElem<K, T>>();
        }

        public HasitoSzotarTulcsordulasiTerulettel(int meret): this(meret, (K) => { return K.GetHashCode() % meret; }) { }
        public void Beir(K kulcs, T ertek)
        {
            SzotarElem<K, T> temp = KulcsKereses(kulcs);
            if (temp != null)
            {
                temp.tart = ertek;
            }
            else
            {
                SzotarElem<K, T> elem = new SzotarElem<K, T>(kulcs,ertek);
                int index = h.Invoke(kulcs);
                if (E[index] == null)
                {
                    E[index] = elem;
                }
                else
                {
                    U.Hozzafuz(elem);
                }
            }
            
        }

        public T Kiolvas(K kulcs)
        {
            SzotarElem<K, T> temp = KulcsKereses(kulcs);
            if (temp == null)
            {
                throw new HibasKulcsKivetel();
            }
            return temp.tart;

        }

        public void Torol(K kulcs)
        {
            SzotarElem<K, T> temp = KulcsKereses(kulcs);
            if (temp == null)
            {
                throw new HibasKulcsKivetel();
            }
            int index = h.Invoke(kulcs);
            E[index] = null;
            U.Torol(temp);


        }

        private SzotarElem<K,T> KulcsKereses(K kulcs)
        {
            int index = h.Invoke(kulcs);
            SzotarElem<K,T> temp = E[index];
            if (E[index]!=null)
            {
                if (temp.kulcs.Equals(kulcs))
                {
                    return temp;
                }
                
            }
            temp = null;
            U.Bejar((x) =>
            {
                if (x.kulcs.Equals(kulcs))
                {
                    temp = x;
                }
            });
            return temp;
        }
    }
}
