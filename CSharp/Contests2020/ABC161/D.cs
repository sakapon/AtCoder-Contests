using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main()
	{
		var nexts = new[] { "01", "012", "123", "234", "345", "456", "567", "678", "789", "89" };
		var k = int.Parse(Console.ReadLine());

		if (k <= 9) { Console.WriteLine(k); return; }

		var t = 9;
		var ls = new List<List<string>> { Enumerable.Range(1, 9).Select(i => $"{i}").ToList() };
		for (int i = 0; ; i++)
		{
			var l = new List<string>();
			ls.Add(l);

			foreach (var s in ls[i])
			{
				foreach (var c in nexts[s[^1] - '0'])
				{
					l.Add(s + c);
					if (++t == k) { Console.WriteLine(l[^1]); return; }
				}
			}
		}
	}
}
