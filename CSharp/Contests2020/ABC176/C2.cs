using System;
using System.Linq;

class C2
{
	static void Main()
	{
		Console.ReadLine();
		var t = 0L;
		Console.WriteLine(Console.ReadLine().Split().Select(long.Parse).Sum(x => (t = Math.Max(t, x)) - x));
	}
}
