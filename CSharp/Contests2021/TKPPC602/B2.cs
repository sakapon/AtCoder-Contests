using System;
using System.Linq;

class B2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();
		var t = Console.ReadLine();

		int[] GetIndexes(string s) => Enumerable.Range(0, n).Where(i => (i % 2 == 0) ^ (s[i] == 'B')).ToArray();

		var a = GetIndexes(s);
		var b = GetIndexes(t);

		if (a.Length != b.Length) return -1;
		return Enumerable.Range(0, a.Length).Sum(i => (long)Math.Abs(a[i] - b[i]));
	}
}
