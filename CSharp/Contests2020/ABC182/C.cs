using System;
using System.Linq;

class C
{
	static void Main()
	{
		var s = Console.ReadLine();
		var n = long.Parse(s);

		var cs = s.Select(c => c - '0').ToArray();
		var c1 = cs.Count(c => c % 3 == 1);
		var c2 = cs.Count(c => c % 3 == 2);

		if (n % 3 == 0)
		{
			Console.WriteLine(0);
		}
		else if (n % 3 == 1)
		{
			var r = c1 > 0 ? 1 : c2 > 1 ? 2 : -1;
			Console.WriteLine(r == s.Length ? -1 : r);
		}
		else
		{
			var r = c2 > 0 ? 1 : c1 > 1 ? 2 : -1;
			Console.WriteLine(r == s.Length ? -1 : r);
		}
	}
}
