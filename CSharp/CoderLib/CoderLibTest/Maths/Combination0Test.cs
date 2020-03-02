using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Maths
{
	[TestClass]
	public class Combination0Test
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

		const string Letters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
		static void EnumStrings(string s, int length, Action<string> action)
		{
			if (s.Length == length)
				action(s);
			else
				foreach (var c in Letters)
					EnumStrings(s + c, length, action);
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
		public void EnumStrings()
		{
			var c = 0;
			EnumStrings("", 4, s => c++);
			Console.WriteLine(c);
		}
		#endregion
	}
}
