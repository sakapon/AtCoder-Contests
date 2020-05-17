using System;
using System.Linq;

class D
{
	static void Main()
	{
		var s = Console.ReadLine();
		var n = s.Length;
		if (n == 1) { Console.WriteLine(2); return; }

		var s2 = Enumerable.Range(0, n - 1).Select(i => s.Substring(i, 2)).ToArray();
		var s22 = Enumerable.Range(0, n - 2).Select(i => $"{s[i]}{s[i + 2]}").ToArray();
		var s3 = Enumerable.Range(0, n - 2).Select(i => s.Substring(i, 3)).ToArray();

		var r = Math.Min(3, n);
		r += s.Distinct().Count();

		r += s.Skip(1).Distinct().Count();
		r += s.Take(n - 1).Distinct().Count();
		r += s2.Distinct().Count();

		r += s.Skip(2).Distinct().Count();
		r += s.Skip(1).Take(n - 2).Distinct().Count();
		r += s.Take(n - 2).Distinct().Count();
		r += s2.Skip(1).Distinct().Count();
		r += s2.Take(n - 2).Distinct().Count();
		r += s22.Distinct().Count();
		r += s3.Distinct().Count();

		Console.WriteLine(r);
	}
}
