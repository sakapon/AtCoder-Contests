using System;
using System.Text;

class F
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static (long, long, long, long) Read4L() { var a = ReadL(); return (a[0], a[1], a[2], a[3]); }
	static void Main()
	{
		var (n, m) = Read2L();
		var qc = int.Parse(Console.ReadLine());
		var sb = new StringBuilder();

		while (qc-- > 0)
		{
			var (a, b, c, d) = Read4L();
			sb.AppendLine(Query(a, b, c, d).ToString());
		}
		Console.Write(sb);

		long GetValue(long i, long j) => (i - 1) * m + j;

		long Query(long a, long b, long c, long d)
		{
			var h = b - a + 1;
			var w = d - c + 1;

			if (h % 2 == 1 && w % 2 == 0)
			{
				if (h == 1)
				{
					return QueryRow(a, b, c, d);
				}
				else
				{
					return (QueryRow(a, a, c, d) + Query(a + 1, b, c, d)) % M;
				}
			}
			if (h % 2 == 0 && w % 2 == 1)
			{
				if (w == 1)
				{
					return QueryCol(a, b, c, d);
				}
				else
				{
					return (QueryCol(a, b, c, c) + Query(a, b, c + 1, d)) % M;
				}
			}

			var count = h % 2 == 0 || w % 2 == 0 || (a + c) % 2 == 1 ? h * w / 2 : (h * w + 1) / 2;
			count %= M;

			var start = GetValue(a, c);
			var end = GetValue(b, d);
			var value = start + end;
			value %= M;

			var r = value * count % M;
			r = r * MHalf % M;
			return r;
		}

		// 横一列 (a==b)、偶数個
		long QueryRow(long a, long b, long c, long d)
		{
			var start = (a + c) % 2 == 1 ? 0 : GetValue(a, c);
			return (start + Query(a, b, c + 1, d)) % M;
		}

		// 縦一列 (c==d)、偶数個
		long QueryCol(long a, long b, long c, long d)
		{
			var start = (a + c) % 2 == 1 ? 0 : GetValue(a, c);
			return (start + Query(a + 1, b, c, d)) % M;
		}
	}

	const long M = 998244353;
	const long MHalf = (M + 1) / 2;
}
