using System;

class F
{
	static void Main()
	{
		var p = int.Parse(Console.ReadLine());
		if (p == 1) { Console.WriteLine(1); return; }

		var n = 1 << 20;

		var f = new int[n + 1];
		f[2] = f[1] = 1;
		for (int i = 3; i <= n; ++i)
		{
			f[i] = (f[i - 1] + f[i - 2]) % p;
			if (f[i] == 0) { Console.WriteLine(i); return; }
		}
		Console.WriteLine(-1);
	}
}
