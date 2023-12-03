﻿namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await PerformTask();
            Console.WriteLine($"Ciekawe");
        }

        public static async Task<int> DoSomethingAsync()
        {
            // Symulacja operacji asynchronicznej (np. pobieranie danych z sieci)
            await Task.Delay(3000); // Oczekiwanie przez 1 sekundę (bez blokowania wątku)

            return 42; // Zwracanie wyniku jako wartość int
        }

        public static async Task PerformTask()
        {
            Console.WriteLine("Rozpoczęcie operacji");

            var resultTask = DoSomethingAsync(); // Wywołanie metody asynchronicznej i oczekiwanie na jej zakończenie
            
            await Task.Delay(3000);
            Console.WriteLine($"Ciekawe czy skonczyl");
            Console.Out.WriteLine(await resultTask);
        }
    }
}