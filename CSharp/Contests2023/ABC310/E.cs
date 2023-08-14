using System;

class E
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var r = 0L;
		var (t0, t1) = (0, 0);

		for (int i = 0; i < n; i++)
		{
			if (s[i] == '0')
			{
				(t0, t1) = (1, t0 + t1);
			}
			else
			{
				(t0, t1) = (t1, t0 + 1);
			}
			r += t1;
		}
		return r;
	}
}
