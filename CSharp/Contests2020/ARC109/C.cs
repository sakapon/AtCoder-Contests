using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, k) = Read2();
		var s = Console.ReadLine();

		var d = new Dictionary<int, char>
		{
			{ 'R' + 'R', 'R' },
			{ 'P' + 'P', 'P' },
			{ 'S' + 'S', 'S' },
			{ 'R' + 'S', 'R' },
			{ 'P' + 'R', 'P' },
			{ 'S' + 'P', 'S' },
		};

		for (int i = 0; i < k; i++)
		{
			s += s;
			s = string.Join("", Enumerable.Range(0, n).Select(j => d[s[2 * j] + s[2 * j + 1]]));
		}
		Console.WriteLine(s[0]);
	}
}
