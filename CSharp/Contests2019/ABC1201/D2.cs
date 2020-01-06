using System;
using System.Linq;

class D2
{
	static void Main()
	{
		Console.ReadLine();
		var d = Console.ReadLine().Select((c, i) => new { c, i }).ToLookup(_ => _.c, _ => _.i);

		var q = from i1 in d.Select(g => g.First())
				from i2 in d.Select(g => g.FirstOrDefault(x => x > i1)).Where(x => x > 0)
				select d.Count(g => g.Last() > i2);
		Console.WriteLine(q.Sum());
	}
}
