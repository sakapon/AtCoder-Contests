using System;
using System.Collections.Generic;
using System.Linq;

class Q067
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var z = Console.ReadLine().Split();
		var n = z[0];
		var k = int.Parse(z[1]);

		while (k-- > 0) n = Convert(n);
		return n;
	}

	static string Convert(string n)
	{
		return n.ConvertFrom(8).ConvertAsString(9).Replace('8', '5');
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
