using System;
using System.Linq;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		Console.ReadLine();
		var a = Read();
		Console.ReadLine();
		var b = Read();

		// OrderBy を使うと TLE。
		var r = a.Union(b).ToArray();
		Array.Sort(r);
		Console.WriteLine(string.Join("\n", r));
	}
}
