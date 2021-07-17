using System;
using System.Linq;

class B
{
	const long M = 1000000007;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		long r = a[0];
		for (int i = 1; i < n; i++)
		{
			r += (1 + r) * a[i];
			r %= M;
		}
		return r;
	}
}
