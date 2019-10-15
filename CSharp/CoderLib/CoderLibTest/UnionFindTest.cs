using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

class UF
{
	int[] p;
	public UF(int n) { p = Enumerable.Range(0, n).ToArray(); }

	public void Unite(int a, int b) { if (!AreUnited(a, b)) p[p[b]] = p[a]; }
	public bool AreUnited(int a, int b) => GetRoot(a) == GetRoot(b);
	int GetRoot(int a) => p[a] == a ? a : p[a] = GetRoot(p[a]);
	public int[][] ToGroups() => Enumerable.Range(0, p.Length).GroupBy(GetRoot).Select(g => g.ToArray()).ToArray();
}

[TestClass]
public class UnionFindTest
{
	Random random = new Random();

	[TestMethod]
	public void Unite()
	{
		var n = 100000;
		var groups = Enumerable.Range(0, n).Select(i => new { i, key = random.Next(n) }).ToLookup(_ => _.key, _ => _.i);

		var uf = new UF(n);
		foreach (var g in groups)
		{
			var x0 = g.First();
			foreach (var x in g) uf.Unite(x0, x);
		}
		var actual = uf.ToGroups();

		Assert.AreEqual(groups.Count, actual.Length);
		foreach (var _ in groups.Zip(actual, (x, y) => new { x, y }))
			CollectionAssert.AreEqual(_.x.ToArray(), _.y);
	}
}
