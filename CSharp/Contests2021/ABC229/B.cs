using System;
using System.Linq;

class B
{
	static void Main() => Console.WriteLine(Solve() ? "Easy" : "Hard");
	static bool Solve()
	{
		var ab = Console.ReadLine().Split();

		var a = ab[0].Select(c => c - '0').Reverse();
		var b = ab[1].Select(c => c - '0').Reverse();
		return a.Zip(b, (x, y) => x + y).All(x => x < 10);
	}
}
