using System.Collections.Generic;
using GoldMine.ServerBase.Init;

namespace GoldMine.MainServer
{
    /// <summary>
    /// Maintains List of prime numbers below int.MAX
    /// Has some utility methods like PickRandom()
    /// </summary>
    [PostAppInit]
    public class PrimeNumberTable
    {
        private static List<int> primeNumbers = new List<int>();

        static PrimeNumberTable()
        {
            
        }   
    }
}