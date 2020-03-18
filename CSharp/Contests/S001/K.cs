using System;
using System.Linq;

class K
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var M = 1000000007;

		var f = new long[n + 1];
		f[0] = 1;
		for (int i = 0; i < n; i++)
			f[i + 1] = f[i] * (i + 1) % M;

		var r = 0L;
		var st = new SegmentTree(n + 1);
		for (int i = 0; i < n; i++)
		{
			var c = st.Sum(0, a[i]);
			r = (r + (a[i] - c - 1) * f[n - i - 1]) % M;
			st.Add(a[i], 1);
		}
		Console.WriteLine(r + 1);
	}
}
