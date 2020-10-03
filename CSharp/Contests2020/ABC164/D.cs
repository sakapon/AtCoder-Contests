using System;
using System.Linq;

class D
{
	static void Main()
	{
		var s = Console.ReadLine();
		var n = s.Length;
		var m = new int[n + 1];
		for (int d = 1, i = n - 1; i >= 0; d = d * 10 % 2019, i--)
			m[i] = ((s[i] - '0') * d + m[i + 1]) % 2019;
		Console.WriteLine(m.GroupBy(x => x).Select(g => g.LongCount()).Sum(c => c * (c - 1) / 2));
	}
}
