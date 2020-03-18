using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Values
{
	struct V
	{
		public static V Zero = new V();
		public static V XBasis = new V(1, 0);
		public static V YBasis = new V(0, 1);

		public int X, Y;
		public V(int x, int y) { X = x; Y = y; }

		public static V operator +(V v) => v;
		public static V operator -(V v) => new V(-v.X, -v.Y);
		public static V operator +(V v1, V v2) => new V(v1.X + v2.X, v1.Y + v2.Y);
		public static V operator -(V v1, V v2) => new V(v1.X - v2.X, v1.Y - v2.Y);

		public static bool operator ==(V v1, V v2) => v1.X == v2.X && v1.Y == v2.Y;
		public static bool operator !=(V v1, V v2) => !(v1 == v2);
		public override bool Equals(object obj) => obj is V && this == (V)obj;
		public override int GetHashCode() => X ^ Y;
		public override string ToString() => $"{X} {Y}";
	}

	[TestClass]
	public class VectorTest
	{
	}
}
