using System;
using System.Linq;

class B2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		if (a.Any(x => x == 0)) { Console.WriteLine(0); return; }
		try
		{
			var r = checked(a.Aggregate((x, y) => x * y));
			Console.WriteLine(r > 1000000000000000000 ? -1 : r);
		}
		catch (OverflowException)
		{
			Console.WriteLine(-1);
		}
	}
}
