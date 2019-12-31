using System;
using System.Linq;

class C
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var p1 = read();
		var p2 = read();
		var p3 = read();
		int c1 = p1[1] - p1[0] + 1, c2 = p2[1] - p2[0] + 1, c3 = p3[1] - p3[0] + 1;
		Console.WriteLine(Enumerable.Range(p1[0], c1).Sum(x => (double)Math.Max(0, p2[1] - Math.Max(x, p2[0] - 1)) / c2 * Math.Max(0, p3[1] - Math.Max(x, p3[0] - 1)) / c3) / c1);
	}
}
