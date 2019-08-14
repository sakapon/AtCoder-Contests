using System;
using System.Linq;
using static System.Console;

class A
{
	static void Main()
	{
		var a = ReadLine().Split().Select(int.Parse).ToArray();

		var length = a.Select(ToText).FirstOrDefault()?.Length ?? 0;

		// Literal
		var M = 1_000_000_007;

		// Value Tuple
		WriteLine((2, -3));

		// Out Var
		if (int.TryParse("-234", out var r)) WriteLine(r);

		// Pattern Matching
		if (Enumerable.Empty<int>() is int[] c) WriteLine(c.Length % M);

		// Local Function
		string ToText(int i) => $"{i}";
	}
}
