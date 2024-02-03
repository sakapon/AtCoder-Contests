using System;

class A
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		Console.WriteLine(Fibonacci1(n));
	}

	// 貰う
	static long Fibonacci1(int n)
	{
		var f = new long[n + 1];
		f[1] = f[0] = 1;
		for (int i = 2; i <= n; i++)
			f[i] = f[i - 1] + f[i - 2];
		return f[n];
	}

	// 配る
	static long Fibonacci2(int n)
	{
		var f = new long[n + 1];
		f[1] = f[0] = 1;
		for (int i = 1; i < n; i++)
			f[i + 1] = f[i] + f[i - 1];
		return f[n];
	}

	// メモリ節約
	static long Fibonacci3(int n)
	{
		if (n <= 1) return 1;
		long v0 = 1, v1 = 1, v2 = 0;
		for (int i = 2; i <= n; i++)
		{
			v2 = v0 + v1;
			v0 = v1;
			v1 = v2;
		}
		return v2;
	}
}
