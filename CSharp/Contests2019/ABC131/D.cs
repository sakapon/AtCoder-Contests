using System;
using System.Linq;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var ws = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).OrderBy(x => x[1]).ToArray();

		var ok = true;
		var t = 0;
		foreach (var w in ws) if ((t += w[0]) > w[1]) { ok = false; break; }
		Console.WriteLine(ok ? "Yes" : "No");
	}
}
