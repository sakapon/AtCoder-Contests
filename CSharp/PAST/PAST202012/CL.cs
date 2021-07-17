using System;
using System.Collections.Generic;

class CL
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		Console.WriteLine(ConvertAsString(n, 36));
	}

	public static int[] Convert(long x, int b)
	{
		var r = new List<int>();
		for (; x > 0; x /= b) r.Add((int)(x % b));
		return r.ToArray();
	}

	const string AN = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
	public static string ConvertAsString(long x, int b)
	{
		if (x == 0) return "0";
		if (x < 0) return "-" + ConvertAsString(-x, b);
		var r = Convert(x, b);
		Array.Reverse(r);
		return new string(Array.ConvertAll(r, d => AN[d]));
	}
}
