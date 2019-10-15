using System;
using System.Linq;

class E
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var a = read();

		var s = a.Sum();
		for (var i = s; i > 0; i--)
		{
			if (s % i != 0) continue;
			var m = a.Select(x => x % i).OrderByDescending(x => x).ToArray();
			if (m.Skip(m.Sum() / i).Sum() <= h[1]) { Console.WriteLine(i); return; }
		}
	}
}
