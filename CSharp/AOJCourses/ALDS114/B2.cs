using System;
using System.IO;

class B2
{
	static void Main()
	{
		Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		long M = 1000000007;

		var s = Console.ReadLine();
		var p = Console.ReadLine();
		var n = s.Length;
		var m = p.Length;
		if (n < m) return;

		var sh = new RH(s, M);
		var ph = RH.Hash(p, M);

		for (int i = 0; i + m <= n; i++)
			if (sh.Hash(i, m) == ph) Console.WriteLine(i);
		Console.Out.Flush();
	}
}
