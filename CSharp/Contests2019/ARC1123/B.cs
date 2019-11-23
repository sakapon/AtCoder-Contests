using System;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);

		var s = new long[n + 1];
		for (int i = 0; i < n; i++) s[i + 1] = s[i] + a[i];

		var j = Array.BinarySearch(s, (s[n] + 1) / 2);
		if (j < 0) j = ~j;
		Console.WriteLine(Math.Min(s[n] - 2 * s[j - 1], 2 * s[j] - s[n]));
	}
}
