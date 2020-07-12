using System;
using System.Collections.Generic;
using System.Linq;

class I
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var q = int.Parse(Console.ReadLine());

		var rows = Enumerable.Range(0, n + 1).ToArray();
		var cols = Enumerable.Range(0, n + 1).ToArray();
		var t = false;

		var r = new List<long>();
		for (int i = 0; i < q; i++)
		{
			var s = Read();
			if (s[0] == 1)
			{
				if (t)
					Swap(cols, s[1], s[2]);
				else
					Swap(rows, s[1], s[2]);
			}
			else if (s[0] == 2)
			{
				if (t)
					Swap(rows, s[1], s[2]);
				else
					Swap(cols, s[1], s[2]);
			}
			else if (s[0] == 3)
			{
				t = !t;
			}
			else
			{
				if (t)
					r.Add((long)n * (rows[s[2]] - 1) + cols[s[1]] - 1);
				else
					r.Add((long)n * (rows[s[1]] - 1) + cols[s[2]] - 1);
			}
		}
		Console.WriteLine(string.Join("\n", r));
	}

	static void Swap<T>(T[] a, int i, int j) { var o = a[i]; a[i] = a[j]; a[j] = o; }
}
