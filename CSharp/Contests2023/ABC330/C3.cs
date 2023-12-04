using System;

class C3
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var d = long.Parse(Console.ReadLine());

		var r = 1L << 60;

		for (long x = 0; x < 1500000; x++)
		{
			var c = x * x - d;
			if (c >= 0)
			{
				r = Math.Min(r, c);
			}
			else
			{
				var sq = (long)Math.Sqrt(-c);
				r = Math.Min(r, Math.Abs(c + sq * sq));
				sq++;
				r = Math.Min(r, Math.Abs(c + sq * sq));
			}
		}
		return r;
	}
}
