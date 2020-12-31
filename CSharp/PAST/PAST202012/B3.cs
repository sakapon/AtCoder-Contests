using System;
using System.Collections.Generic;
using System.Linq;

class B3
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var d = new Dictionary<char, int>();
		for (int i = 0; i < n; i++)
			d[s[i]] = i;
		Console.WriteLine(string.Join("", d.OrderBy(p => p.Value).Select(p => p.Key)));
	}
}
