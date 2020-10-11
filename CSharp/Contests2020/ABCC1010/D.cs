using System;
using System.Linq;

class D
{
	static void Main()
	{
		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		var t = int.Parse(Console.ReadLine());
		const long M = 1000000007;

		for (int k = 0; k < t; k++)
		{
			var h = Console.ReadLine().Split().Select(long.Parse).ToArray();
			long n = h[0], a = h[1], b = h[2];
			if (a < b) (a, b) = (b, a);

			var ca = (n - a + 1) * (n - a + 1) % M;
			var cb = (n - b + 1) * (n - b + 1) % M;
			var cAll = ca * cb % M;

			var m = Math.Min(n, a + b - 1);
			var x = (n - a + 1) * (a - b + 1) % M;
			var y = (2 * n - m - a + 1) * (m - a) % M;
			var cUnion = (x + y) * (x + y) % M;

			Console.WriteLine((cAll - cUnion + M) % M);
		}
		Console.Out.Flush();
	}
}
