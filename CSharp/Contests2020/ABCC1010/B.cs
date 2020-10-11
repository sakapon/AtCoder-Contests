using System;
using System.Linq;

class B
{
	static void Main()
	{
		var z = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		int h = z[0], w = z[1];
		var s = new int[h].Select(_ => Console.ReadLine()).ToArray();

		var c1 = s.Sum(t => Enumerable.Range(0, w - 1).Count(j => t[j] == '.' && t[j + 1] == '.'));
		var c2 = Enumerable.Range(0, w).Sum(j => Enumerable.Range(0, h - 1).Count(i => s[i][j] == '.' && s[i + 1][j] == '.'));
		Console.WriteLine(c1 + c2);
	}
}
