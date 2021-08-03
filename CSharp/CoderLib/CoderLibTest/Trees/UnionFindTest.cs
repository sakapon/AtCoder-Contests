using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.DataTrees;
using KLibrary.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Trees
{
	[TestClass]
	public class UnionFindTest
	{
		[TestMethod]
		public void Unite()
		{
			var n = 100000;
			var groups = Enumerable.Range(0, n).Select(i => new { i, key = RandomHelper.Random.Next(n) }).ToLookup(_ => _.key, _ => _.i);

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
}
