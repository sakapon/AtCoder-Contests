using System;
using System.Linq;

namespace CoderLib6.Values
{
	// 任意次元行列
	// 行、列の順のジャグ配列をラップします。
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/2/ITP1/6/ITP1_6_D
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/2/ITP1/7/ITP1_7_D
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
	}
}
