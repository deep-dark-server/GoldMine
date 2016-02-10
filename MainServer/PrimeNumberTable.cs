using GoldMine.ServerBase.Init;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldMine.MainServer
{
    /// <summary>
    /// Maintains List of prime numbers which is less than or equals to Max
    /// Has some utility methods like PickRandom()
    /// </summary>
    [PostAppInit]
    public static class PrimeNumberTable
    {
        private const int Max = 20000000;
        private static readonly List<int> PrimeNumbers = new List<int>();
        private static readonly Random Random = new Random();

        public static void PostAppInit()
        {
            Console.WriteLine($"WarmUp: PrimeNumberTable initialize - picked {PickOne()}");
        }

        static PrimeNumberTable()
        {
            GeneratePrimeNumberSet();
        }

        public static bool IsPrime(int i) => (i >= 2 && PrimeNumbers.BinarySearch(i) >= 0);

        public static int PickOne() => (PrimeNumbers[Random.Next(PrimeNumbers.Count)]);

        public static IEnumerable<int> PickN(int n)
        {
            if (n > PrimeNumbers.Count)
                throw new ArgumentOutOfRangeException(nameof(n), "Cannot pick prime numbers exceeding total count of prime numbers <= int max");

            var pickedNumbers = new HashSet<int>();
            while (pickedNumbers.Count < n)
                pickedNumbers.Add(PickOne());

            return pickedNumbers;
        }

        private static void GeneratePrimeNumberSet()
        {
            var nonPrimeNumbers = new HashSet<int>();

            // Algorithm hint from https://en.wikipedia.org/wiki/Sieve_of_Eratosthenes
            for (var i = 2; i <= (int)Math.Sqrt(Max); i++)
            {
                if (nonPrimeNumbers.Contains(i))
                    continue;

                for (var j = i * i; j <= Max; j += i)
                    nonPrimeNumbers.Add(j);
            }

            for (var i = 2; i < Max; i++)
                if (!nonPrimeNumbers.Contains(i))
                    PrimeNumbers.Add(i);
        }

        public static bool IsPrimeNumberSetValid =>
            !(from pn in PrimeNumbers.AsParallel() from n in Enumerable.Range(2, pn - 2) where pn % n == 0 select pn).Any();
    }
}