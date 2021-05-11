using System;
using System.Linq;

class Q027L
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var q = new bool[n].Select((_, i) => (i: i + 1, s: Hash(Console.ReadLine(), 1000000007))).GroupBy(_ => _.s).Select(g => g.First().i);
		Console.WriteLine(string.Join("\n", q));
	}

	public static long Hash(string s, long p) => Hash(s, 0, s.Length, p);
	public static long Hash(string s, int start, int count, long p)
	{
		var h = 0L;
		for (int i = 0; i < count; ++i) h = h * p + s[start + i];
		return h;
	}
}
