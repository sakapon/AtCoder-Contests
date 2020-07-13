using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var d = new int[n + 1].Select(_ => new List<string>()).ToArray();
		d[0].Add("");

		for (int i = 1; i <= n; i++)
			foreach (var s in d[i - 1])
				foreach (var c in "abcdefghij")
				{
					var end = !s.Contains(c);
					d[i].Add(s + c);
					if (end) break;
				}
		Console.WriteLine(string.Join("\n", d[n]));
	}
}
