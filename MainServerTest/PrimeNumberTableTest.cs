using NUnit.Framework;

namespace GoldMine.MainServer.Test
{
    [TestFixture]
    public class PrimeNumberTableTest
    {
        //[Test]
        /// <summary>
        /// This test uses very inefficient method to check validity, 
        /// so pass this test when there was no major modifications.
        /// </summary>
        public void IsPrimeNumbersValid()
        {
            Assert.Equals(true, PrimeNumberTable.IsPrimeNumberSetValid);
        }

        /// <summary>
        /// Testing IsPrime() funcionality - pre-generated prime number table lookup
        /// </summary>
        /// <param name="n"></param>
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(7)]
        [TestCase(11)]
        public void IsPrime(int n)
        {
            Assert.AreEqual(true, PrimeNumberTable.IsPrime(n));
        }
    }
}