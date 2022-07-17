using System;
using System.Collections.Generic;

class C
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var k = long.Parse(Console.ReadLine());
		return k.ConvertAsString(2).Replace("1", "2");
	}
}

public static class PositionalNotation
{
	public static int[] Convert(this long x, int b)
	{
		var r = new List<int>();
		for (; x > 0; x /= b) r.Add((int)(x % b));
		return r.ToArray();
	}

	const string AN = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

	public static string ConvertAsString(this long x, int b)
	{
		if (x == 0) return "0";
		if (x < 0) return "-" + ConvertAsString(-x, b);
		var r = Convert(x, b);
		Array.Reverse(r);
		return new string(Array.ConvertAll(r, d => AN[d]));
	}
}
