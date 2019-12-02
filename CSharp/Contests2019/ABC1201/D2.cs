using System;
using System.Linq;

class D2
{
	static void Main()
	{
		Console.ReadLine();
		var d = Console.ReadLine().Select((c, i) => new { c, i }).GroupBy(_ => _.c).Select(g => g.Select(_ => _.i).ToArray()).ToArray();

		var q = from i1 in d.Select(x => x[0])
				from i3 in d.Select(x => x.Last()).Where(i3 => i3 - i1 >= 2)
				select d.Count(x => x.Any(i2 => i1 < i2 && i2 < i3));
		Console.WriteLine(q.Sum());
	}
}
