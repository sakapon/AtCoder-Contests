using System;
using System.Collections.Generic;
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

	[TestMethod]
	public void Comb2()
	{
		foreach (var a in Comb2(5))
			Console.WriteLine(string.Join(" ", a));
		foreach (var a in Comb2(6))
			Console.WriteLine(string.Join(" ", a));
	}

	[TestMethod]
	public void Comb3()
	{
		foreach (var a in Comb3(5))
			Console.WriteLine(string.Join(" ", a));
		foreach (var a in Comb3(6))
			Console.WriteLine(string.Join(" ", a));
	}
}
