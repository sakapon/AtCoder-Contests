using System;
using System.Collections.Generic;
using System.Linq;

namespace CoderLib8.Numerics
{
	// Test: https://atcoder.jp/contests/monamieHB2021/tasks/monamieHB2021_b
	// Test: https://atcoder.jp/contests/past202012-open/tasks/past202012_c
	public static class PositionalNotation
	{
		public static int[] Convert(this long x, int b)
		{
			var r = new List<int>();
			for (; x > 0; x /= b) r.Add((int)(x % b));
			return r.ToArray();
		}

		public static long ConvertFrom(this int[] a, int b)
		{
			var r = 0L;
			for (int i = a.Length - 1; i >= 0; --i) r = r * b + a[i];
			return r;
		}

		const string AN = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		static readonly Dictionary<char, int> ANMap = AN.Select((c, i) => (c, i)).ToDictionary(_ => _.c, _ => _.i);

		public static string ConvertAsString(this long x, int b)
		{
			if (x == 0) return "0";
			if (x < 0) return "-" + ConvertAsString(-x, b);
			var r = Convert(x, b);
			Array.Reverse(r);
			return new string(Array.ConvertAll(r, d => AN[d]));
		}

		public static long ConvertFrom(this string s, int b)
		{
			if (s.StartsWith('-')) return -ConvertFrom(s.Substring(1), b);
			var a = Array.ConvertAll(s.ToCharArray(), c => ANMap[c]);
			Array.Reverse(a);
			return ConvertFrom(a, b);
		}
	}
}
