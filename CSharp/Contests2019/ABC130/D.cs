using System;

class D
{
	static void Main()
	{
		Func<long[]> read = () => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		var h = read();
		var a = read();
		long n = h[0], k = h[1];

		var j = 0;
		long c = 0, s = 0;
		for (int i = 0; i < n; c += n - j + 1, s -= a[i++])
		{
			for (; j < n && s < k; s += a[j++]) ;
			if (s < k) break;
		}
		Console.WriteLine(c);
	}
}
