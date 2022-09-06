using System;

class E
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());

		var r = 0.0;

		for (int i = 0; i < n; i++)
		{
			var t = 0.0;
			for (int x = 1; x <= 6; x++)
			{
				t += Math.Max(x, r);
			}
			r = t / 6;
		}
		return r;
	}
}
