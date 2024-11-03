using System;

namespace DomainHexagonal.Utilities
{
    // Either Class
    public abstract class Either<L, R> 
    {
        public abstract TResult Match<TResult>(Func<L, TResult> LEFT, Func<R, TResult> RIGHT);

        public static Either<L, R> FromLeft(L left) => new Left<L, R>(left);
        public static Either<L, R> FromRight(R right) => new Right<L, R>(right);

        private class Left<L, R> : Either<L, R>
        {
            public L Value { get; }
            public Left(L value) => Value = value;
            public override TResult Match<TResult>(Func<L, TResult> leftFunc, Func<R, TResult> rightFunc) => leftFunc(Value);
        }

        private class Right<L, R> : Either<L, R>
        {
            public R Value { get; }
            public Right(R value) => Value = value;
            public override TResult Match<TResult>(Func<L, TResult> leftFunc, Func<R, TResult> rightFunc) => rightFunc(Value);
        }


        //public L Left { get; }
        //public R Right { get; }
        //public bool IsLeft { get; }
        //public bool IsRight => !IsLeft;

        //private Either(L left)
        //{
        //    Left = left;
        //    IsLeft = true;
        //}

        //private Either(R right)
        //{
        //    Right = right;
        //    IsLeft = false;
        //}

        //public static Either<L, R> FromLeft(L left) => new Either<L, R>(left);
        //public static Either<L, R> FromRight(R right) => new Either<L, R>(right);
    }
}
