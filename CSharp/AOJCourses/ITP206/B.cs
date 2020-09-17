using System;
using System.Linq;

class B
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var m = int.Parse(Console.ReadLine());
		var b = Read();
		Console.WriteLine(a.Intersect(b).Count() == m ? 1 : 0);
	}
}
