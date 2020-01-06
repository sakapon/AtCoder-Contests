using System;
using System.Linq;

class D3
{
	static void Main()
	{
		Console.ReadLine();
		var d = Console.ReadLine().Select((c, i) => new { c, i }).ToLookup(_ => _.c, _ => _.i);

		var r = 0;
		foreach (var i1 in d.Select(g => g.First()))
			foreach (var i2 in d.Select(g => g.FirstOrDefault(x => x > i1)).Where(x => x > 0))
				foreach (var g3 in d.Where(g => g.Last() > i2))
					r++;
		Console.WriteLine(r);
	}
}
