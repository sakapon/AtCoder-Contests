using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static void Main()
	{
		var s = Console.ReadLine();

		var sis = new List<int> { 0 };
		for (int i = 1; i < s.Length; i++)
			if (char.IsUpper(s[i - 1]) && char.IsUpper(s[i]) && sis.Last() < i - 1) sis.Add(i);
		sis.Add(s.Length);

		var ls = Enumerable.Range(1, sis.Count - 1).Select(i => sis[i] - sis[i - 1]).ToArray();
		Console.WriteLine(string.Join("", ls.Select((l, i) => s.Substring(sis[i], l)).OrderBy(x => x)));
	}
}
