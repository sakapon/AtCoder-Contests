using System;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var map = new int[n + 1][];
		for (int i = 0; i < n; i++)
		{
			var a = Read();
			map[a[0]] = a.Skip(2).ToArray();
		}

		var pre = new int[n + 1];
		var post = new int[n + 1];
		var u = new bool[n + 1];
		var t = 0;

		Action<int> Dfs = null;
		Dfs = v =>
		{
			if (u[v]) return;
			u[v] = true;

			pre[v] = ++t;
			foreach (var nv in map[v])
			{
				Dfs(nv);
			}
			post[v] = ++t;
		};

		for (int v = 1; v <= n; v++)
			Dfs(v);

		for (int v = 1; v <= n; v++)
			Console.WriteLine($"{v} {pre[v]} {post[v]}");
	}
}
