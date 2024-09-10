 using System;
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
    public class FeladatTarolo<T> where T : IVegrehajthato
    {
        T[] tarolo;
        int n = 0; //number of elements

        public FeladatTarolo(int lenght)
        {
            this.tarolo = new T[lenght];
        }

        public void Felvesz(T element)
        {
            if (n < tarolo.Length)
            {
                tarolo[n] = element;
                n++;
            }
            else
            {
                throw new TaroloMegteltKivetel();
            }
        }

        public void MindentVegrehajt()
        {
            for (int i = 0; i < tarolo.Length; i++)
            {
                tarolo[i].Vegrehajtas();
            }
        }
    }
    public class FuggoFeladatTarolo<T> where T : FeladatTarolo<T>, IVegrehajthato, IFuggo
    {
        //Todo: override
    }

    public class TaroloMegteltKivetel : Exception
    {

    }
}
