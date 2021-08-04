using System;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		Console.ReadLine();
		var a = Read();
		Console.ReadLine();
		var b = Read();

		var r = a.Intersect(b).ToArray();
		if (r.Any()) Console.WriteLine(string.Join("\n", r));
	}
}
