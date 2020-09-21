using System;
using System.IO;

class B2
{
	static void Main()
	{
		Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });

		var s = Console.ReadLine();
		var p = Console.ReadLine();
		var n = s.Length;
		var m = p.Length;
		if (n < m) return;

		var M_Pow = Pow(M, m);
		var sh = RH_Hash(s, 0, m);
		var ph = RH_Hash(p, 0, m);
		if (sh == ph) Console.WriteLine(0);

		for (int i = 0; i + m < n; i++)
		{
			sh = M * sh - M_Pow * s[i] + s[i + m];
			if (sh == ph) Console.WriteLine(i + 1);
		}
		Console.Out.Flush();
	}

	const long M = 1000000007;
	static long RH_Append(long h, char c) => M * h + c;
	static long RH_Hash(string s, int start, int count)
	{
		var h = 0L;
		for (int i = 0; i < count; i++) h = RH_Append(h, s[start + i]);
		return h;
	}

	static long Pow(long b, long i)
	{
		for (var r = 1L; ; b *= b)
		{
			if ((i & 1) != 0) r *= b;
			if ((i >>= 1) == 0) return r;
		}
	}
}
