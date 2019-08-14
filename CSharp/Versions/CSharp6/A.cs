using System;
using System.Collections.Generic;
using System.Linq;
// using static
using static System.Console;
using static System.Math;

class A
{
	static void Main()
	{
		var a = ReadLine().Split().Select(int.Parse).ToArray();
		WriteLine(string.Join(" ", a));
		WriteLine(Environment.CurrentDirectory);

		Func<int, string> ToText = i => $"{i}";
		// ?. Operator
		var length = a.Select(ToText).FirstOrDefault()?.Length ?? 0;

		var v = new V(a[0], a[1]);
		// String Interpolation
		WriteLine($"{v.X} {v.Y} {v.Norm:F3}");

		// Index Initializer
		var d1 = new Dictionary<int, string>
		{
			[123] = "123",
			[456] = "456",
		};
		var d2 = new Dictionary<int, string> { 123, 456 };

		// C# 7
		//var M = 1_000_000_007;
		//WriteLine((2, -3));
		//if (int.TryParse("-234", out var r)) WriteLine(r);
		//if (Enumerable.Empty<int>() is int[] c) WriteLine(c.Length % M);
		//string ToText(int i) => $"{i}";
	}
}

struct V
{
	public int X, Y;
	// Expression-bodied Property
	public double Norm => Sqrt(X * X + Y * Y);
	public V(int x, int y) { X = x; Y = y; }
}

class C
{
	// Property Initializer
	public int Id { get; } = 1;
	// Expression-bodied Method
	// nameof Operator
	public string GetName() => $"{nameof(Id)}: {Id}";
}

static class E
{
	// Collection Initializer by Extension
	public static void Add(this Dictionary<int, string> d, int i) => d[i] = $"{i}";
}
