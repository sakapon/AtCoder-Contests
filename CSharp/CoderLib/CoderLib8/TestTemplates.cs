using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CoderLib8
{
	class Test_Array
	{
		const int n = 6;
		static void Main()
		{
			using (var writer = File.CreateText("F.txt"))
			{
				// Permutation
				Power(Enumerable.Range(1, n).ToArray(), n, a =>
				{
					var expected = Naive(n, a);
					var actual = Solve(n, a);
					if (expected == actual) return;

					writer.WriteLine(string.Join(" ", a));
					writer.WriteLine($"expected: {expected}, actual: {actual}");
				});
			}
		}

		static int Solve(int n, int[] a)
		{
			return 0;
		}

		static int Naive(int n, int[] a)
		{
			return 0;
		}

		static void Power<T>(T[] values, int r, Action<T[]> action)
		{
			var n = values.Length;
			var p = new T[r];

			if (r > 0) Dfs(0);
			else action(p);

			void Dfs(int i)
			{
				var i2 = i + 1;
				for (int j = 0; j < n; ++j)
				{
					p[i] = values[j];

					if (i2 < r) Dfs(i2);
					else action(p);
				}
			}
		}
	}
}
