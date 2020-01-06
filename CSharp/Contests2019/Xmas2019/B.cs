using System;
using System.Diagnostics;
using System.Linq;

class B
{
	static void Main()
	{
		var sw = Stopwatch.StartNew();

		var h = Console.ReadLine().Split();
		var n = int.Parse(h[0]);
		var op = h[1];

		var random = new Random();
		var empty = new int[2 * n];

		if (op == "=")
		{
			Console.WriteLine(string.Join(" ", Enumerable.Range(0, n)));
		}
		else if (op == "<")
		{
			if (n <= 2) { Console.WriteLine("Merry Christmas!"); return; }

			var n2 = n * 10;
			while (sw.ElapsedMilliseconds < 1900)
			{
				var x = empty.Select(_ => random.Next(n2)).Distinct().Take(n).ToArray();
				var sl = x.SelectMany(a => x.Select(b => a + b)).Distinct().Count();
				var dl = x.SelectMany(a => x.Select(b => a - b)).Distinct().Count();
				if (sl < dl) { Console.WriteLine(string.Join(" ", x.OrderBy(v => v))); return; }
			}
			Console.WriteLine("Merry Christmas!");
		}
		else
		{
			if (n <= 7) { Console.WriteLine("Merry Christmas!"); return; }

			var n2 = n * 3;
			while (sw.ElapsedMilliseconds < 1800)
			{
				var x = empty.Select(_ => random.Next(n2)).Distinct().Take(n).ToArray();
				var sl = x.SelectMany(a => x.Select(b => a + b)).Distinct().Count();
				var dl = x.SelectMany(a => x.Select(b => a - b)).Distinct().Count();
				if (sl > dl) { Console.WriteLine(string.Join(" ", x.OrderBy(v => v))); return; }
			}
			Console.WriteLine("Merry Christmas!");
		}
	}
}
