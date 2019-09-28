using System.Collections.Generic;
using static System.Console;

class C
{
	static void Main()
	{
		var n = int.Parse(ReadLine());
		var s = ReadLine();

		var rs = new List<int>();
		int t = 0, o = 0;
		for (var i = 0; i < s.Length; i++)
		{
			if (s[i] == 'W' ^ t % 2 == 0)
			{
				if (o == n) { WriteLine(0); return; }
				t++;
				o++;
			}
			else
			{
				if (t == 0) { WriteLine(0); return; }
				t--;
				rs.Add(i);
			}
		}

		var r = 1L;
		for (var i = 0; i < n; i++) r = r * (rs[i] - 2 * i) * (i + 1) % 1000000007;
		WriteLine(r);
	}
}
