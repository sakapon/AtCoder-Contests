using System;
using System.Linq;

class D3
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var d = s.ToLookup(c => c);
		var r = d.Count < 3 ? 0 : d.Select(g => g.LongCount()).Aggregate((x, y) => x * y);

		var q =
			from i in Enumerable.Range(0, n)
			from j in Enumerable.Range(i + 1, n - i - 1)
			where 2 * j - i < n && s[i] + s[j] + s[2 * j - i] == 'R' + 'G' + 'B'
			select 0;
		Console.WriteLine(r - q.Count());
	}
}
