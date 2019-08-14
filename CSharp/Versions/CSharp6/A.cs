using System;
using System.Linq;
// using static
using static System.Console;
using static System.Math;

class A
{
	static void Main()
	{
		var a = ReadLine().Split().Select(int.Parse).ToArray();

		Func<int, string> ToText = i => $"{i}";
		// ?. Operator
		var length = a.Select(ToText).FirstOrDefault()?.Length ?? 0;
		WriteLine(Environment.CurrentDirectory);

		var v = new V(2, 3);
		// String Interpolation
		WriteLine($"{v.X} {v.Y} {v.Norm:F3}");

		// C# 7
		//WriteLine((0, 1));
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
	// String Interpolation
	// nameof Operator
	public string GetName() => $"{nameof(Id)}: {Id}";
}
