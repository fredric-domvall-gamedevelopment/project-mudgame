namespace Infrastructure.Helpers;

public static class ConsoleHelper
{
    public static void Continue()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }
}
