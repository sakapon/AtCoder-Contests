using System;

class A
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var f = new long[n + 1];
		f[1] = f[0] = 1;
		for (int i = 2; i <= n; ++i) f[i] = f[i - 1] + f[i - 2];

		Console.WriteLine(f[n]);
	}
}
