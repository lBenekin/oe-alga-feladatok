using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{
    public class FaElem<T> where T : IComparable<T>
    {
        public T tart {  get; set; }
        public FaElem<T> bal {  get; set; }
        public FaElem<T> jobb {  get; set; }

        public FaElem(T tart, FaElem<T> bal, FaElem<T> jobb)
        {
            this.tart = tart;
            this.bal = bal;
            this.jobb = jobb;
        }
    }

    public class FaHalmaz<T> : Halmaz<T> where T : IComparable<T>
    {
        public FaElem<T> gyoker { get; set; }

        public void Bejar(Action<T> muvelet)
        {
            ReszfaBejarasPreOrder(gyoker,muvelet);
        }

        public void Beszur(T ertek)
        {
            gyoker = ReszfabaBeszur(gyoker, ertek);
        }

        public bool Eleme(T ertek)
        {
            return ReszfaEleme(gyoker, ertek);
        }

        public void Torol(T ertek)
        {
            gyoker = ReszfabolTorol(gyoker,ertek);
        }
        protected static FaElem<T> ReszfabolTorol(FaElem<T> p, T ertek)
        {

            if (p != null)
            {
                if (p.tart.CompareTo(ertek) > 0)
                {
                    p.bal = ReszfabolTorol(p.bal, ertek);
                }
                else
                {
                    if (p.tart.CompareTo(ertek) < 0)
                    {
                        p.jobb = ReszfabolTorol(p.jobb,ertek);
                    }
                    else
                    {
                        if (p.bal == null)
                        {
                            p = p.jobb;
                        }
                        else
                        {
                            if (p.jobb == null)
                            {
                                p = p.bal;
                            }
                            else
                            {
                                p.bal =  KetGyerekesTorles(p,p.bal);
                            }
                        }
                    }
                }
                return p;
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }
        protected static FaElem<T> KetGyerekesTorles(FaElem<T> e, FaElem<T> p)
        {
            if (p.jobb != null)
            {
                p.jobb  = KetGyerekesTorles(e,p.jobb);
                return p;
            }
            e.tart = p.tart;
            return p.bal;
        }
        protected static FaElem<T> ReszfabaBeszur(FaElem<T> p, T ertek)
        {
            if (p == null)
            {
                FaElem<T> temp = new FaElem<T>(ertek, null, null);
                return temp;
            }
            if (p.tart.CompareTo(ertek) > 0)    
            {
                p.bal = ReszfabaBeszur(p.bal, ertek);
            }
            else
            {
                if (p.tart.CompareTo(ertek) < 0)
                {
                    p.jobb = ReszfabaBeszur(p.jobb, ertek);
                }
            }
            return p;

        }
        protected static bool ReszfaEleme(FaElem<T> p, T ertek)
        {
            if (p != null)
            {
                if (p.tart.CompareTo(ertek) > 0)
                {
                    return ReszfaEleme(p.bal, ertek);
                }
                else if (p.tart.CompareTo(ertek) < 0)
                {
                    return ReszfaEleme(p.jobb, ertek);  
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
        protected void ReszfaBejarasPreOrder(FaElem<T> p, Action<T> muvelet)
        {
            if (p != null)
            {
                muvelet?.Invoke(p.tart);
                ReszfaBejarasPreOrder(p.bal,muvelet);
                ReszfaBejarasPreOrder(p.jobb, muvelet);
            }
        }
        protected void ReszfaBejarasInOrder(FaElem<T> p, Action<T> muvelet)
        {
            if (p != null)
            {
                ReszfaBejarasPreOrder(p.bal, muvelet);
                muvelet?.Invoke(p.tart);
                ReszfaBejarasPreOrder(p.jobb, muvelet);
            }
        }
        protected void ReszfaBejarasPostOrder(FaElem<T> p, Action<T> muvelet)
        {
            if (p != null)
            {
                ReszfaBejarasPreOrder(p.bal, muvelet);
                ReszfaBejarasPreOrder(p.jobb, muvelet);
                muvelet?.Invoke(p.tart);
            }
        }
    }
}
