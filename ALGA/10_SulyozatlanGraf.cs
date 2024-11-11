using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{

    public class EgeszGrafEl : GrafEl<int>, IComparable<EgeszGrafEl>
    {
        public int Honnan { get; }

        public int Hova { get; }

        public EgeszGrafEl(int honnan, int hova)
        {
            Honnan = honnan;
            Hova = hova;
        }

        public int CompareTo(EgeszGrafEl obj)
        {
            if (this.Honnan != obj.Honnan)
            {
                return (this.Honnan>obj.Honnan) ? 1 : -1;
            }
            if (this.Honnan == obj.Honnan)
            {
                if (this.Hova == obj.Hova)
                {
                    return 0;
                }
                return (this.Hova > obj.Hova) ? 1 : -1;
            }
            else
            {
                return 0;
            }

        }
    }
    public class CsucsmatrixSulyozatlanEgeszGraf : SulyozatlanGraf<int, EgeszGrafEl>
    {
        int n; //number of nodes
        bool[,] M;

        public CsucsmatrixSulyozatlanEgeszGraf(int n)
        {
            this.n = n;
            M = new bool[n, n];
        }

        public int CsucsokSzama { get { return n; } }

        public int ElekSzama
        {
            get
            {
                int count = 0;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (M[i,j])
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
                FaHalmaz<int> fa = new FaHalmaz<int>();
                for (int i = 0; i < n; i++)
                {
                    fa.Beszur(i);
                }
                return fa;
            }
        }

        public Halmaz<EgeszGrafEl> Elek
        {
            get
            {
                FaHalmaz<EgeszGrafEl> fa = new FaHalmaz<EgeszGrafEl>();
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (M[i, j])
                        {
                            fa.Beszur(new EgeszGrafEl(i,j));
                        }
                    }
                }
                return fa;
            }
        }

        public Halmaz<int> Szomszedai(int csucs)
        {
            FaHalmaz<int> fa = new FaHalmaz<int>();

            for (int i = 0; i < n; i++)
            {
                if (M[csucs,i])
                {
                    fa.Beszur(i);
                }
            }
            return fa;
        }

        public void UjEl(int honnan, int hova)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    M[honnan, hova] = true;
                }
            }
        }

        public bool VezetEl(int honnan, int hova)
        {
            return M[honnan, hova];
        }
    }

    public class GrafBejarasok
    {
        public static Halmaz<V> SzelessegiBejaras<V,E>(Graf<V,E> g, V start, Action<V> muvelet ) where V : IComparable<V>
        {
            Sor<V> sor = new LancoltSor<V>();
            Halmaz<V> halmaz = new FaHalmaz<V>();
            sor.Sorba(start);
            halmaz.Beszur(start);

            while (!sor.Ures)
            {
                V k = sor.Sorbol();
                muvelet(k);
                g.Szomszedai(k).Bejar((x) =>
                {
                    if (!halmaz.Eleme(x))
                    {
                        sor.Sorba(x);
                        halmaz.Beszur(x);
                    }
                });
            }
            return halmaz;
        }

        public static Halmaz<V> MelysegiBejaras<V,E>(Graf<V, E> g, V start, Action<V> muvelet) where V : IComparable<V>
        {
            Halmaz<V> F = new FaHalmaz<V>();
            MelysegiBejarasRekurzio(g, start,ref F, muvelet);
            return F;
        }
        private static void MelysegiBejarasRekurzio<V,E>(Graf<V, E> g, V k, ref  Halmaz<V>F, Action<V> muvelet)
        {
            F.Beszur(k);
            muvelet(k);
            Halmaz<V> refF = F;
            g.Szomszedai(k).Bejar((x) =>
            {
                if (!refF.Eleme(x))
                {
                    MelysegiBejarasRekurzio(g, x, ref refF, muvelet);
                }
            });
        }
    }
}
