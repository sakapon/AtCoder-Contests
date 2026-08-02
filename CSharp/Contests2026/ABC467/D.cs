using System.Numerics;

class D
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));
	static object Solve()
	{
		var a = ReadL();

		var (a1, b1, c1) = GetMiddleLineEquation(a[0], a[1], a[2], a[3]);
		var (a2, b2, c2) = GetMiddleLineEquation(a[4], a[5], a[6], a[7]);

		if (a1 * b2 != a2 * b1) return "Yes";
		if (a1 != 0 && a1 * c2 == a2 * c1) return "Yes";
		if (b1 != 0 && b1 * c2 == b2 * c1) return "Yes";
		return "No";
	}

	// 指定された2点からの距離が等しい点の集合である、直線の方程式を求めます。
	// ax + by + c = 0 の形式で返します。
	//public static (double a, double b, double c) GetMiddleLineEquation(double x1, double y1, double x2, double y2)
	//{
	//	var dx = x2 - x1;
	//	var dy = y2 - y1;
	//	return (dx * 2, dy * 2, -dx * (x1 + x2) - dy * (y1 + y2));
	//}
	//public static (long a, long b, long c) GetMiddleLineEquation(long x1, long y1, long x2, long y2)
	//{
	//	var dx = x2 - x1;
	//	var dy = y2 - y1;
	//	return (dx * 2, dy * 2, -dx * (x1 + x2) - dy * (y1 + y2));
	//}
	public static (BigInteger a, BigInteger b, BigInteger c) GetMiddleLineEquation(BigInteger x1, BigInteger y1, BigInteger x2, BigInteger y2)
	{
		var dx = x2 - x1;
		var dy = y2 - y1;
		return (dx * 2, dy * 2, -dx * (x1 + x2) - dy * (y1 + y2));
	}
}
