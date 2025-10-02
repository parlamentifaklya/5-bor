using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

class Bor
{
    private int _darabSzam;
    private int _ar;

    public Bor(int darabSzam, int ar)
    {
        _darabSzam = darabSzam;
        _ar = ar;
    }

    public int DarabSzam { get { return _darabSzam; } }
    public int Ar { get { return _ar; } }
}

class Program
{
    public static void Main(string[] args)
    {
        int evekSzama;
        int.TryParse(Console.ReadLine(), out evekSzama);
        var borok = Enumerable.Range(1, evekSzama).Select(x =>
        {
            var input = Console.ReadLine();
            var parts = input.Split(' ');
            while (true)
            {
                if (parts.Length == 2 && int.TryParse(parts[0], out int darabSzam) && int.TryParse(parts[1], out int ar))
                {
                    return new { Index = x, Bor = new Bor(darabSzam, ar) };
                }
                else
                {
                    throw new ArgumentException("Invalid input format");
                }
            }
        }).ToDictionary(x => x.Index, x => x.Bor);

        //1 feladat
       Console.WriteLine(borok.OrderBy(x => x.Value.DarabSzam).ThenBy(x => x.Key).First().Key);

        //2 feladat
        Console.WriteLine($"{borok.Where(x => x.Value.DarabSzam > 1000).Select(x => x.Value.Ar).DefaultIfEmpty(-1).Max()}");

        //3 feladat
        Console.WriteLine($"{borok.Select(x => x.Value.Ar).Distinct().Count()}");

        //4 feladat
        Func<Dictionary<int, Bor>, List<int>> RekordEvek = borok =>
        borok.OrderBy(x => x.Key).Skip(1)
            .Aggregate(
                new { Max = borok[1].DarabSzam, Lista = new List<int>() },
                (acc, curr) => new
                {
                    Max = Math.Max(acc.Max, curr.Value.DarabSzam),
                    Lista = curr.Value.DarabSzam > acc.Max ? acc.Lista.Append(curr.Key).ToList() : acc.Lista
                }
            ).Lista;
        Console.WriteLine($"{RekordEvek(borok).Count} {string.Join(" ", RekordEvek(borok))}");
    }
}