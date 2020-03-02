using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Maths
{
	class Perm<T>
	{
		T[] v, p;
		bool[] u;
		Action<T[]> act;

		public void Find(T[] _v, int r, Action<T[]> _act)
		{
			v = _v; p = new T[r]; u = new bool[v.Length]; act = _act;
			if (p.Length > 0) Dfs(0); else act(p);
		}

		void Dfs(int i)
		{
			for (int j = 0; j < v.Length; ++j)
			{
				if (u[j]) continue;
				p[i] = v[j];
				u[j] = true;
				if (i + 1 < p.Length) Dfs(i + 1); else act(p);
				u[j] = false;
			}
		}
	}

	[TestClass]
	public class PermutationTest
	{
		[TestMethod]
		public void Permutation()
		{
			int n = 10, r = 9, c = 0;
			var nPr = Enumerable.Range(n - r + 1, r).Aggregate((x, y) => x * y);
			new Perm<int>().Find(Enumerable.Range(0, n).ToArray(), r, p => ++c);
			Assert.AreEqual(nPr, c);
		}

		[TestMethod]
		public void Permutation_r()
		{
			for (int n = 4, r = 0; r <= n; r++, Console.WriteLine())
				new Perm<int>().Find(Enumerable.Range(1, n).ToArray(), r, p => Console.WriteLine(string.Join(" ", p)));

			new Perm<int>().Find(new[] { 9, 7, 5, 3 }, 3, p => Console.WriteLine(string.Join(" ", p)));
		}
	}
}
