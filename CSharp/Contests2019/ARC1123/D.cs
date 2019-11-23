using System;
using System.Linq;

class D
{
	static void Main() => Console.WriteLine(Dfs(new int[int.Parse(Console.ReadLine())].Select(_ => Console.ReadLine().Split().Select(long.Parse).ToArray()).ToArray()));

	static long Dfs(long[][] dc)
	{
		var s = dc.Sum(x => x[0] * x[1]);
		return dc.Sum(x => x[1]) - 1 + (s < 10 ? 0 : Dfs(new[] { new[] { 1, s / 10 }, new[] { s % 10, 1 } }));
	}
}
