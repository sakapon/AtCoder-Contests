using System;
using System.Linq;

namespace CoderLib6.Trees
{
	// n: 個数
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
}
