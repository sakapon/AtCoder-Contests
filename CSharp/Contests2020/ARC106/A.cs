using System;

class A
{
	static void Main()
	{
		var n = long.Parse(Console.ReadLine());

		var p3 = PowsL(3, 39);
		var p5 = PowsL(5, 27);

		for (int i = 1; i < p3.Length; i++)
		{
			for (int j = 1; j < p5.Length; j++)
			{
				if (p3[i] + p5[j] == n)
				{
					Console.WriteLine($"{i} {j}");
					return;
				}
			}
		}
		Console.WriteLine(-1);
	}

	public static long[] PowsL(long b, int n)
	{
		var p = new long[n + 1];
		p[0] = 1;
		for (int i = 0; i < n; ++i) p[i + 1] = p[i] * b;
		return p;
	}
}
