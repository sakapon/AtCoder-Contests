using System.Linq;
using System.Numerics;
using static System.Console;

class B
{
	static void Main()
	{
		var h = ReadLine().Split().Select(int.Parse).ToArray();
		var a = ReadLine().Split().Select(int.Parse).ToArray();

		BigInteger x1 = Enumerable.Range(0, h[0]).SelectMany(i => Enumerable.Range(0, i).Select(j => new { i, j })).Count(_ => a[_.j] < a[_.i]);
		BigInteger x2 = Enumerable.Range(0, h[0]).SelectMany(i => Enumerable.Range(i + 1, h[0] - i - 1).Select(j => new { i, j })).Count(_ => a[_.j] < a[_.i]);
		WriteLine((x1 * (h[1] - 1) + x2 * (h[1] + 1)) * h[1] / 2 % 1000000007);
	}
}
