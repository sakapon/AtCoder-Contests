using System;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void WriteYesNo(bool b) => Console.WriteLine(b ? "Yes" : "No");
	static void Main()
	{
		var (n, m, c) = Read3();
		var s = Console.ReadLine().Replace(" ", "");
		var l0 = Convert.ToInt32(string.Join("", s.Reverse()), 2);
		var b = Array.ConvertAll(new bool[n], _ => Read());

		var rc = Enumerable.Range(0, c).ToArray();

		var off = false;
		var on = false;
		var allOn = (1 << m) - 1;

		Power(rc, n, p =>
		{
			var l = l0;

			for (int j = 0; j < n; j++)
			{
				var i = b[j][p[j]] - 1;
				l ^= 1 << i;
			}

			if (l == 0) off = true;
			if (l == allOn) on = true;
		});

		WriteYesNo(off);
		WriteYesNo(on);
	}

	public static void Power<T>(T[] values, int r, Action<T[]> action)
	{
		var n = values.Length;
		var p = new T[r];

		if (r > 0) Dfs(0);
		else action(p);

		void Dfs(int i)
		{
			var i2 = i + 1;
			for (int j = 0; j < n; ++j)
			{
				p[i] = values[j];

				if (i2 < r) Dfs(i2);
				else action(p);
			}
		}
	}
}
