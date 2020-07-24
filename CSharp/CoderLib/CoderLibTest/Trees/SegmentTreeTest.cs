using System;
using System.Collections.Generic;
using System.Linq;
using KLibrary.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Trees
{
	class ST
	{
		int kMax;
		List<long[]> vs = new List<long[]> { new long[1] };
		public ST(int n)
		{
			for (int c = 1; c < n; vs.Add(new long[c <<= 1])) ;
			kMax = vs.Count - 1;
		}

		(int k, int i)[] GetLevels(int i)
		{
			var r = new List<(int, int)>();
			for (int k = kMax; k >= 0; --k, i >>= 1) r.Add((k, i));
			return r.ToArray();
		}

		(int k, int i)[] GetRange(int minIn, int maxEx)
		{
			var r = new List<(int, int)>();
			for (int k = kMax, f = 1; k >= 0 && minIn < maxEx; --k, f <<= 1)
			{
				if ((minIn & f) != 0) r.Add((k, (minIn += f) / f - 1));
				if ((maxEx & f) != 0) r.Add((k, (maxEx -= f) / f));
			}
			return r.ToArray();
		}

		// 範囲の和を求める場合の例。
		public long Get(int i) => vs[kMax][i];
		public void Set(int i, long v) => Add(i, v - vs[kMax][i]);

		public void Add(int i, long v)
		{
			foreach (var (k, j) in GetLevels(i)) vs[k][j] += v;
		}

		public long Subsum(int minIn, int maxEx) => GetRange(minIn, maxEx).Sum(x => vs[x.k][x.i]);

		// 範囲に加算する場合の例。
		//public void Add(int minIn, int maxEx, long v)
		//{
		//	foreach (var (k, i) in GetRange(minIn, maxEx)) vs[k][i] += v;
		//}

		//public long Get(int i) => GetLevels(i).Sum(x => vs[x.k][x.i]);
	}

	[TestClass]
	public class SegmentTreeTest
	{
		[TestMethod]
		public void Subsum_Random()
		{
			for (int k = 0; k < 10; k++)
			{
				for (int n = 0; n < 10; n++) Test(n);
				for (int n = 250; n < 260; n++) Test(n);
			}

			void Test(int n)
			{
				var a = RandomHelper.CreateData(n);
				var s = new int[n + 1];
				var st = new ST(n);

				for (int i = 0; i < n; i++)
				{
					s[i + 1] = s[i] + a[i];
					st.Add(i, a[i]);
				}

				for (int i = 0; i < n; i++)
					for (int j = i; j <= n; j++)
						Assert.AreEqual(s[j] - s[i], st.Subsum(i, j));
			}
		}

		[TestMethod]
		public void Inversion_Random()
		{
			for (int k = 0; k < 10; k++)
			{
				for (int n = 0; n < 10; n++) Test(n);
				for (int n = 250; n < 260; n++) Test(n);
			}

			void Test(int n)
			{
				var a = RandomHelper.ShuffleRange(1, n);

				var expected = 0;
				for (int i = 0; i < n; i++)
					for (int j = i + 1; j < n; j++)
						if (a[i] > a[j]) expected++;

				var st = new ST(n + 1);
				var r = 0L;
				for (int i = 0; i < n; i++)
				{
					r += a[i] - 1 - st.Subsum(0, a[i]);
					st.Add(a[i], 1);
				}
				Assert.AreEqual(expected, r);
			}
		}
	}
}
