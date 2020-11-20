using System;
using System.Linq;

namespace CoderLib6.Trees
{
	// n: 個数
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/11/ALDS1_11_D
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/3/DSL/1/DSL_1_A
	// Test: https://atcoder.jp/contests/practice2/tasks/practice2_a
	// Test: https://atcoder.jp/contests/abl/tasks/abl_c
	// Test: https://atcoder.jp/contests/abc183/tasks/abc183_f
	class UF
	{
		int[] p, sizes;
		public int GroupsCount;
		public UF(int n)
		{
			p = Enumerable.Range(0, n).ToArray();
			sizes = Array.ConvertAll(p, _ => 1);
			GroupsCount = n;
		}

		public int GetRoot(int x) => p[x] == x ? x : p[x] = GetRoot(p[x]);
		public int GetSize(int x) => sizes[GetRoot(x)];

		public bool AreUnited(int x, int y) => GetRoot(x) == GetRoot(y);
		public bool Unite(int x, int y)
		{
			x = GetRoot(x);
			y = GetRoot(y);
			if (x == y) return false;

			// 要素数が大きいほうのグループに合流します。
			if (sizes[x] < sizes[y]) Merge(y, x);
			else Merge(x, y);
			return true;
		}
		protected virtual void Merge(int x, int y)
		{
			p[y] = x;
			sizes[x] += sizes[y];
			--GroupsCount;
		}
		public int[][] ToGroups() => Enumerable.Range(0, p.Length).GroupBy(GetRoot).Select(g => g.ToArray()).ToArray();
	}

	class UF<T> : UF
	{
		T[] a;
		// (parent, child) => result
		Func<T, T, T> MergeData;
		public UF(int n, Func<T, T, T> merge, T[] a0) : base(n)
		{
			a = a0;
			MergeData = merge;
		}

		public T GetValue(int x) => a[GetRoot(x)];
		protected override void Merge(int x, int y)
		{
			base.Merge(x, y);
			a[x] = MergeData(a[x], a[y]);
		}
	}
}
