using System;
using System.Collections.Generic;
using System.Numerics;

class G2
{
	static AsciiIO io = new AsciiIO();
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = io.Int();
		var a = io.Array(n, () => new BitSet(n));

		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < n; j++)
			{
				a[i][j] = io.Char() == '1';
			}
		}

		var r = 0L;
		for (int i = 0; i < n; i++)
		{
			for (int j = i + 1; j < n; j++)
			{
				if (!a[i][j]) continue;

				var and = a[i].And(a[j]);
				r += and.PopCount();
			}
		}
		return r / 3;
	}
}
