using System;

class F
{
	static AsciiIO io = new AsciiIO();
	static void Main() => Console.WriteLine(string.Join("\n", io.Array(io.Int(), () => Solve())));
	static object Solve()
	{
		var b = io.Long();
		var k = io.Long();
		var (sx, sy) = io.Long2();
		var (gx, gy) = io.Long2();

		var r = Distance(sx, sy, gx, gy) * k;

		foreach (var (x1, y1, t1) in ToMainStreet(sx, sy))
		{
			foreach (var (x2, y2, t2) in ToMainStreet(gx, gy))
			{
				r = Math.Min(r, OnMainStreet(x1, y1, x2, y2) + t1 + t2);
			}
		}
		return r;

		(long, long, long t)[] ToMainStreet(long x, long y)
		{
			return new[]
			{
				(x / b * b, y, x % b * k),
				(x / b * b + b, y, (b - x % b) * k),
				(x, y / b * b, y % b * k),
				(x, y / b * b + b, (b - y % b) * k),
			};
		}

		// 大通りの座標同士
		long OnMainStreet(long x1, long y1, long x2, long y2)
		{
			if ((x1 / b, y1 / b) == (x2 / b, y2 / b)) return Distance(x1, y1, x2, y2);

			if (x1 % b == 0)
			{
				(x1, y1) = (y1, x1);
				(x2, y2) = (y2, x2);
			}

			var d1 = Distance(x1 / b * b, y1, x2, y2) + x1 % b;
			var d2 = Distance(x1 / b * b + b, y1, x2, y2) + b - x1 % b;
			return Math.Min(d1, d2);
		}

		long Distance(long x1, long y1, long x2, long y2)
		{
			return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
		}
	}
}

public class AsciiIO
{
	static bool[] lf = new bool[1 << 7];
	static bool[] sp = new bool[1 << 7];
	static AsciiIO() => lf['\r'] = lf['\n'] = sp['\r'] = sp['\n'] = sp[' '] = true;

	System.IO.Stream si = new System.IO.BufferedStream(Console.OpenStandardInput(), 8192);

	int b, s;
	void NextValid() { while (sp[b = si.ReadByte()]) ; }
	bool Next() => !sp[b = si.ReadByte()];

	public int Int() => (int)Long();
	public (int, int) Int2() => (Int(), Int());

	public long Long()
	{
		NextValid();
		if ((s = b) == '-') Next();
		var r = 0L;
		do r = r * 10 + (b & ~'0'); while (Next());
		return s == '-' ? -r : r;
	}
	public (long, long) Long2() => (Long(), Long());

	public T[] Array<T>(int n, Func<T> f) { var r = new T[n]; for (var i = 0; i < n; ++i) r[i] = f(); return r; }
}
