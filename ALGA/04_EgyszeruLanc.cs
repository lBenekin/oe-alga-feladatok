using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{
    public class LancoltVerem<T>
    {
        public LancElem<T> fej { get; set; }
        public bool Ures => fej.tart == null;

        public LancoltVerem() 
        {
            fej = null;
        }
        public void Verembe(T ertek)
        {
            if (Ures)
            {
                throw new NincsElemKivetel();
            }
            LancElem<T> temp = new LancElem<T>(ertek, fej);
            fej = temp;
        }

        public T Verembol()
        {
            if (Ures)
            {
                throw new NincsElemKivetel();
            }
            T temp = fej.tart;
            fej = fej.kov;
            return temp;
        }
        public T Felso()
        {
            if (Ures)
            {
                throw new NincsElemKivetel();
            }
            return fej.tart;
        }
    }
    public class LancElem<T>
    {
        public T tart {  get; set; }
        public LancElem<T> kov { get; set; }

        public LancElem(T tart, LancElem<T> kov)
        {
            this.tart = tart;
            this.kov = kov;
        }
    }
}
