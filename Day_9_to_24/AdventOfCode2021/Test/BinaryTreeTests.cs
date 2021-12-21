using System.Diagnostics;
using Utilities;
using Xunit;

namespace Test
{
    public class BinaryTreeTests
    {
        [Theory]
        [InlineData(20, 30, 40, 20)]
        [InlineData(30, 40, 20, 30)]
        [InlineData(40, 30, 20, 40)]
        public void TestRoot(int v1, int v2, int v3, int expectedRootValue)
        {
            BinaryTree tree = new();
            tree.Add(v1);
            tree.Add(v2);
            tree.Add(v3);
            Debug.WriteLine($"tree.Root: {tree.Root}");
            Assert.Equal(expectedRootValue, tree.Root.Data);
        }
    }
}