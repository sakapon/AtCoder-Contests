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

	public static IEnumerable<int[]> Perm2(int n)
	{
		for (var i = 0; i < n; i++)
			for (var j = 0; j < n; j++)
				if (i != j) yield return new[] { i, j };
	}

	public static IEnumerable<int[]> Perm3(int n)
	{
		for (var i = 0; i < n; i++)
			for (var j = 0; j < n; j++)
				if (i != j)
					for (var k = 0; k < n; k++)
						if (i != k && j != k) yield return new[] { i, j, k };
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
	#endregion
}
