using System;
using System.Linq;
using static System.Math;

class Q018
{
	static double[] ReadD() => Array.ConvertAll(Console.ReadLine().Split(), double.Parse);
	static (double, double, double) Read3D() { var a = ReadD(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var T = int.Parse(Console.ReadLine());
		var (L, X, Y) = Read3D();
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => int.Parse(Console.ReadLine()));

		(double y, double z) GetPosition(int t)
		{
			var angle = 2 * PI * t / T;
			return (-L / 2 * Sin(angle), L / 2 * (1 - Cos(angle)));
		}

		double GetAngle(int t)
		{
			var (y, z) = GetPosition(t);
			var xy = Sqrt(X * X + (y - Y) * (y - Y));
			return Atan2(z, xy) * 180 / PI;
		}

		return string.Join("\n", qs.Select(GetAngle));
	}
}
