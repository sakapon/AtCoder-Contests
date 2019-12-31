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

		Func<int[], int, double> high = (p, x) => (double)Math.Max(0, p[1] - Math.Max(x, p[0] - 1)) / (p[1] - p[0] + 1);
		Console.WriteLine(Enumerable.Range(p1[0], p1[1] - p1[0] + 1).Average(x => high(p2, x) * high(p3, x)));
	}
}
