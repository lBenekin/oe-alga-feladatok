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
                        if (M[i,j] != default)
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
                        if (M[i, j] != default)
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
            if (M[honnan,hova] == default)
            {
                throw new NincsElKivetel();
            }
            return M[honnan, hova];

        }

        public Halmaz<int> Szomszedai(int csucs)
        {
            FaHalmaz<int> faHalmaz = new FaHalmaz<int>();
            for (int j = 0; j < n; j++)
            {
                if (M[csucs,j] != default)
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
            return M[honnan,hova] != default;
        }
    }

    public class Utkereses
    {
        public static HasitoSzotarTulcsordulasiTerulettel<V,float> Dijkstra<V,E> (SulyozottGraf<V,E> g, V start)
        {
            
            HasitoSzotarTulcsordulasiTerulettel<V, float> L = new HasitoSzotarTulcsordulasiTerulettel<V, float>(g.CsucsokSzama);
            HasitoSzotarTulcsordulasiTerulettel<V, V> P = new HasitoSzotarTulcsordulasiTerulettel<V, V>(g.ElekSzama);
            //
            KupacPrioritasosSor<V> S = new KupacPrioritasosSor<V>(g.CsucsokSzama, (x, y) => L.Kiolvas(x) < L.Kiolvas(y));
            g.Csucsok.Bejar(x =>
            {
                L.Beir(x,float.MaxValue);
                S.Sorba(x);

            });
            L.Beir(start, 0);
            S.Frissit(start);
            while (!S.Ures)
            {
                V u = S.Sorbol();
                g.Szomszedai(u).Bejar(x =>
                {
                    if (L.Kiolvas(u)+g.Suly(u,x) < L.Kiolvas(x))
                    {
                        L.Beir(x, L.Kiolvas(u)+g.Suly(u,x));
                        P.Beir(x, u);
                    }
                });
            }
            return L;
        }
    }


    public class FeszitofaKereses
    {
        public static Szotar<V,V> Prim<V,E>(SulyozottGraf<V,E> g, V start) where V : IComparable<V>
        {
            HasitoSzotarTulcsordulasiTerulettel<V, float> K = new HasitoSzotarTulcsordulasiTerulettel<V, float>(g.CsucsokSzama);
            HasitoSzotarTulcsordulasiTerulettel<V, V> P = new HasitoSzotarTulcsordulasiTerulettel<V, V>(g.ElekSzama);
            FaHalmaz<V> halmaz = new FaHalmaz<V>();
            KupacPrioritasosSor<V> S = new KupacPrioritasosSor<V>(g.ElekSzama, (x, y) => K.Kiolvas(x) < K.Kiolvas(y));
           
            g.Csucsok.Bejar(x =>
            {
                K.Beir(x, float.MaxValue);
                P.Beir(x, default(V));
                S.Sorba(x);
                halmaz.Beszur(x);
            });

            K.Beir(start, 0);
            S.Frissit(start);

            while (!S.Ures)
            {
                V u = S.Sorbol();
                halmaz.Torol(u);

                g.Szomszedai(u).Bejar(x =>
                {
                    if (halmaz.Eleme(x) && g.Suly(u, x) < K.Kiolvas(x))
                    {
                        K.Beir(x, g.Suly(u, x));
                        S.Frissit(x);
                        P.Beir(x, u);
                    }
                });
            }
            return P;
        }

        public static Halmaz<E> Kruskal<V,E>(SulyozottGraf<V,E> g) where E : SulyozottGrafEl<V>, IComparable<E>
        {
            Halmaz<E> A = new FaHalmaz<E>();
            Szotar<V, int> halmazok = new HasitoSzotarTulcsordulasiTerulettel<V, int>(g.CsucsokSzama);
            KupacPrioritasosSor<E> S = new KupacPrioritasosSor<E>(g.ElekSzama, (x, y) => x.Suly < y.Suly);
            //Halmaz létrehozás
            int i = 0;
            g.Csucsok.Bejar(x => { halmazok.Beir(x, i++); });

            g.Elek.Bejar(e =>
            {
                S.Sorba(e);
                S.Frissit(e);
            });

            while (!S.Ures)
            {
                E minEdge = S.Sorbol();

                //u Honnan
                //v Hova
                if (halmazok.Kiolvas(minEdge.Honnan) != halmazok.Kiolvas(minEdge.Hova))
                {
                    A.Beszur(minEdge);
                    //Halmazösszevonás
                    int id = halmazok.Kiolvas(minEdge.Hova);
                    g.Csucsok.Bejar(x =>
                    {
                        //a - 0
                        //b - 1
                        //c - 2

                        if (halmazok.Kiolvas(x) == id)
                        {
                            halmazok.Beir(x, halmazok.Kiolvas(minEdge.Honnan));
                        }
                    });
                }
            }
            return A;
        }
    }
}
