using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main()
	{
		var d = new[]
		{
			"1234345456",
			"2123234345",
			"3212323434",
			"4321432543",
			"3234123234",
			"4323212323",
			"5432321432",
			"4345234123",
			"5434323212",
			"6543432321",
		};
		map = Enumerable.Range(0, 10).ToDictionary(i => (char)('0' + i), i => Enumerable.Range(0, 10).ToDictionary(j => (char)('0' + j), j => d[i][j] - '0'));

		var h = Console.ReadLine().Split().Select(long.Parse).ToArray();
		long m = h[0], r = h[1];

		var c = Count(r);
		for (int k = 0; k < 1000000; k++)
		{
			r += m;
			c = Math.Min(c, Count(r));
		}
		Console.WriteLine(c);
	}

	static Dictionary<char, Dictionary<char, int>> map;

	static int Count(long n)
	{
		var s = $"0{n}";
		return Enumerable.Range(0, s.Length - 1).Sum(i => map[s[i]][s[i + 1]]);
	}
}
