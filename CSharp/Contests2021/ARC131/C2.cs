using System;
using System.Linq;

class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Win" : "Lose");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var xor = a.Aggregate((x, y) => x ^ y);
		return n % 2 == 1 || a.Any(x => (xor ^ x) == 0);
	}
}
