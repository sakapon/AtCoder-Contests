﻿using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, x, y) = ((int, long, long))Read3L();
		var a = ReadL();

		var r60 = Enumerable.Range(0, 60).ToArray();
		bool[] ToBool(long v) => Array.ConvertAll(r60, i => (v & (1L << i)) != 0);
		bool?[] ToBoolN(long v) => Array.ConvertAll(r60, i => (bool?)((v & (1L << i)) != 0));
		bool Matches(bool[] x, bool?[] y) => Array.TrueForAll(r60, i => (x[i] ^ y[i]) != true);

		var p = new Stack<int>();
		var xf = ToBool(x);

		bool Dfs(int i, bool?[] yf)
		{
			if (i < 0)
			{
				return Matches(xf, yf);
			}
			else
			{
				var af = ToBool(a[i]);

				if (Matches(af, yf))
				{
					p.Push(3);
					while (p.Count < n) p.Push(1);
					return true;
				}
				{
					var ok = true;
					var t = new bool?[60];

					for (int f = 0; f < 60; f++)
					{
						if (yf[f] == null || (af[f] & null) == yf[f]) { }
						else if ((af[f] & true) == yf[f]) t[f] = true;
						else if ((af[f] & false) == yf[f]) t[f] = false;
						else { ok = false; break; }
					}
					if (ok)
					{
						p.Push(1);
						if (Dfs(i - 1, t)) return true;
						p.Pop();
					}
				}
				{
					var ok = true;
					var t = new bool?[60];

					for (int f = 0; f < 60; f++)
					{
						if (yf[f] == null || (af[f] | null) == yf[f]) { }
						else if ((af[f] | true) == yf[f]) t[f] = true;
						else if ((af[f] | false) == yf[f]) t[f] = false;
						else { ok = false; break; }
					}
					if (ok)
					{
						p.Push(2);
						if (Dfs(i - 1, t)) return true;
						p.Pop();
					}
				}
				return false;
			}
		}

		if (!Dfs(n - 1, ToBoolN(y))) return -1;
		return string.Join(" ", p);
	}
}
