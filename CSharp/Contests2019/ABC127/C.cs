using System;
using System.Linq;

class C
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var gs = new int[h[1]].Select(_ => read()).ToArray();
		Console.WriteLine(Math.Max(gs.Min(g => g[1]) - gs.Max(g => g[0]) + 1, 0));
	}
}
