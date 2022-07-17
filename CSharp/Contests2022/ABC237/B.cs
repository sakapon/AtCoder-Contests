using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var a = Array.ConvertAll(new bool[h], _ => ReadL());

		var m = new IntMatrix(a);
		return m.Transpose().ToString();
	}
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

	public long[] GetRow(int r) => (long[])V[r].Clone();
	public long[] GetColumn(int c) => Array.ConvertAll(V, r => r[c]);
	public IntMatrix Transpose() => V[0].Select((x, c) => GetColumn(c)).ToArray();
}
