using System;

namespace CoderLib6.Collections
{
	// いもす法
	class StaticRAQ
	{
		long[] d;
		public StaticRAQ(int n) { d = new long[n]; }

		// O(1)
		// l_in < 0, r_ex >= n も可。
		public void Add(int l_in, int r_ex, long v)
		{
			d[Math.Max(0, l_in)] += v;
			if (r_ex < d.Length) d[r_ex] -= v;
		}

		// O(n)
		public long[] GetAll()
		{
			var a = new long[d.Length];
			a[0] = d[0];
			for (int i = 1; i < d.Length; ++i) a[i] = a[i - 1] + d[i];
			return a;
		}
	}
}
