namespace OE.ALGA.Sandbox
{
    internal class Program
    {
        static void Main()
        {
            DateTime a = DateTime.Parse("2024-09-01T10:00:00Z");
            DateTime b = DateTime.Parse("2024-09-01");
            Console.WriteLine(a.Date == b);

        }

    }
}
