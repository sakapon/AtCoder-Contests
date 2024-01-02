using System;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var u = new int[n + 1];
		var i = 0;
		var sv = DFS(1);

		int DFS(int v)
		{
			if (u[v] != 0) return v;
			u[v] = ++i;
			return DFS(a[v - 1]);
		}

		var r = Enumerable.Range(1, n).Where(v => u[v] >= u[sv]).OrderBy(v => u[v]).ToArray();
		return $"{r.Length}\n" + string.Join(" ", r);
	}
}
