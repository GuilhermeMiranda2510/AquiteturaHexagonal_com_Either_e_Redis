using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainHexagonal.Utilities
{
    public class Either<L, R>
    {
        public L Left { get; }
        public R Right { get; }
        public bool IsLeft { get; }
        public bool IsRight => !IsLeft;

        private Either(L left)
        {
            Left = left;
            IsLeft = true;
        }

        private Either(R right)
        {
            Right = right;
            IsLeft = false;
        }

        public static Either<L, R> FromLeft(L left) => new Either<L, R>(left);
        public static Either<L, R> FromRight(R right) => new Either<L, R>(right);
    }

    //Para tornar o uso mais fluido, você pode adicionar métodos de extensão para mapear e manipular o valor de sucesso ou o erro sem necessidade de verificações explícitas.
    //public static class EitherExtensions
    //{
    //    public static Either<L, U> Map<L, T, U>(this Either<L, T> either, Func<T, U> map)
    //    {
    //        return either.IsRight ? Either<L, U>.FromRight(map(either.Right)) : Either<L, U>.FromLeft(either.Left);
    //    }

    //    public static void Match<L, R>(this Either<L, R> either, Action<L> leftAction, Action<R> rightAction)
    //    {
    //        if (either.IsLeft)
    //            leftAction(either.Left);
    //        else
    //            rightAction(either.Right);
    //    }
    //}
}
