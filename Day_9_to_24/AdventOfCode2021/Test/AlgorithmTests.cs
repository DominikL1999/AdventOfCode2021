using System.Collections.Generic;
using Utilities;
using Xunit;

namespace Test
{
    public class AlgorithmTests
    {
        [Fact]
        public void TryOperationsTest1()
        {
            List<int> ints = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            bool changed = Algorithm.TryOperations(ints, new() { Remove20s });
            Assert.False(changed);
            Assert.Contains(10, ints);
        }

        [Fact]
        public void TryOperationsTest2()
        {
            List<int> ints = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            bool changed = Algorithm.TryOperations(ints, new() { Remove10s });
            Assert.True(changed);
            Assert.DoesNotContain(10, ints);
        }

        [Fact]
        public void TryOperationsTest3()
        {
            List<int> ints = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            bool changed = Algorithm.TryOperations(ints, new() { Remove20s, Remove10s });
            Assert.True(changed);
            Assert.DoesNotContain(10, ints);
        }

        private bool Remove10s(List<int> ints)
        {
            return ints.Remove(10);
        }

        private bool Remove20s(List<int> ints)
        {
            return ints.Remove(20);
        }
    }
}