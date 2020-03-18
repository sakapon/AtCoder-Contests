using System;
using System.Linq;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var p = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).OrderBy(x => x[0]).ToArray();

		var a_max = p.Last()[0];
		var d = p.Max(x => x[1] - 2 * a_max + x[0]);
		Console.WriteLine(2 * a_max + Math.Max(d, 0));
	}
}
