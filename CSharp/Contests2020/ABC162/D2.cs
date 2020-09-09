using System;
using System.Linq;

class D2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var d = s.ToLookup(c => c);
		var r = d['R'].LongCount() * d['G'].Count() * d['B'].Count();
		for (int i = 0; i < n; i++)
			for (int j = i + 1; j < n; j++)
				if (2 * j - i < n && s[i] + s[j] + s[2 * j - i] == 'R' + 'G' + 'B') r--;
		Console.WriteLine(r);
	}
}
