using System;
using System.Linq;

class C
{
	static void Main() => Console.WriteLine(PrimesF(int.Parse(Console.ReadLine()), 100003)[0]);

	static int[] PrimesF(int m, int M)
	{
		var b = PrimeFlags(M);
		return Enumerable.Range(m, M - m + 1).Where(i => b[i]).ToArray();
	}
	static bool[] PrimeFlags(int M)
	{
		var rM = (int)Math.Sqrt(M);
		var b = new bool[M + 1]; b[2] = true;
		for (int i = 3; i <= M; i += 2) b[i] = true;
		for (int p = 3; p <= rM; p++) if (b[p]) for (var i = p * p; i <= M; i += 2 * p) b[i] = false;
		return b;
	}
}
