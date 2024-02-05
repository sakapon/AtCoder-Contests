using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (n, m) = Read2();
		var s = Console.ReadLine().ToCharArray();
		var t = Console.ReadLine();

		var Wild = new string('#', m);
		var q = new Queue<int>();

		for (int l = 0; l + m <= n; l++)
		{
			if (AreEqual(l, t)) q.Enqueue(l);
		}

		while (q.Count > 0)
		{
			var v = q.Dequeue();

			for (int i = 0; i < m; i++)
			{
				s[v + i] = '#';
			}

			for (int d = m - 1; d > 0; d--)
			{
				var nv = v - d;
				if (nv >= 0 && !AreEqual(nv, Wild) && AreEqual(nv, t)) q.Enqueue(nv);

				nv = v + d;
				if (nv + m <= n && !AreEqual(nv, Wild) && AreEqual(nv, t)) q.Enqueue(nv);
			}
		}

		return s.All(c => c == '#');

		bool AreEqual(int l, string t)
		{
			for (int i = 0; i < m; i++)
			{
				if (s[l + i] != '#' && s[l + i] != t[i]) return false;
			}
			return true;
		}
	}
}
