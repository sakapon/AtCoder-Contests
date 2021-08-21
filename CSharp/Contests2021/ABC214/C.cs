using System;

class C
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = ReadL();
		var t = ReadL();

		for (int i = 0; i < 2 * n; i++)
		{
			var i0 = i % n;
			var i1 = (i + 1) % n;
			t[i1] = Math.Min(t[i1], t[i0] + s[i0]);
		}

		return string.Join("\n", t);
	}
}
