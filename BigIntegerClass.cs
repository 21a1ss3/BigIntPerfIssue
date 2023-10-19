using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Text;

namespace ConcurrentCollectionsPerfTest
{
    public class BigIntegerClass : IComparable<BigIntegerClass>, IComparable<BigInteger>, IComparable
    {
        public BigInteger Number { get; private set; }

        public int CompareTo([AllowNull] BigInteger other)
        {
            return Number.CompareTo(other);
        }

        public int CompareTo([AllowNull] BigIntegerClass other)
        {
            return Number.CompareTo(other.Number);
        }

        public int CompareTo(object obj)
        {
            if (obj is BigInteger)
                return CompareTo((BigInteger)obj);

            return CompareTo(obj as BigIntegerClass);
        }

        public static implicit operator BigInteger(BigIntegerClass c) => c.Number;
        public static implicit operator BigIntegerClass(BigInteger num) => new BigIntegerClass() { Number = num };
    }
}
