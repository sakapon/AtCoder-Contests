using System;
using System.Linq;

class E
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();

		var r = 1L;
		var t = Enumerable.Repeat(-1, 3).ToArray();
		for (int i = 0; i < n; i++)
		{
			r = r * t.Count(x => x == a[i] - 1) % 1000000007;
			for (int j = 0; j < 3; j++) if (t[j] == a[i] - 1) { t[j]++; break; }
		}
		Console.WriteLine(r);
	}
}
