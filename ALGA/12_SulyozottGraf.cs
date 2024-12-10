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
            return !float.IsNormal(M[honnan,hova]);
        }
    }

    public class Utkereses
    {
        public static HasitoSzotarTulcsordulasiTerulettel<V,float> Dijkstra<V,E> (SulyozottGraf<V,E> g, V start)
        {
            
            HasitoSzotarTulcsordulasiTerulettel<V, float> L = new HasitoSzotarTulcsordulasiTerulettel<V, float>(g.CsucsokSzama);
            HasitoSzotarTulcsordulasiTerulettel<V, V> P = new HasitoSzotarTulcsordulasiTerulettel<V, V>(g.ElekSzama);
            //
            KupacPrioritasosSor<V> S = new KupacPrioritasosSor<V>(g.ElekSzama, (x, y) => L.Kiolvas(x).CompareTo(L.Kiolvas(y))>=0);
            g.Csucsok.Bejar(x =>
            {
                L.Beir(x,float.MaxValue);
                P.Beir(x, default(V));
                S.Sorba(x);

            });
            L.Beir(start, 0);
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
        public static Szotar<V,V> Prim<V,E>(SulyozottGraf<V,E> g, V start)
        {
            /*HasitoSzotarTulcsordulasiTerulettel<V, V> K = new HasitoSzotarTulcsordulasiTerulettel<V, V>(g.CsucsokSzama);
            HasitoSzotarTulcsordulasiTerulettel<V, V> P = new HasitoSzotarTulcsordulasiTerulettel<V, V>(g.ElekSzama);
            KupacPrioritasosSor<V> S = new KupacPrioritasosSor<V>(g.ElekSzama, (x, y) => K.Kiolvas(x).CompareTo(P.Kiolvas(y)) >= 0);
            g.Csucsok.Bejar(x =>
            {
                K.Beir(x, );
                P.Beir(x, default(V));
                S.Sorba(x);
            });
            K.Beir(start, 0);
            S.Frissit();
            while (!S.Ures)
            {
                V u = S.Elso();
                g.Szomszedai(u).Bejar(x =>
                {
                    if (K.Kiolvas(u) + g.Suly(u, x) < K.Kiolvas(x))
                    {
                        K.Beir(x, K.Kiolvas(u) + g.Suly(u, x));
                        S.Frissit();
                        P.Beir(x, u);
                    }
                });
            }
            return K;*/
            return new HasitoSzotarTulcsordulasiTerulettel<V,V>(g.ElekSzama);
        }

        public static Halmaz<E> Kruskal<V,E>(SulyozottGraf<V,E> g) where E : SulyozottGrafEl<V>, IComparable<E>
        {
            Halmaz<E> A = new FaHalmaz<E>();
            Szotar<V, int> halmazok = new HasitoSzotarTulcsordulasiTerulettel<V, int>(g.CsucsokSzama);
            KupacPrioritasosSor<E> S = new KupacPrioritasosSor<E>(g.ElekSzama, (x, y) => x.CompareTo(y) >= 0);
            //Halmaz létrehozás
            int i = 0;
            g.Csucsok.Bejar(x => { halmazok.Beir(x, i++); });

            g.Elek.Bejar(e =>
            {
                S.Sorba(e);


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
                    g.Csucsok.Bejar(x =>
                    {
                        if (halmazok.Kiolvas(x) == halmazok.Kiolvas(minEdge.Honnan))
                        {
                            halmazok.Beir(x, halmazok.Kiolvas(minEdge.Hova));
                        }
                    });
                }
            }
            return A;
        }
    }
}
