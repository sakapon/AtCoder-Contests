using System;

namespace CoderLib6.Values
{
	// 比較には V を使います。
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/7/DPL/5/DPL_5_C
	struct MInt
	{
		//const long M = 1000000007;
		const long M = 998244353;
		public long V;
		public MInt(long v) { V = (v %= M) < 0 ? v + M : v; }
		public override string ToString() => $"{V}";
		public static implicit operator MInt(long v) => new MInt(v);

		public static MInt operator -(MInt x) => -x.V;
		public static MInt operator +(MInt x, MInt y) => x.V + y.V;
		public static MInt operator -(MInt x, MInt y) => x.V - y.V;
		public static MInt operator *(MInt x, MInt y) => x.V * y.V;
		public static MInt operator /(MInt x, MInt y) => x.V * y.Inv().V;

		public static long MPow(long b, long i)
		{
			long r = 1;
			for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
			return r;
		}
		public MInt Pow(long i) => MPow(V, i);
		public MInt Inv() => MPow(V, M - 2);
	}
}
