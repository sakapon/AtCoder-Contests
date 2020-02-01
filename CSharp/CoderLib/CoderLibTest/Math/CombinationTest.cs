using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

class Comb<T>
{
	T[] v, p;
	Action<T[]> act;

	public void Find(T[] _v, int r, Action<T[]> _act)
	{
		v = _v; p = new T[r]; act = _act;
		if (p.Length > 0) Dfs(0, 0); else act(p);
	}

	void Dfs(int i, int j0)
	{
		for (int j = j0; j < v.Length; ++j)
		{
			p[i] = v[j];
			if (i + 1 < p.Length) Dfs(i + 1, j + 1); else act(p);
		}
	}
}

[TestClass]
public class CombinationTest
{
	[TestMethod]
	public void Combination()
	{
		int n = 60, r = 5, c = 0;
		var nCr = Enumerable.Range(n - r + 1, r).Aggregate((x, y) => x * y) / Enumerable.Range(1, r).Aggregate((x, y) => x * y);
		new Comb<int>().Find(Enumerable.Range(0, n).ToArray(), r, p => ++c);
		Assert.AreEqual(nCr, c);
	}

	[TestMethod]
	public void Combination_r()
	{
		for (int n = 6, r = 0; r <= n; r++, Console.WriteLine())
			new Comb<int>().Find(Enumerable.Range(1, n).ToArray(), r, p => Console.WriteLine(string.Join(" ", p)));

		new Comb<int>().Find(new[] { 9, 7, 5, 3, 1 }, 3, p => Console.WriteLine(string.Join(" ", p)));
	}
}
