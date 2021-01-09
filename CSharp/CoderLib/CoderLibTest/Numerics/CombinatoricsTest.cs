using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Numerics
{
	[TestClass]
	public class CombinatoricsTest
	{
		[TestMethod]
		public void Permutation()
		{
			int n = 10, r = 9, c = 0;
			var nPr = Enumerable.Range(n - r + 1, r).Aggregate((x, y) => x * y);
			Combinatorics.Permutation(Enumerable.Range(0, n).ToArray(), r, p => ++c);
			Assert.AreEqual(nPr, c);
		}

		[TestMethod]
		public void Permutation_r()
		{
			for (int n = 4, r = 0; r <= n; r++, Console.WriteLine())
				Combinatorics.Permutation(Enumerable.Range(1, n).ToArray(), r, p => Console.WriteLine(string.Join(" ", p)));

			Combinatorics.Permutation(new[] { 9, 7, 5, 3 }, 3, p => Console.WriteLine(string.Join(" ", p)));
		}

		[TestMethod]
		public void Combination()
		{
			int n = 60, r = 5, c = 0;
			var nCr = Enumerable.Range(n - r + 1, r).Aggregate((x, y) => x * y) / Enumerable.Range(1, r).Aggregate((x, y) => x * y);
			Combinatorics.Combination(Enumerable.Range(0, n).ToArray(), r, p => ++c);
			Assert.AreEqual(nCr, c);
		}

		[TestMethod]
		public void Combination_r()
		{
			for (int n = 6, r = 0; r <= n; r++, Console.WriteLine())
				Combinatorics.Combination(Enumerable.Range(1, n).ToArray(), r, p => Console.WriteLine(string.Join(" ", p)));

			Combinatorics.Combination(new[] { 9, 7, 5, 3, 1 }, 3, p => Console.WriteLine(string.Join(" ", p)));
		}

		[TestMethod]
		public void Power()
		{
			int n = 10, r = 7, c = 0;
			var pow = Enumerable.Repeat(n, r).Aggregate((x, y) => x * y);
			Combinatorics.Power(Enumerable.Range(0, n).ToArray(), r, p => ++c);
			Assert.AreEqual(pow, c);
		}

		[TestMethod]
		public void Power_r()
		{
			for (int n = 3, r = 0; r <= 4; r++, Console.WriteLine())
				Combinatorics.Power(Enumerable.Range(1, n).ToArray(), r, p => Console.WriteLine(string.Join(" ", p)));

			Combinatorics.Power(new[] { 9, 7, 5, 3, 1 }, 2, p => Console.WriteLine(string.Join(" ", p)));
		}
	}
}
