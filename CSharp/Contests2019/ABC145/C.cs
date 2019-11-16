using System;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();
		Console.WriteLine(ps.SelectMany(p => ps.Select(q => Norm(p, q))).Sum() / n);
	}

	static double Norm(int[] p, int[] q)
	{
		var dx = p[0] - q[0];
		var dy = p[1] - q[1];
		return Math.Sqrt(dx * dx + dy * dy);
	}
}
