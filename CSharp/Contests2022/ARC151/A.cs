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

		var s1 = s.Count(c => c == '1');
		var t1 = t.Count(c => c == '1');
		var d = s1 + t1;
		if (d % 2 != 0) return -1;

		var m1s = Math.Min(s1, t1);
		var m1t = m1s;
		for (int i = 0; i < n; i++)
		{
			if (s[i] == '1' && m1s > 0)
			{
				s[i] = '0';
				m1s--;
				s1--;
			}
			if (t[i] == '1' && m1t > 0)
			{
				t[i] = '0';
				m1t--;
				t1--;
			}
		}

		if (s1 == 0)
		{
			s = t;
			s1 = t1;
		}

		m1s = s1 / 2;
		for (int i = 0; i < n; i++)
		{
			if (s[i] == '1' && m1s > 0)
			{
				s[i] = '0';
				m1s--;
				s1--;
			}
		}

		return new string(s);
	}
}
