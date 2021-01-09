using System;
using System.Linq;

class C
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var scs = new int[h[1]].Select(_ => Console.ReadLine()).ToArray();
		Console.WriteLine(Enumerable.Range(0, 1000).Select(i => $"{i}").FirstOrDefault(x => x.Length == h[0] && scs.All(sc => x[sc[0] - '1'] == sc[2])) ?? "-1");
	}
}
