using System;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Read();
		var t = int.Parse(Console.ReadLine());

		Array.Sort(s);

		var r = 0;
		var nt = 0;

		foreach (var x in s)
		{
			if (x < nt) continue;

			r++;
			nt = x - x % t + t;
		}
		return r;
	}
}
