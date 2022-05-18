using System;

class C
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());

		var r = new int[(1 << n) - 1];

		for (int k = 1; k <= n; k++)
		{
			var d = 1 << k;

			for (int i = d / 2 - 1; i < r.Length; i += d)
			{
				r[i] = k;
			}
		}
		return string.Join(" ", r);
	}
}
