using System;

namespace DomainHexagonal.Utilities
{
    // Either Class
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
}
