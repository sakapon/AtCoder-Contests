using System;
using System.Linq;

class D
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var s = Console.ReadLine();
		int n = h[0], k = h[1];

		if (n == 1) { Console.WriteLine(0); return; }
		var x = Enumerable.Range(1, n - 2).Count(i => s[i] == 'L' ? s[i - 1] == 'L' : s[i + 1] == 'R');
		if (s.StartsWith("RR")) x++;
		if (s.EndsWith("LL")) x++;

		var f = Enumerable.Range(0, n - 1).Count(i => s[i] == 'R' && s[i + 1] == 'L');
		if (s[0] == s[n - 1])
		{
			x += 2 * Math.Min(k, f);
		}
		else if (s[0] == 'L')
		{
			x += 2 * Math.Min(k, f);
			if (k > f) x++;
		}
		else
		{
			x += 2 * Math.Min(k, f - 1);
			if (k >= f) x++;
		}
		Console.WriteLine(x);
	}
}
