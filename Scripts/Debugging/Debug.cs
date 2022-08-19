public static class Debug
{
    public static void Log(string message)
    {
        System.Console.WriteLine(message);
    }

    public static void LogWarning(string message)
    {
        System.Console.ForegroundColor = System.ConsoleColor.Yellow;
        System.Console.WriteLine(message);
        System.Console.ForegroundColor = System.ConsoleColor.White;
    }

    public static void LogError(string message)
    {
        System.Console.ForegroundColor = System.ConsoleColor.Red;
        System.Console.WriteLine(message);
        System.Console.ForegroundColor = System.ConsoleColor.White;
    }
}