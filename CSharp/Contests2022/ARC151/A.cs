using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine().ToCharArray();
		var t = Console.ReadLine().ToCharArray();

		for (int i = 0; i < n; i++)
		{
			if (s[i] == t[i])
			{
				s[i] = t[i] = '0';
			}
		}

		var rn = Enumerable.Range(0, n).ToArray();
		var s1 = Array.FindAll(rn, i => s[i] == '1');
		var t1 = Array.FindAll(rn, i => t[i] == '1');

		if (s1.Length < t1.Length) (s1, t1) = (t1, s1);
		s1 = s1[t1.Length..];
		if (s1.Length % 2 != 0) return -1;
		s1 = s1[(s1.Length / 2)..];

		var r = new int[n];
		foreach (var i in s1)
		{
			r[i] = 1;
		}
		return string.Join("", r);
	}
}
