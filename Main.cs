namespace SeaBattle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Master master = new();
                master.ShowMenu();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}\n" +
                                  $"Источник: {e.Source}");
            }
        }
    }
}
