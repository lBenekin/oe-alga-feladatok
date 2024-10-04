using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{
    public class LancElem<T>
    {
        public T tart { get; set; }
        public LancElem<T> kov { get; set; }

        public LancElem(T tart, LancElem<T> kov)
        {
            this.tart = tart;
            this.kov = kov;
        }
    }
    public class LancoltVerem<T> : Verem<T>
    {
        public LancElem<T> fej { get; set; }
        public bool Ures
        {
            get
            {
                return fej == null;
            }
        }

        public LancoltVerem() 
        {
            fej = null;
        }
        public void Verembe(T ertek)
        {
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

   public class LancoltSor<T> : Sor<T>
    {
        public LancElem<T> fej;
        public LancElem<T> vege;
        public bool Ures
        {
            get
            {
                return fej == null;
            }
        }
        public LancoltSor()
        {
            // 1 2 3 4 5
            // v        f
            fej = null;
            vege = null;
        }
        public T Elso()
        {
            if (Ures)
            {
                throw new NincsElemKivetel();
            }
            return fej.tart;
        }

        public void Sorba(T ertek)
        {
            // 1 2 3 4 5
            // f       v
            LancElem<T> temp = new LancElem<T>(ertek, null);

            if (Ures)
            {
                fej = temp;
                vege = fej;
            }
            else
            {
                vege.kov = temp;
                vege = temp;
            }

            
        }

        public T Sorbol()
        {
            if (Ures)
            {
                throw new NincsElemKivetel();
            }

            T temp = fej.tart;
            fej = fej.kov;
            return temp;
        }
    }

    public class LancoltLista<T> : Lista<T>, IEnumerable<T>
    {
        public LancElem<T> fej;
        public int Elemszam
        {
            get
            {
                LancElem<T> temp = fej;
                int counter = 0;
                while (temp != null)
                {

                    temp = temp.kov;
                    counter++;
                }
                return counter;
            }
        }

        public LancoltLista()
        {
            fej = null;
        }

        public T Kiolvas(int index)
        {
            LancElem<T> temp = fej;
            int counter = 0;
            while (counter != index && temp.kov != null)
            {
                
                temp = temp.kov;
                counter++;
            }
            if (temp == null)
            {
                throw new HibasIndexKivetel();
            }
            return temp.tart;
        }

        public void Modosit(int index, T ertek)
        {
            LancElem<T> temp = fej;
            int counter = 0;
            while (temp != null && counter < index)
            {
                temp = temp.kov;
                counter++;
            }
            if (temp != null)
            {
                temp.tart = ertek;
            }
            else
            {
                throw new HibasIndexKivetel();
            }
        }

        public void Hozzafuz(T ertek)
        {
            LancElem<T> value = new LancElem<T>(ertek, null);
            LancElem<T> temp = new LancElem<T>(ertek, null);

            if (fej == null)
            {
                fej = value;
            }
            else
            {
                temp = fej;
                while (temp.kov != null)
                {
                    temp = temp.kov;
                }

                temp.kov = value;
            }
            
        }

        public void Beszur(int index, T ertek)
        {
            int counter = 1;
            LancElem<T> temp = fej;
            LancElem<T> value;
            if (fej == null || index == 0)
            {
                value = new LancElem<T>(ertek, fej);
                fej = value;
            }
            else
            {
                while (temp.kov != null && counter < index)
                {
                    temp = temp.kov;
                    counter++;
                }
                if (counter <= index)
                {
                    value = new LancElem<T>(ertek, temp.kov);
                    temp.kov = value;
                }
                else
                {
                    throw new HibasIndexKivetel();

                }
            }
            

        }

        public void Torol(T ertek)
        {
            LancElem<T> temp = fej;
            LancElem<T> e = null;

            do
            {
                while (temp != null && !temp.tart.Equals(ertek))
                {
                    e = temp;
                    temp = temp.kov;
                }
                if (temp != null)
                {
                    LancElem<T> q = temp.kov;
                    if (e == null)
                    {
                        fej = q;
                    }
                    else
                    {
                        e.kov = q;
                    }

                    temp = q;
                }
            } while (temp != null);


        }

        public void Bejar(Action<T> muvelet)
        {
            LancElem<T> temp = fej;
            while (temp != null)
            {
                muvelet?.Invoke(temp.tart);
                temp = temp.kov;

            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            LancoltListaBejaro<T> bejaro = new LancoltListaBejaro<T>(fej);
            return bejaro;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public class LancoltListaBejaro<T> : IEnumerator<T>
    {
        LancElem<T> aktualisElem;
        LancElem<T> fej;
        public T Current
        {
            get
            {
                return aktualisElem.tart;
            }
        }
        public LancoltListaBejaro(LancElem<T> fej)
        {
            this.fej = fej;
            aktualisElem = null;
        }
        object IEnumerator.Current => aktualisElem;

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            if (aktualisElem == null)
            {
                aktualisElem = fej;
            }
            else
            {
                aktualisElem = aktualisElem.kov;
            }
            return aktualisElem != null;
        }

        public void Reset()
        {
            aktualisElem = null;
        }
    }
}
