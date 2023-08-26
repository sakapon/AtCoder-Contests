using System;

class C3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var sv = 1;
		var i = 1;
		var u = new int[n + 1];

		while (u[sv] == 0)
		{
			u[sv] = i++;
			sv = a[sv - 1];
		}

		var r = new int[i - u[sv]];
		for (int v = 1; v <= n; v++)
		{
			if (u[v] >= u[sv]) r[u[v] - u[sv]] = v;
		}
		return $"{r.Length}\n" + string.Join(" ", r);
	}
}
