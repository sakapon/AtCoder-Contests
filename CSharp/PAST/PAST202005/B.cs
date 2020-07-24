using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int n = h[0], m = h[1], q = h[2];

		var players = new int[n].Select(_ => new bool[m]).ToArray();
		var scores = Enumerable.Repeat(n, m).ToArray();

		var r = new List<int>();
		for (int i = 0; i < q; i++)
		{
			var s = Read();
			if (s[0] == 1)
			{
				r.Add(players[s[1] - 1].Select((b, j) => b ? scores[j] : 0).Sum());
			}
			else
			{
				players[s[1] - 1][s[2] - 1] = true;
				scores[s[2] - 1]--;
			}
		}
		Console.WriteLine(string.Join("\n", r));
	}
}
