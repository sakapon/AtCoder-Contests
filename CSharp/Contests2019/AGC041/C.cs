using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static void Main()
	{
		var p3 = new[]
		{
			".aa",
			"a..",
			"a..",
		};
		var p4 = new[]
		{
			"aaef",
			"bbef",
			"ghcc",
			"ghdd",
		};

		var n = int.Parse(Console.ReadLine());
		if (n == 2) { Console.WriteLine(-1); return; }
		if (n == 3) { Console.WriteLine(string.Join("\n", p3)); return; }
		var c4 = n / 4 - 1;
		var n4 = n - 4 * c4;

		var r = new int[n].Select(_ => Enumerable.Repeat('.', n).ToArray()).ToArray();
		Copy(r, p4, 0, c4);
		Copy(r, n4 == 4 ? p4 : GetPattern(n4), 4 * c4, 1);
		Console.WriteLine(string.Join("\n", r.Select(x => new string(x))));
	}

	static void Copy(char[][] r, string[] p, int start, int count)
	{
		var pn = p.Length;
		for (int k = 0; k < pn * count; k += pn)
			for (int i = 0; i < pn; i++)
				for (int j = 0; j < pn; j++)
					r[start + k + i][start + k + j] = p[i][j];
	}

	static string[] GetPattern(int n)
	{
		var random = new Random();
		Func<P> next1 = () => new P { i = random.Next(n), j = random.Next(n - 1) };
		Func<P> next2 = () => new P { i = random.Next(n - 1), j = random.Next(n) };

		var qi = new Queue<P>();
		var qj = new Queue<P>();
		var u = new bool[n, n];
		var ui = new int[n];
		var uj = new int[n];
		for (int k = 0; k < n; k++)
		{
			while (true)
			{
				var p = next1();
				if (u[p.i, p.j] || u[p.i, p.j + 1]) continue;
				qi.Enqueue(p);
				u[p.i, p.j] = true;
				u[p.i, p.j + 1] = true;
				ui[p.i]++;
				uj[p.j]++; uj[p.j + 1]++;
				break;
			}
			while (true)
			{
				var p = next2();
				if (u[p.i, p.j] || u[p.i + 1, p.j]) continue;
				qj.Enqueue(p);
				u[p.i, p.j] = true;
				u[p.i + 1, p.j] = true;
				ui[p.i]++; ui[p.i + 1]++;
				uj[p.j]++;
				break;
			}
		}
		while (true)
		{
			if (ui.All(x => x == 3) && uj.All(x => x == 3))
				return ToPattern(n, qi.ToArray(), qj.ToArray());

			{
				var p = qi.Dequeue();
				u[p.i, p.j] = false;
				u[p.i, p.j + 1] = false;
				ui[p.i]--;
				uj[p.j]--; uj[p.j + 1]--;
			}
			while (true)
			{
				var p = next1();
				if (u[p.i, p.j] || u[p.i, p.j + 1]) continue;
				qi.Enqueue(p);
				u[p.i, p.j] = true;
				u[p.i, p.j + 1] = true;
				ui[p.i]++;
				uj[p.j]++; uj[p.j + 1]++;
				break;
			}

			if (ui.All(x => x == 3) && uj.All(x => x == 3))
				return ToPattern(n, qi.ToArray(), qj.ToArray());

			{
				var p = qj.Dequeue();
				u[p.i, p.j] = false;
				u[p.i + 1, p.j] = false;
				ui[p.i]--; ui[p.i + 1]--;
				uj[p.j]--;
			}
			while (true)
			{
				var p = next2();
				if (u[p.i, p.j] || u[p.i + 1, p.j]) continue;
				qj.Enqueue(p);
				u[p.i, p.j] = true;
				u[p.i + 1, p.j] = true;
				ui[p.i]++; ui[p.i + 1]++;
				uj[p.j]++;
				break;
			}
		}
	}

	static string[] ToPattern(int n, P[] pi, P[] pj)
	{
		var r = new int[n].Select(_ => Enumerable.Repeat('.', n).ToArray()).ToArray();
		for (int k = 0; k < n; k++)
		{
			r[pi[k].i][pi[k].j] = (char)('a' + k);
			r[pi[k].i][pi[k].j + 1] = (char)('a' + k);
			r[pj[k].i][pj[k].j] = (char)('n' + k);
			r[pj[k].i + 1][pj[k].j] = (char)('n' + k);
		}
		return r.Select(x => new string(x)).ToArray();
	}

	struct P
	{
		public int i, j;
	}
}
