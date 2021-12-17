using System;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Win" : "Lose");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var xor = a.Aggregate((x, y) => x ^ y);

		foreach (var x in a)
			if ((xor ^ x) == 0) return true;
		return n % 2 == 1;
	}
}
