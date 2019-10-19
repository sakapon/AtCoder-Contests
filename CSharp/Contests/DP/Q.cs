using System;
using System.Collections.Generic;

class Q
{
	static void Main()
	{
		Func<int[]> read = () => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		var n = int.Parse(Console.ReadLine());
		var h = read();
		var a = read();

		var l = new List<int>() { 0 };
		var dp = new long[n + 1];
		for (int i = 0; i < n; i++)
		{
			var li = ~l.BinarySearch(h[i]);
			var v = dp[l[li - 1]] + a[i];
			while (li < l.Count && dp[l[li]] <= v) l.RemoveAt(li);
			l.Insert(li, h[i]);
			dp[h[i]] = v;
		}
		Console.WriteLine(dp[l[l.Count - 1]]);
	}
}
