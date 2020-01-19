using System;
using System.Linq;

class I
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		a = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		for (int i = 0; i < n; i++)
		{
			var ok = true;
			for (int j = 0; j < n; j++)
			{
				if (i == j) continue;
				if (Match(i, j) != i) { ok = false; break; }
			}
			if (ok) { Console.WriteLine(i + 1); return; }
		}
		Console.WriteLine(-1);
	}

	static int[][] a;

	static int Match(int i, int j)
	{
		var ti = (a[i][0] - 1) / a[j][1];
		var tj = (a[j][0] - 1) / a[i][1];

		return ti > tj ? i : ti < tj ? j : -1;
	}
}
