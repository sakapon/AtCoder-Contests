using System;
using System.Linq;

class D
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int n = h[0], x = h[1], y = h[2];

		var r = new int[n];
		for (int i = 1; i <= n; i++)
			for (int j = i + 1; j <= n; j++)
				r[Math.Min(j - i, Math.Abs(i - x) + 1 + Math.Abs(j - y))]++;
		Console.WriteLine(string.Join("\n", r.Skip(1)));
	}
}
