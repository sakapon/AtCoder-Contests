using System;
using System.Linq;

class A
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = new int[n].Select(_ => Console.ReadLine().Split().Select(double.Parse).ToArray()).ToArray();

		//var d = 4.01 * Enumerable.Range(0, n - 1).Min(i => NormL1(ps[i + 1], ps[i]));
		var d = 401D;
		double r;
		while (double.IsNaN(r = MinDistance(ps, d /= 2))) ;
		Console.WriteLine($"{r:F9}");
	}

	const int Sup = 400;
	static double MinDistance(double[][] ps, double d)
	{
		var gs0 = ps.GroupBy(p => new P((int)Math.Floor((p[0] + 100) / d), (int)Math.Floor((p[1] + 100) / d))).Select(g => g.ToArray()).ToArray();
		if (gs0.Max(g => g.Length) > Sup) return double.NaN;

		var gs1 = ps.GroupBy(p => new P((int)Math.Floor((p[0] + 100) / d), (int)Math.Floor((p[1] + 100 + d / 2) / d))).Select(g => g.ToArray()).ToArray();
		if (gs1.Max(g => g.Length) > Sup) return double.NaN;

		var gs2 = ps.GroupBy(p => new P((int)Math.Floor((p[0] + 100 + d / 2) / d), (int)Math.Floor((p[1] + 100) / d))).Select(g => g.ToArray()).ToArray();
		if (gs2.Max(g => g.Length) > Sup) return double.NaN;

		var gs3 = ps.GroupBy(p => new P((int)Math.Floor((p[0] + 100 + d / 2) / d), (int)Math.Floor((p[1] + 100 + d / 2) / d))).Select(g => g.ToArray()).ToArray();
		if (gs3.Max(g => g.Length) > Sup) return double.NaN;

		return gs0.Concat(gs1).Concat(gs2).Concat(gs3).Min(g => AllMinDistance(g));
	}

	static double AllMinDistance(double[][] p)
	{
		var m = double.MaxValue;
		for (int i = 0; i < p.Length; i++)
			for (int j = i + 1; j < p.Length; j++)
				m = Math.Min(m, Norm(p[i], p[j]));
		return m;
	}

	static double Norm(double[] p, double[] q) => Math.Sqrt((p[0] - q[0]) * (p[0] - q[0]) + (p[1] - q[1]) * (p[1] - q[1]));
	static double NormL1(double[] p, double[] q) => Math.Abs(p[0] - q[0]) + Math.Abs(p[1] - q[1]);
}

public struct P : IEquatable<P>
{
	public int X, Y;
	public P(int x, int y) { X = x; Y = y; }

	public bool Equals(P other) => X == other.X && Y == other.Y;
	public static bool operator ==(P v1, P v2) => v1.Equals(v2);
	public static bool operator !=(P v1, P v2) => !v1.Equals(v2);
	public override bool Equals(object obj) => obj is P && Equals((P)obj);
	public override int GetHashCode() => X ^ Y;
}
