using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainHexagonal.Utilities
{
    public static class EitherExtensions
    {
        // Applies a mapping function on the 'Right' value if present
        public static Either<L, U> Map<L, T, U>(this Either<L, T> either, Func<T, U> map)
        {
            return either.IsRight ? Either<L, U>.FromRight(map(either.Right)) : Either<L, U>.FromLeft(either.Left);
        }

        // Matches and performs an action based on whether Either is Left or Right
        public static void Match<L, R>(this Either<L, R> either, Action<L> leftAction, Action<R> rightAction)
        {
            if (either.IsLeft)
                leftAction(either.Left);
            else
                rightAction(either.Right);
        }
    }
}
