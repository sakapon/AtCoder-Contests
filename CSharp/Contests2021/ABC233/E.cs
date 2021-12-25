using System;
using System.Linq;

class E
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var x = Console.ReadLine().Select(c => (long)(c - '0')).ToArray();
		var n = x.Length;

		var r = new long[n];
		var s = x.Sum();
		var t = s;

		for (int i = n - 1; i >= 0; i--)
		{
			r[i] = t % 10;

			t /= 10;
			s -= x[i];
			t += s;
		}

		var rs = string.Join("", r);
		if (t > 0) rs = t + rs;
		return rs;
	}
}
