using System;
using System.Linq;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Array.ConvertAll(new bool[n], _ => (V)Read2());
		var t = Array.ConvertAll(new bool[n], _ => (V)Read2());

		if (n == 1) return true;

		var tset = t.ToHashSet();

		var sm = s.Aggregate((u, v) => u + v) / n;
		var tm = t.Aggregate((u, v) => u + v) / n;

		s = Array.ConvertAll(s, p => p - sm);
		var t2 = Array.ConvertAll(t, p => p - tm);

		var sp = s.First(p => p.Norm > 0.1);
		return t2.Any(tp => Check(sp, tp));

		// sp と tp が対応すると仮定してチェック
		bool Check(V sp, V tp)
		{
			if (!EqualsNearly(tp.Norm, sp.Norm, 9)) return false;

			var angle = tp.Angle - sp.Angle;
			var s2 = Array.ConvertAll(s, p => p.Rotate(angle));
			s2 = Array.ConvertAll(s2, p => p + tm);

			if (!Array.TrueForAll(s2, p => IsAlmostInteger(p.X, 9))) return false;
			if (!Array.TrueForAll(s2, p => IsAlmostInteger(p.Y, 9))) return false;
			s2 = Array.ConvertAll(s2, p => new V(Math.Round(p.X), Math.Round(p.Y)));

			return tset.SetEquals(s2);
		}
	}

	public static bool EqualsNearly(double x, double y, int digits = 12) =>
		Math.Round(x - y, digits) == 0;
	public static bool IsAlmostInteger(double x, int digits = 12) =>
		Math.Round(x - Math.Round(x), digits) == 0;
}
