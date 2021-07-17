using System;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (a, b, c) = Read3();

		var r = CubicEquation0.Solve(1, -a, b, -c);
		return string.Join(" ", r);
	}
}

// 一般形のまま Newton 法を適用します。
// 誤差は小さいです。
public static class CubicEquation0
{
	static Func<double, double> CreateFunction(double a, double b, double c, double d) =>
		x => x * (x * (a * x + b) + c) + d;
	static Func<double, double> CreateDerivative(double a, double b, double c) =>
		x => x * (3 * a * x + 2 * b) + c;

	// f(x) = ax^3 + bx^2 + cx + d = 0
	public static double[] Solve(double a, double b, double c, double d)
	{
		if (a == 0) throw new ArgumentException("The value must not be 0.", nameof(a));
		if (a < 0) return Solve(-a, -b, -c, -d);

		var f = CreateFunction(a, b, c, d);
		var xc = -b / (3 * a);
		var yc = f(xc);
		if (yc < 0) return Solve(a, -b, c, -d).Reverse().Select(x => -x).ToArray();

		var f1 = CreateDerivative(a, b, c);
		// 微修正
		// 自明解
		if (yc == 0 && f1(xc) > 0) return new[] { xc };
		if (yc == 0 && f1(xc) == 0) return new[] { xc, xc, xc };

		// xc より小さい実数解
		var x1 = SolveNegative();
		var p = a * x1 + b;
		var q = p * x1 + c;
		return QuadraticEquation0.Solve(a, p, q).Prepend(x1).ToArray();

		double SolveNegative()
		{
			var x0 = -1D;
			while (f(xc + x0) > 0) x0 *= 2;
			return NewtonMethod.Solve(f, f1, xc + x0);
		}
	}
}

// 一般形のまま Newton 法を適用します。
// 誤差は小さいです。
public static class QuadraticEquation0
{
	static Func<double, double> CreateFunction(double a, double b, double c) =>
		x => x * (a * x + b) + c;
	static Func<double, double> CreateDerivative(double a, double b) =>
		x => 2 * a * x + b;

	// f(x) = ax^2 + bx + c = 0
	public static double[] Solve(double a, double b, double c)
	{
		if (a == 0) throw new ArgumentException("The value must not be 0.", nameof(a));
		if (a < 0) return Solve(-a, -b, -c);

		var det = b * b - 4 * a * c;
		if (det.EqualsNearly(0)) return new[] { -b / (2 * a) };
		if (det < 0) return new double[0];

		var f = CreateFunction(a, b, c);
		var f1 = CreateDerivative(a, b);
		var x01 = (-b - Math.Max(det, 1)) / (2 * a);
		var x02 = (-b + Math.Max(det, 1)) / (2 * a);
		return new[] { NewtonMethod.Solve(f, f1, x01), NewtonMethod.Solve(f, f1, x02) };
	}
}

public static class NewtonMethod
{
	/// <summary>
	/// 方程式 f(x) = 0 を満たす x の近似値を Newton 法により求めます。
	/// </summary>
	/// <param name="f">対象となる関数。</param>
	/// <param name="f1">f の導関数。</param>
	/// <param name="x0">x の初期値。</param>
	/// <returns>方程式 f(x) = 0 の近似解。</returns>
	public static double Solve(Func<double, double> f, Func<double, double> f1, double x0)
	{
		for (var i = 0; i < 100; i++)
		{
			var temp = x0 - f(x0) / f1(x0);
			if (x0 == temp) break;
			x0 = temp;
		}
		return x0;
	}

	public static double RoundSolution(Func<double, double> f, double x)
	{
		if (f(x) == 0) return x;
		var r = Math.Round(x, 12);
		return f(r) == 0 ? r : x;
	}

	public static bool EqualsNearly(this double x, double y, int digits = 12) =>
		Math.Round(x - y, digits) == 0;
	public static bool IsAlmostInteger(this double x, int digits = 12) =>
		Math.Round(x - Math.Round(x), digits) == 0;

	public static double RoundAlmost(this double x)
	{
		var r = Math.Round(x, 6);
		return Math.Round(x - r, 12) == 0 ? r : x;
	}
}
