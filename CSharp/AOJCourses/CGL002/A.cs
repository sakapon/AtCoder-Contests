using System;
using System.Linq;

class A
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));

	static long Solve()
	{
		var h = Read();
		V p0 = new V(h[0], h[1]), p1 = new V(h[2], h[3]), p2 = new V(h[4], h[5]), p3 = new V(h[6], h[7]);
		V s1 = p1 - p0, s2 = p3 - p2;

		return V.IsParallel(s1, s2) ? 2 : V.IsOrthogonal(s1, s2) ? 1 : 0;
	}
}

struct V
{
	public int X, Y;
	public V(int x, int y) { X = x; Y = y; }
	public override string ToString() => $"{X} {Y}";

	public static V operator -(V v) => new V(-v.X, -v.Y);
	public static V operator +(V v1, V v2) => new V(v1.X + v2.X, v1.Y + v2.Y);
	public static V operator -(V v1, V v2) => new V(v1.X - v2.X, v1.Y - v2.Y);

	public double Norm => Math.Sqrt(X * X + Y * Y);

	public static int Dot(V v1, V v2) => v1.X * v2.X + v1.Y * v2.Y;
	public static bool IsParallel(V v1, V v2) => v1.X * v2.Y == v2.X * v1.Y;
	public static bool IsOrthogonal(V v1, V v2) => v1.X * v2.X == -v1.Y * v2.Y;
}
