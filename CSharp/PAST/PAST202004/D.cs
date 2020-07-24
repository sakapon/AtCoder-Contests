using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main()
	{
		var s = Console.ReadLine();
		var n = s.Length;
		var s2 = Enumerable.Range(0, n - 1).Select(i => s.Substring(i, 2)).ToArray();
		var s3 = Enumerable.Range(0, Math.Max(0, n - 2)).Select(i => s.Substring(i, 3)).ToArray();

		var l = new List<string>();
		bool[] tf = { true, false };

		foreach (var b0 in tf)
			foreach (var t in s)
				l.Add($"{(b0 ? t : '.')}");

		foreach (var b0 in tf)
			foreach (var b1 in tf)
				foreach (var t in s2)
					l.Add($"{(b0 ? t[0] : '.')}{(b1 ? t[1] : '.')}");

		foreach (var b0 in tf)
			foreach (var b1 in tf)
				foreach (var b2 in tf)
					foreach (var t in s3)
						l.Add($"{(b0 ? t[0] : '.')}{(b1 ? t[1] : '.')}{(b2 ? t[2] : '.')}");

		Console.WriteLine(l.Distinct().Count());
	}
}
