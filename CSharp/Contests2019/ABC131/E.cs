using System;

class E
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		int n = h[0], m = n * (n - 1) / 2 - h[1];

		if (m < n - 1) { Console.WriteLine(-1); return; }

		Console.WriteLine(m);
		for (int c = 0, i = 1; i <= n; i++)
			for (int j = i + 1; j <= n && c < m; j++, c++)
				Console.WriteLine($"{i} {j}");
	}
}
