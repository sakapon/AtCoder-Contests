using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine().ToCharArray();

		Array.Reverse(s);
		var sb = new StringBuilder();

		for (int i = 0; i < s.Length; i++)
		{
			if (i > 0 && i % 3 == 0)
			{
				sb.Append(',');
			}
			sb.Append(s[i]);
		}

		return string.Join("", sb.ToString().Reverse());
	}
}
