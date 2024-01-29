using System;
using System.Collections.Generic;
using System.Linq;

namespace CoderLib8.DataTrees.SBTs
{
	public class SBTRollingHash
	{
		const long B = 987654323;
		const long M = 998244353;

		static long MPow(long b, long i)
		{
			long r = 1;
			for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
			return r;
		}
		static long MInv(long x) => MPow(x, M - 2);

		readonly int n;
		readonly MergeSBT<long> st;
		readonly long[] pow, pow_;

		public SBTRollingHash(string s, long b = B)
		{
			n = s.Length;
			st = new MergeSBT<long>(n, Monoid.Int64_Add);

			pow = new long[n + 1];
			pow_ = new long[n + 1];
			pow[0] = 1;
			pow_[0] = 1;
			var binv = MInv(b);

			for (int i = 0; i < n; ++i)
			{
				pow[i + 1] = pow[i] * b % M;
				pow_[i + 1] = pow_[i] * binv % M;
				this[i] = s[i];
			}
		}

		public char this[int i]
		{
			set => st[i] = value * pow[i] % M;
		}

		public long Hash(int l, int r) => st[l, r] % M * pow_[l] % M;
	}
}
