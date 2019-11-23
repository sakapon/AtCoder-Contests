using System;

class F
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

		long M = 0, t = 0;
		for (int i = 1; i < n / 2; i++, t = 0)
			for (int l = 0, r = n - 1; i < r && (l < r || r % i != 0); l += i, r -= i)
				M = Math.Max(M, t += s[l] + s[r]);
		Console.WriteLine(M);
	}
}
