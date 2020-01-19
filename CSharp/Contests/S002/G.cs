using System;
using System.Linq;

class G
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		Console.WriteLine(string.Join("\n", a.Select(x => Gcd(x[0], x[1]))));
	}

	static int Gcd(int x, int y) { for (int r; (r = x % y) > 0; x = y, y = r) ; return y; }
}
