using System;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = Read()[0];
		var a = Read();
		var l = Read();

		foreach (var i in l)
		{
			if (a[i - 1] == n) continue;
			if (i < a.Length && a[i - 1] + 1 == a[i]) continue;
			a[i - 1]++;
		}
		return string.Join(" ", a);
	}
}
