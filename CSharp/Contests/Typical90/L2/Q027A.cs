using System;
using System.Collections.Generic;

class Q027A
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var f = 1 << 18;
		var map = new List<string>[f];
		f--;

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		for (int i = 1; i <= n; i++)
		{
			var s = Console.ReadLine();
			var h = s.GetHashCode() & f;
			var l = map[h];
			if (l?.Contains(s) ?? false) continue;
			if (l is null) map[h] = new List<string> { s };
			else l.Add(s);
			Console.WriteLine(i);
		}
		Console.Out.Flush();
	}
}
