using System;
using System.Collections.Generic;

namespace Utilities
{
    public static class Algorithm
    {
        /// <summary>
        /// Tries all operations in order until the first succeeds.
        /// </summary>
        /// <typeparam name="T">
        /// Class type, given by reference to be modified.
        /// </typeparam>
        /// <param name="item">The parameter to modify.</param>
        /// <param name="operations">
        /// The operations that modify <paramref name="item" />. Return
        /// true if the operation succeeded.
        /// </param>
        /// <returns><![CDATA[true]]> , if any operation succeeded</returns>
        public static bool TryOperations<T>(T item, List<Func<T, bool>> operations) where T : class
        {
            foreach (var operation in operations)
            {
                if (operation(item))
                    return true;
            }
            return false;
        }
    }
}