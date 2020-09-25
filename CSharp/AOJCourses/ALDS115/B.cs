using System;
using System.Linq;

class B
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		var w = h[1];

		var rv = 0M;
		var tw = 0;

		foreach (var p in new int[h[0]].Select(_ => Read()).OrderBy(p => -(decimal)p[0] / p[1]))
		{
			if (p[1] >= w - tw)
			{
				rv += (decimal)(w - tw) / p[1] * p[0];
				break;
			}
			else
			{
				rv += p[0];
				tw += p[1];
			}
		}
		Console.WriteLine($"{rv:F9}");
	}
}
