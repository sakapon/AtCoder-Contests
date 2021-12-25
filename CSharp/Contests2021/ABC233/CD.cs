using System;

class CD
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, x) = Read2L();
		var a = Array.ConvertAll(new bool[n], _ => ReadL()[1..]);

		var r = 0;
		Dfs(0, 1);
		return r;

		void Dfs(int i, long v)
		{
			if (i == n)
			{
				if (v == x) r++;
				return;
			}

			foreach (var av in a[i])
			{
				if (v <= x / av) Dfs(i + 1, v * av);
			}
		}
	}
}
