using System;
using System.Numerics;

class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static Vector3 ReadC() { var a = Read(); return new Vector3(a[0], a[1], 0); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var a = ReadC();
		var b = ReadC();
		var c = ReadC();
		var d = ReadC();

		return Check(b - a, d - a)
			&& Check(c - b, a - b)
			&& Check(d - c, b - c)
			&& Check(a - d, c - d);
	}

	static bool Check(Vector3 v1, Vector3 v2) => Vector3.Cross(v1, v2).Z > 0;
}
