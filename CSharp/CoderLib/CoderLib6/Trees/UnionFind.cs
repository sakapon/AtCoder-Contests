using System;
using System.Linq;

namespace CoderLib6.Trees
{
	// n: 個数
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/11/ALDS1_11_D
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/3/DSL/1/DSL_1_A
	// Test: https://atcoder.jp/contests/practice2/tasks/practice2_a
	class UF
	{
		int[] p;
		public UF(int n) { p = Enumerable.Range(0, n).ToArray(); }

		public void Unite(int a, int b) { if (!AreUnited(a, b)) p[p[b]] = p[a]; }
		public bool AreUnited(int a, int b) => GetRoot(a) == GetRoot(b);
		public int GetRoot(int a) => p[a] == a ? a : p[a] = GetRoot(p[a]);
		public int[][] ToGroups() => Enumerable.Range(0, p.Length).GroupBy(GetRoot).Select(g => g.ToArray()).ToArray();
	}

	class UF<T>
	{
		int[] p, sizes;
		T[] a;
		public Func<T, T, T> Merge;

		// (parent, child) -> result
		public UF(int n, Func<T, T, T> merge, T[] a0)
		{
			p = Enumerable.Range(0, n).ToArray();
			sizes = Array.ConvertAll(new bool[n], _ => 1);
			a = a0;
			Merge = merge;
		}

		public void Unite(int x, int y)
		{
			var px = GetRoot(x);
			var py = GetRoot(y);
			if (px == py) return;

			// 要素数が多いほうのグループに合流します。
			//if (sizes[px] < sizes[py]) (px, py) = (py, px);
			if (sizes[px] < sizes[py]) { var t = px; px = py; py = t; }
			p[py] = px;
			sizes[px] += sizes[py];
			a[px] = Merge(a[px], a[py]);
		}
		public bool AreUnited(int x, int y) => GetRoot(x) == GetRoot(y);
		public int GetRoot(int x) => p[x] == x ? x : p[x] = GetRoot(p[x]);
		public int[][] ToGroups() => Enumerable.Range(0, p.Length).GroupBy(GetRoot).Select(g => g.ToArray()).ToArray();
		public int GetSize(int x) => sizes[GetRoot(x)];
		public T GetValue(int x) => a[GetRoot(x)];
	}
}
