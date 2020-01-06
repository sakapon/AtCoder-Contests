using System;
using System.Linq;

class E
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var a = read();
		int n = h[0], k = h[1];

		var s = new int[n + 1];
		for (int i = 0; i < n; i++) s[i + 1] = (s[i] + a[i] - 1) % k;

		var r = 0L;
		var d = new int[0].ToDictionary(_ => _);
		for (int i = 0; i <= n; i++)
		{
			if (i >= k) d[s[i - k]]--;
			if (!d.ContainsKey(s[i])) d[s[i]] = 0;
			r += d[s[i]];
			d[s[i]]++;
		}
		Console.WriteLine(r);
	}
}
