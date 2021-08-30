using System;

class C
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());

		if (n == 4) return "2 3 2 1";
		if (n == 5) return "3 2 3 1 1";
		if (n < 7) return -1;

		var r = new int[n];
		Array.Fill(r, 1);
		r[0] = n - 3;
		r[1] = 3;
		r[2] = 2;
		r[n - 4] = 2;
		return string.Join(" ", r);
	}
}
