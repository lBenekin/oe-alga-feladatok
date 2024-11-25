using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{
    public class SulyozottEgeszGrafEl : EgeszGrafEl, SulyozottGrafEl<int>
    {
        public float Suly { get; set; }
        public SulyozottEgeszGrafEl(int honnan, int hova, float suly) : base(honnan, hova)
        {
            this.Suly = suly;
        }

    }
    public class CsucsmatrixSulyozottEgeszGraf : SulyozottGraf<int, SulyozottEgeszGrafEl>
    {
        int n;
        float[,] M;

        public CsucsmatrixSulyozottEgeszGraf(int n)
        {
            this.n = n;
            M = new float[n, n];
        }

        public int CsucsokSzama
        {
            get { return n; }
        }

        public int ElekSzama
        {
            get
            {
                int count = 0;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (M[i,j] != null)
                        {
                            count++;
                        }
                    }
                }
                return count;
            }
        }

        public Halmaz<int> Csucsok
        {
            get
            {
                FaHalmaz<int> faHalmaz = new FaHalmaz<int>();
                for (int i = 0; i < n; i++)
                {
                    faHalmaz.Beszur(i);
                }
                return faHalmaz;
            }
        }

        public Halmaz<SulyozottEgeszGrafEl> Elek
        {
            get
            {
                FaHalmaz<SulyozottEgeszGrafEl> faHalmaz = new FaHalmaz<SulyozottEgeszGrafEl> ();

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (M[i, j] != null)
                        {
                            SulyozottEgeszGrafEl grafEl = new SulyozottEgeszGrafEl(i,j, M[i,j]);
                            faHalmaz.Beszur(grafEl);
                        }
                    }
                }
                return faHalmaz;
            }
        }

        public float Suly(int honnan, int hova)
        {
            if (M[honnan,hova] == null)
            {
                throw new NincsElemKivetel();
            }
            return M[honnan, hova];

        }

        public Halmaz<int> Szomszedai(int csucs)
        {
            FaHalmaz<int> faHalmaz = new FaHalmaz<int>();
            for (int j = 0; j < n; j++)
            {
                if (M[csucs,j] != null)
                {
                    faHalmaz.Beszur(j);
                }
            }
            return faHalmaz;
        }

        public void UjEl(int honnan, int hova, float suly)
        {
            M[honnan, hova] = suly;
        }

        public bool VezetEl(int honnan, int hova)
        {
            return M[honnan, hova] != null;
        }
    }

    public class Utkereses
    {
        static void Dijkstra<V,E>(SulyozatlanGraf<V,E> g, V start)
        {
            /*
            HasitoSzotarTulcsordulasiTerulettel<V, float> L = new HasitoSzotarTulcsordulasiTerulettel<V, float>(g.CsucsokSzama);
            HasitoSzotarTulcsordulasiTerulettel<V, V> P = new HasitoSzotarTulcsordulasiTerulettel<V, V>(g.ElekSzama);
            KupacPrioritasosSor<V> S = new KupacPrioritasosSor<V>(g.ElekSzama, (x, y) => L.Kiolvas(x).CompareTo(P.Kiolvas(y))>=0);
            g.Csucsok.Bejar(x =>
            {
                L.Beir(x,float.MaxValue);
                P.Beir(x, default(V));
                S.Sorba(x);
            });
            L.Beir(start, 0);
            while (!S.Ures)
            {
                V u = S.Elso();
                g.Szomszedai(u).Bejar(x =>
                {
                    if (L.Kiolvas(u))
                    {

                    }
                }
            }*/
        }
    }
}
