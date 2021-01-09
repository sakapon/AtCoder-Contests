using System;
using System.IO;

class B
{
	static void Main()
	{
		Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });

		var s = Console.ReadLine() + "$";
		var p = Console.ReadLine();
		var n = s.Length;
		var m = p.Length;
		if (n - 1 < m) return;

		long M = 1000000007;
		var M_Pow = Pow(M, m);

		long sh = 0, ph = 0;
		for (int i = 0; i < m; i++)
		{
			sh = M * sh + s[i];
			ph = M * ph + p[i];
		}

		for (int i = 0; i + m < n; i++)
		{
			// 厳密な比較は省略
			if (sh == ph) Console.WriteLine(i);
			sh = M * sh - M_Pow * s[i] + s[i + m];
		}
		Console.Out.Flush();
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
