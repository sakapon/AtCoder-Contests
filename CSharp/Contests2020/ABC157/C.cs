using System;
using System.Linq;

class C
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var scs = new int[h[1]].Select(_ => Console.ReadLine()).ToArray();

		var r = Enumerable.Range(0, 1000).Select(i => $"{i}").Where(x => x.Length == h[0] && scs.All(sc => x[sc[0] - '1'] == sc[2])).ToArray();
		Console.WriteLine(r.Any() ? r[0] : "-1");
	}
}
