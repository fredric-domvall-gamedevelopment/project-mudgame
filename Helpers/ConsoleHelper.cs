namespace MUD.Helpers;
public static class ConsoleHelper
{
    public static void Continue()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }
}
