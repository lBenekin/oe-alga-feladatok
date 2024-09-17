namespace OE.ALGA.Sandbox
{
    internal class Program
    {
        static void Main()
        {
            List<int> a = new List<int> { 1, 2, 3, 13, 27, 6, 7, 8, 9, 10 };
            IEnumerator<int> enumerator = a.GetEnumerator();

            while (enumerator.MoveNext())
            {

            }
        }
    }
}
