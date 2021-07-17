using System;

class F
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var p = int.Parse(Console.ReadLine());
		if (p == 1) return 1;

		var f = new int[p * p + 2];
		f[2] = f[1] = 1;
		for (int i = 3; i < f.Length; ++i)
		{
			f[i] = (f[i - 1] + f[i - 2]) % p;
			if (f[i] == 0) return i;
		}
		return -1;
	}
}
