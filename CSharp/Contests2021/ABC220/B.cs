using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var k = int.Parse(Console.ReadLine());
		var ab = Console.ReadLine().Split();

		var a = ab[0].ConvertFrom(k);
		var b = ab[1].ConvertFrom(k);
		return a * b;
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

	public static long ConvertFrom(this int[] a, int b)
	{
		var r = 0L;
		for (int i = a.Length - 1; i >= 0; --i) r = r * b + a[i];
		return r;
	}

	const string AN = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
	static readonly Dictionary<char, int> ANMap = AN.Select((c, i) => (c, i)).ToDictionary(_ => _.c, _ => _.i);

	public static string ConvertAsString(this long x, int b)
	{
		if (x == 0) return "0";
		if (x < 0) return "-" + ConvertAsString(-x, b);
		var r = Convert(x, b);
		Array.Reverse(r);
		return new string(Array.ConvertAll(r, d => AN[d]));
	}

	public static long ConvertFrom(this string s, int b)
	{
		if (s.StartsWith('-')) return -ConvertFrom(s.Substring(1), b);
		var a = Array.ConvertAll(s.ToCharArray(), c => ANMap[c]);
		Array.Reverse(a);
		return ConvertFrom(a, b);
	}
}
