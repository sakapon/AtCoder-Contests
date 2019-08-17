using System;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var d = Console.ReadLine().Split().Select(int.Parse).OrderBy(x => x).ToArray();

		Console.WriteLine(d[n / 2] - d[n / 2 - 1]);
	}
}
