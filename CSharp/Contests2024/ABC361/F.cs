using System.Numerics;
using CoderLib8.Numerics;

class F
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = long.Parse(Console.ReadLine());

		var r = InverseFunc.FloorSqrt()(n);
		var set = new HashSet<long>();

		for (int b = 3; b < 60; b++)
		{
			for (int a = 2; ; a++)
			{
				var x = BigInteger.Pow(a, b);
				if (x > n) break;

				if (InverseFunc.IsSquareNumber((long)x)) continue;
				if (set.Add((long)x)) r++;
			}
		}

		return r;
	}
}
