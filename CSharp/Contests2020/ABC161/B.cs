using System;
using System.Linq;

class B
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var m = Read()[1];
		var a = Read();

		var s = a.Sum();
		Console.WriteLine(a.Count(x => 4 * m * x >= s) >= m ? "Yes" : "No");
	}
}
