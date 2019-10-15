using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class CombinationTest
{
	static IEnumerable<int[]> Comb2(int n)
	{
		for (var i = 0; i < n; i++)
			for (var j = i + 1; j < n; j++)
				yield return new[] { i, j };
	}

	static IEnumerable<int[]> Comb3(int n)
	{
		for (var i = 0; i < n; i++)
			for (var j = i + 1; j < n; j++)
				for (var k = j + 1; k < n; k++)
					yield return new[] { i, j, k };
	}

	static IEnumerable<int[]> Perm2(int n)
	{
		for (var i = 0; i < n; i++)
			for (var j = 0; j < n; j++)
				if (i != j) yield return new[] { i, j };
	}

	static IEnumerable<int[]> Perm3(int n)
	{
		for (var i = 0; i < n; i++)
			for (var j = 0; j < n; j++)
				if (i != j)
					for (var k = 0; k < n; k++)
						if (i != k && j != k) yield return new[] { i, j, k };
	}

	static IEnumerable<int[]> Comb(int[] vs, int r) => Comb(vs.Length, r).Select(x => x.Select(i => vs[i]).ToArray());
	static IEnumerable<int[]> Comb(int n, int r) => Comb(n, r, 0, r);
	// s: 開始番号, k: 元の r
	static IEnumerable<int[]> Comb(int n, int r, int s, int k)
	{
		if (r == 0) return new[] { new int[k] };
		if (r == 1) return Enumerable.Range(s, n).Select(i => { var a = new int[k]; a[k - 1] = i; return a; });

		return Enumerable.Range(s, n - r + 1).SelectMany(i => Comb(s + n - i - 1, r - 1, i + 1, k).Select(a => { a[k - r] = i; return a; }));
	}

	static IEnumerable<int[]> Perm(int[] vs, int r) => Perm(vs.Length, r).Select(x => x.Select(i => vs[i]).ToArray());
	static IEnumerable<int[]> Perm(int n, int r) => Perm(new SortedSet<int>(Enumerable.Range(0, n)), r, r);
	// k: 元の r
	static IEnumerable<int[]> Perm(SortedSet<int> set, int r, int k)
	{
		if (r == 0) return new[] { new int[k] };
		if (r == 1) return set.Select(i => { var a = new int[k]; a[k - 1] = i; return a; });

		return set.ToArray().SelectMany(i =>
		{
			set.Remove(i);
			var p = Perm(set, r - 1, k).Select(a => { a[k - r] = i; return a; }).ToArray();
			set.Add(i);
			return p;
		});
	}

	#region Test Methods

	[TestMethod]
	public void Comb2()
	{
		foreach (var a in Comb2(5))
			Console.WriteLine(string.Join(" ", a));
		foreach (var a in Comb2(6))
			Console.WriteLine(string.Join(" ", a));

		var n = 5;
		foreach (var _ in Enumerable.Range(0, n).SelectMany(i => Enumerable.Range(i + 1, n - i - 1).Select(j => new { i, j })))
			Console.WriteLine($"{_.i} {_.j}");
	}

	[TestMethod]
	public void Comb3()
	{
		foreach (var a in Comb3(5))
			Console.WriteLine(string.Join(" ", a));
		foreach (var a in Comb3(6))
			Console.WriteLine(string.Join(" ", a));
	}

	[TestMethod]
	public void Perm2()
	{
		foreach (var a in Perm2(5))
			Console.WriteLine(string.Join(" ", a));
		foreach (var a in Perm2(6))
			Console.WriteLine(string.Join(" ", a));
	}

	[TestMethod]
	public void Perm3()
	{
		foreach (var a in Perm3(5))
			Console.WriteLine(string.Join(" ", a));
		foreach (var a in Perm3(6))
			Console.WriteLine(string.Join(" ", a));
	}

	[TestMethod]
	public void Comb()
	{
		for (var r = 0; r <= 5; r++)
		{
			foreach (var a in Comb(5, r))
				Console.WriteLine(string.Join(" ", a));
			Console.WriteLine();
		}

		foreach (var a in Comb(new[] { 9, 7, 5, 3, 1 }, 3))
			Console.WriteLine(string.Join(" ", a));
	}

	[TestMethod]
	public void Perm()
	{
		for (var r = 0; r <= 4; r++)
		{
			foreach (var a in Perm(4, r))
				Console.WriteLine(string.Join(" ", a));
			Console.WriteLine();
		}

		foreach (var a in Perm(new[] { 9, 7, 5, 3 }, 3))
			Console.WriteLine(string.Join(" ", a));
	}
	#endregion
}
