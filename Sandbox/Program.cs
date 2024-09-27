namespace OE.ALGA.Sandbox
{
    internal class Program
    {
        public static int[] E = new int[1];
        public static int n = 0;

        static void Main()
        {
            hozzafuz(1);
            hozzafuz(3);
            hozzafuz(12);
            hozzafuz(3);
            hozzafuz(2);

            torol(3);
            //beszur(4, 76775);
            for (int i = 0; i < E.Length; i++)
            {
                Console.WriteLine(E[i]);
            }

        }
        public static void beszur(int index, int ertek)
        {
            if (index > n || index < 0)
            {
                //throw new HibasIndexKivetel();
                Console.WriteLine("Nem megyen");
            }
            if (++n >= E.Length)
            {
                int[] temp = new int[E.Length * 2];
                for (int i = 0; i < E.Length; i++)
                {
                    temp[i] = E[i];
                }
                E = temp;
                
                //n++;
            }
            for (int i = n; i > index; i--)
            {
                E[i] = E[i - 1];
            }
            E[index] = ertek;


        }
        public static void hozzafuz(int ertek)
        {
            beszur(n, ertek);
        }
        public static void torol(int ertek)
        {
            int db = 0;
            for (int i = 0; i < E.Length; i++)
            {
                write(i-db, i);
                Console.WriteLine();
                E[i - db] = E[i];
                if (E[i].Equals(ertek))
                {
                    db++;
                }
            }
            n -= db;
        }

        public static void write(int db, int index)
        {
            for (int i = 0; i < E.Length; i++)
            {
                if (i == db)
                {
                    Console.ForegroundColor = ConsoleColor.Green;

                }
                if (i == index)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.Write(E[i] + "  ");
                Console.ForegroundColor = ConsoleColor.White;

            }
        }
    }
}
