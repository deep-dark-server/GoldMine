using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
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

        /// <summary>
        /// Test should be performed with threadCount > 1
        /// </summary>
        /// <param name="threadCount"># of threads to perform test with</param>
        [Test]
        [TestCase(5)]
        public void PickOneTest(int threadCount)
        {
            Assert.Greater(threadCount, 1);

            var testBag = new ConcurrentBag<int>();
            var threads = new List<Thread>();
            for (var i = 0; i < threadCount; ++i)
            {
                // ReSharper disable once ObjectCreationAsStatement
                // explicit thread generation needed for proper thread safety check
                var thd = new Thread(() => { testBag.Add(PrimeNumberTable.PickOne()); });
                thd.Start();
                threads.Add(thd);
            }

            foreach (var thd in threads)
                thd.Join();

            int? itemBefore = null;
            Assert.Greater(testBag.Count, 1);

            while (!testBag.IsEmpty)
            {
                int tmp;
                if (itemBefore == null)
                {
                    if (testBag.TryTake(out tmp))
                        itemBefore = tmp;

                    Assert.NotNull(itemBefore);
                }

                int? itemNow = null;
                if (testBag.TryTake(out tmp))
                    itemNow = tmp;
                else
                    return;

                Assert.NotNull(itemNow);
                Assert.AreNotEqual(itemNow, itemBefore);

                itemBefore = itemNow;
            }
        }
    }
}