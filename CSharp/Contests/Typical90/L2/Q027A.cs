using System;
using System.Collections.Generic;

class Q027A
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var table = new List<string>[f + 1];

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		for (int i = 1; i <= n; i++)
		{
			var s = Console.ReadLine();
			var h = Hash(s) & f;
			var l = table[h];
			if (l != null && l.Contains(s)) continue;
			if (l == null) table[h] = new List<string> { s };
			else l.Add(s);
			Console.WriteLine(i);
		}
		Console.Out.Flush();
	}

	const int f = (1 << 18) - 1;

	static int Hash(string s)
	{
		var h = 0;
		foreach (var c in s) h = h * 11111117 + c;
		return h;
	}
}
