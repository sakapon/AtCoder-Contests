using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var s = Array.ConvertAll(new bool[10], _ => Console.ReadLine());

		var r10 = Enumerable.Range(0, 10).ToArray();
		var a = r10.First(i => s[i].Contains('#'));
		var b = r10.Last(i => s[i].Contains('#'));

		var c = s[a].IndexOf('#');
		var d = s[a].LastIndexOf('#');

		Console.WriteLine($"{a + 1} {b + 1}");
		Console.WriteLine($"{c + 1} {d + 1}");
	}
}
