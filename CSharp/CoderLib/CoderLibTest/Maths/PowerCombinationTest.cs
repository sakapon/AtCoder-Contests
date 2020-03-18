using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Maths
{
	class PowComb<T>
	{
		T[] v, p;
		Action<T[]> act;

		public void Find(T[] _v, int r, Action<T[]> _act)
		{
			v = _v; p = new T[r]; act = _act;
			if (p.Length > 0) Dfs(0); else act(p);
		}

		void Dfs(int i)
		{
			for (int j = 0; j < v.Length; ++j)
			{
				p[i] = v[j];
				if (i + 1 < p.Length) Dfs(i + 1); else act(p);
			}
		}
	}

	[TestClass]
	public class PowerCombinationTest
	{
		[TestMethod]
		public void Power()
		{
			int n = 10, r = 7, c = 0;
			var pow = Enumerable.Repeat(n, r).Aggregate((x, y) => x * y);
			new PowComb<int>().Find(Enumerable.Range(0, n).ToArray(), r, p => ++c);
			Assert.AreEqual(pow, c);
		}

		[TestMethod]
		public void Power_r()
		{
			for (int n = 3, r = 0; r <= 4; r++, Console.WriteLine())
				new PowComb<int>().Find(Enumerable.Range(1, n).ToArray(), r, p => Console.WriteLine(string.Join(" ", p)));

			new PowComb<int>().Find(new[] { 9, 7, 5, 3, 1 }, 2, p => Console.WriteLine(string.Join(" ", p)));
		}
	}
}
