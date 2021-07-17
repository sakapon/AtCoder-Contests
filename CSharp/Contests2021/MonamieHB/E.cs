using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var p = Read().Select(x => x - 1).ToArray();

		var r = new List<(int i, int j)>();

		for (int x = 0; x < n; x++)
		{
			var i = Array.IndexOf(p, x, x);

			for (int j = i - 1; j >= x; j--)
			{
				if (p[j] >= i)
				{
					(p[i], p[j]) = (p[j], p[i]);
					r.Add((j, i));
					i = j;
				}
			}
		}

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		Console.WriteLine(r.Count);
		foreach (var (i, j) in r) Console.WriteLine($"{i + 1} {j + 1}");
		Console.Out.Flush();
	}
}
