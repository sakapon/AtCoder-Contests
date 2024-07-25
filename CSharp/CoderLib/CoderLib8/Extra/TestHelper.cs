using System;
using System.Linq;

namespace CoderLib8.Extra
{
	public static class TestHelper
	{
		static readonly Random random = new Random();

		public static T[] Shuffle<T>(T[] a)
		{
			var n = a.Length;
			var r = new T[n];
			for (int i = 0; i < n; ++i)
			{
				var j = random.Next(n - i);
				r[i] = a[j];
				a[j] = a[^(i + 1)];
			}
			return r;
		}
		public static int[] Shuffle(int n)
		{
			var a = new int[n];
			for (int i = 0; i < n; ++i) a[i] = i;
			return Shuffle(a);
		}

		#region Trees
		public static (int u, int v)[] CreateTree(int n, bool from1 = true)
		{
			var a = new int[n];
			for (int i = 0; i < n; ++i) a[i] = from1 ? i + 1 : i;
			a = Shuffle(a);

			var r = new (int, int)[n - 1];
			for (int i = 1; i < n; ++i) r[i - 1] = (a[random.Next(i)], a[i]);
			return r;
		}
		public static void WriteTree(int n, bool from1 = true)
		{
			var es = CreateTree(n, from1);
			var ess = string.Join("\n", es.Select(e => $"{e.u} {e.v}"));
			Console.Write($"{n} {n - 1}\n{ess}\n");
		}
		#endregion

		#region Rationals
		const int M = 998244353;
		static int Gcd(int a, int b) { if (b == 0) return a; for (int r; (r = a % b) > 0; a = b, b = r) ; return b; }

		// 真分数を復元します。
		// maxDenom < M
		public static void WriteProperRationals(long q, int maxDenom = 1 << 24)
		{
			if (q < 0 || M <= q)
			{
				Console.WriteLine("Out of range.");
				return;
			}

			for (var d = 1; d <= maxDenom; ++d)
			{
				var n = (int)(q * d % M);
				if (n < d && Gcd(n, d) == 1) Console.WriteLine($"{n} / {d}");
			}
		}

		// 仮分数を復元します。
		// maxDenom < M
		public static void WriteRationals(long q, int maxDenom = 1 << 24, long maxValue = 1 << 10, int maxCount = 10)
		{
			if (q < 0 || M <= q)
			{
				Console.WriteLine("Out of range.");
				return;
			}

			for (int d = 1, c = 0; d <= maxDenom && c < maxCount; ++d)
			{
				var n = (int)(q * d % M);
				if (n <= maxValue * d && Gcd(n, d) == 1)
				{
					++c;
					Console.WriteLine($"{n} / {d}");
				}
			}
		}
		#endregion
	}
}
