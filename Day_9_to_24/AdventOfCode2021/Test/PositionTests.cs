using System.Collections.Generic;
using Utilities;
using Xunit;

namespace Test
{
    public class PositionTests
    {
        [Fact]
        public void FindEquivalentInSet()
        {
            Position pos = new(2, 3);
            HashSet<Position> set = new();
            set.Add(pos);
            bool found1 = set.Contains(pos);
            Assert.True(found1);
            bool found2 = set.Contains(new(2, 3));
            Assert.True(found2);
        }
    }
}