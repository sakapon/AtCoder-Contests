using System;
using System.Linq;

namespace CoderLib6.DataTrees
{
	// n: 個数
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/11/ALDS1_11_D
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/3/DSL/1/DSL_1_A
	// Test: https://atcoder.jp/contests/practice2/tasks/practice2_a
	// Test: https://atcoder.jp/contests/abl/tasks/abl_c
	// Test: https://atcoder.jp/contests/abc183/tasks/abc183_f
	public class UF
	{
		int[] p, sizes;
		public int GroupsCount { get; private set; }

		public UF(int n)
		{
			p = Array.ConvertAll(new bool[n], _ => -1);
			sizes = Array.ConvertAll(p, _ => 1);
			GroupsCount = n;
		}

		public int GetRoot(int x) => p[x] == -1 ? x : p[x] = GetRoot(p[x]);
		public bool AreUnited(int x, int y) => GetRoot(x) == GetRoot(y);
		public int GetSize(int x) => sizes[GetRoot(x)];

		public bool Unite(int x, int y)
		{
			if ((x = GetRoot(x)) == (y = GetRoot(y))) return false;

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
		public ILookup<int, int> ToGroups() => Enumerable.Range(0, p.Length).ToLookup(GetRoot);
	}

	public class UF<T> : UF
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
