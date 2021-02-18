using System;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = Read();
		Array.Reverse(a);

		var x = new int[m + 1];
		int c = 1 << 20, t = 1;

		if (a[0] == a[1]) return "No";
		x[a[0]] = c;
		x[a[1]] = c + t++;

		for (int i = 2; i < n; i++)
		{
			if (x[a[i]] == 0)
			{
				if (x[a[i - 2]] < x[a[i - 1]])
					x[a[i]] = c - t++;
				else
					x[a[i]] = c + t++;
			}
			else
			{
				if (!IsKadomatsu(x, a, i - 2)) return "No";
			}
		}

		return "Yes\n" + string.Join(" ", x.Skip(1).Select(v => v == 0 ? 1 : v));
	}

	static bool IsKadomatsu(int[] x, int[] a, int i) => x[a[i]] != x[a[i + 2]] && (x[a[i]] < x[a[i + 1]] && x[a[i + 1]] > x[a[i + 2]] || x[a[i]] > x[a[i + 1]] && x[a[i + 1]] < x[a[i + 2]]);
}
