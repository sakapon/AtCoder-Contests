using System;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

		var s = new long[n + 1];
		for (int i = 0; i < n; ++i) s[i + 1] = s[i] + a[i];

		long sMax = 0, xMax = 0, t = 0;
		for (int i = 1; i <= n; i++)
		{
			sMax = Math.Max(sMax, s[i]);
			xMax = Math.Max(xMax, t + sMax);
			t += s[i];
		}
		Console.WriteLine(xMax);
	}
}
