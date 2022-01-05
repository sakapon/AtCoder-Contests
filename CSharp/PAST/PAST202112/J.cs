using System;
using System.Collections.Generic;
using System.Linq;

class J
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var (n, qc) = Read2();
		var qs = Array.ConvertAll(new bool[qc], _ => Console.ReadLine().Split());

		var d = ((decimal)n - 1) / 2;

		var r90 = new IntMatrix(new[] { new[] { 0L, -1 }, new[] { 1, 0L } });
		var r90_ = new IntMatrix(new[] { new[] { 0L, 1 }, new[] { -1, 0L } });

		var rx = new IntMatrix(new[] { new[] { -1, 0L }, new[] { 0L, 1 } });
		var ry = new IntMatrix(new[] { new[] { 1, 0L }, new[] { 0L, -1 } });

		var a = new Dictionary<(decimal, decimal), int>();
		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < n; j++)
			{
				a[(i - d, j - d)] = 0;
			}
		}

		var m = new IntMatrix(new[] { new[] { 1, 0L }, new[] { 0L, 1 } });

		foreach (var q in qs)
		{
			if (q[0] == "1")
			{
				var x = decimal.Parse(q[1]) - 1 - d;
				var y = decimal.Parse(q[2]) - 1 - d;

				var nx = m[0, 0] * x + m[0, 1] * y;
				var ny = m[1, 0] * x + m[1, 1] * y;

				a[(nx, ny)] = 1 - a[(nx, ny)];
			}
			else if (q[0] == "2")
			{
				if (q[1] == "A")
					m = m * r90;
				else
					m = m * r90_;
			}
			else
			{
				if (q[1] == "A")
					m = m * rx;
				else
					m = m * ry;
			}
		}

		m = new IntMatrix(new[] { new[] { 1, 0L }, new[] { 0L, 1 } });

		foreach (var q in qs)
		{
			if (q[0] == "1")
			{
			}
			else if (q[0] == "2")
			{
				if (q[1] == "A")
					m = r90_ * m;
				else
					m = r90 * m;
			}
			else
			{
				if (q[1] == "A")
					m = rx * m;
				else
					m = ry * m;
			}
		}

		var s = NewArray2<int>(n, n);

		foreach (var (p, v) in a)
		{
			var (x, y) = p;
			var nx = m[0, 0] * x + m[0, 1] * y + d;
			var ny = m[1, 0] * x + m[1, 1] * y + d;

			s[(int)nx][(int)ny] = v;
		}

		foreach (var r in s)
			Console.WriteLine(string.Join("", r));
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}

class IntVector
{
	public long[] V;
	public long this[int i] => V[i];
	public IntVector(long[] v) { V = v; }
	public override string ToString() => string.Join(" ", V);
	public static IntVector Parse(string s) => Array.ConvertAll(s.Split(), long.Parse);

	public static implicit operator IntVector(long[] v) => new IntVector(v);
	public static explicit operator long[](IntVector v) => v.V;

	public static IntVector operator -(IntVector v) => Array.ConvertAll(v.V, x => -x);
	public static IntVector operator +(IntVector v1, IntVector v2) => v1.V.Zip(v2.V, (x, y) => x + y).ToArray();
	public static IntVector operator -(IntVector v1, IntVector v2) => v1.V.Zip(v2.V, (x, y) => x - y).ToArray();

	public long NormL1 => V.Sum(x => Math.Abs(x));
	public double Norm => Math.Sqrt(V.Sum(x => x * x));

	public static long Dot(IntVector v1, IntVector v2) => v1.V.Zip(v2.V, (x, y) => x * y).Sum();
	public static bool IsOrthogonal(IntVector v1, IntVector v2) => Dot(v1, v2) == 0;
}

class IntMatrix
{
	public long[][] V;
	public long this[int r, int c] => V[r][c];
	public IntMatrix(long[][] v) { V = v; }
	public override string ToString() => string.Join("\n", V.Select(r => string.Join(" ", r)));
	public static IntMatrix Parse(string[] ls) => Array.ConvertAll(ls, s => Array.ConvertAll(s.Split(), long.Parse));

	public static implicit operator IntMatrix(long[][] v) => new IntMatrix(v);
	public static explicit operator long[][](IntMatrix v) => v.V;

	public static IntMatrix operator -(IntMatrix v) => Array.ConvertAll(v.V, r => (-(IntVector)r).V);
	public static IntMatrix operator +(IntMatrix v1, IntMatrix v2) => v1.V.Zip(v2.V, (x, y) => ((IntVector)x + y).V).ToArray();
	public static IntMatrix operator -(IntMatrix v1, IntMatrix v2) => v1.V.Zip(v2.V, (x, y) => ((IntVector)x - y).V).ToArray();
	public static IntVector operator *(IntMatrix m, IntVector v) => m.V.Select(r => IntVector.Dot(r, v)).ToArray();
	public static IntMatrix operator *(IntMatrix v1, IntMatrix v2) => v1.V.Select(r => Enumerable.Range(0, v2.V[0].Length).Select(j => r.Select((x, i) => x * v2[i, j]).Sum()).ToArray()).ToArray();
	//public static IntMatrix operator *(IntMatrix v1, IntMatrix v2)
	//{
	//	var t2 = v2.Transpose();
	//	return v1.V.Select(r => t2.V.Select(c => IntVector.Dot(r, c)).ToArray()).ToArray();
	//}

	public long[] GetRow(int r) => (long[])V[r].Clone();
	public long[] GetColumn(int c) => Array.ConvertAll(V, r => r[c]);
	public IntMatrix Transpose() => V[0].Select((x, c) => GetColumn(c)).ToArray();
}
