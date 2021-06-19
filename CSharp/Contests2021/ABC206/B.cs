using System;

class B
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());

		for (int i = 1, s = 0; ; i++)
			if ((s += i) >= n) return i;
	}
}
