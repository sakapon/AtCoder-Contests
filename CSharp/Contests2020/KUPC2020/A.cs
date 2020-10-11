using System;
using System.Linq;

class A
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();
		Console.WriteLine(Enumerable.Range(0, n - 1).Sum(i => Math.Abs(ps[i + 1][0] - ps[i][0]) + Math.Abs(ps[i + 1][1] - ps[i][1])));
	}
}
