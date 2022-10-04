using System;
using System.Linq;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, a, b) = Read3();
		var s = Array.ConvertAll(new bool[n], _ => long.Parse(Console.ReadLine()));

		Array.Sort(s);
		if (s[0] == s[^1]) return -1;

		// (M - m)p = b
		// Sp + nq = na
		var p = (double)b / (s[^1] - s[0]);
		var q = a - s.Sum() * p / n;
		return $"{p} {q}";
	}
}
