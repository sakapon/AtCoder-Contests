using System;
using System.Linq;

class D
{
	static void Main()
	{
		Console.ReadLine();
		var d = Console.ReadLine().Select((c, i) => new { c, i }).GroupBy(_ => _.c).ToDictionary(g => g.Key, g => g.Select(_ => _.i).ToArray());

		var r = 0;
		foreach (var p1 in d)
		{
			var i1 = p1.Value[0];

			foreach (var p3 in d)
			{
				var i3 = p3.Value.Last();
				if (i3 - i1 < 2) continue;

				foreach (var p2 in d)
				{
					var j2 = Array.BinarySearch(p2.Value, i1 + 1);
					if (j2 < 0) j2 = ~j2;
					if (j2 >= p2.Value.Length || p2.Value[j2] >= i3) continue;
					r++;
				}
			}
		}
		Console.WriteLine(r);
	}
}
