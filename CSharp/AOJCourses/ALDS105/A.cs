using System;
using System.Linq;

class A
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var q = int.Parse(Console.ReadLine());
		var m = Read();

		var u = new bool[2001];
		for (int x = 0; x < 1 << n; x++)
		{
			var sum = a.Where((v, i) => (x & (1 << i)) != 0).Sum();
			if (sum <= 2000) u[sum] = true;
		}

		Console.WriteLine(string.Join("\n", m.Select(x => u[x] ? "yes" : "no")));
	}
}
