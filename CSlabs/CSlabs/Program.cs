using static CSlabs.Labs.Lab2;

class Program2
{
    static void Main(string[] args)
    {
        Console.WriteLine("Cat and Mouse Game ");
        Console.WriteLine("=========================================");
        Console.WriteLine();


        string inputFile = @"C:\Users\user\source\repos\vladshmigero\CSlabs\CSlabs\CSlabs\ChaseData.txt";
        string outputFile = @"C:\Users\user\source\repos\vladshmigero\CSlabs\CSlabs\CSlabs\PursuitLog.txt";
        try
        {
            Game game = new Game();
            Console.WriteLine($"Готово! Результат в {outputFile}");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}