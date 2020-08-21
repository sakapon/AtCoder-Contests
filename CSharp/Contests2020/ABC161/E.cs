using System;
using System.Linq;

class E
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int n = h[0], k = h[1], c = h[2];
		var s = Console.ReadLine();

		var u1 = new bool[n];
		for (int j = 0, i = 0; j < k; j++)
		{
			for (; i < n && s[i] == 'x'; i++) ;
			u1[i] = true;
			i += c + 1;
		}

		var u2 = new bool[n];
		for (int j = 0, i = n - 1; j < k; j++)
		{
			for (; i >= 0 && s[i] == 'x'; i--) ;
			u2[i] = true;
			i -= c + 1;
		}

		var d1 = Enumerable.Range(1, n).Where(i => u1[i - 1]).ToArray();
		var d2 = Enumerable.Range(1, n).Where(i => u2[i - 1]).ToArray();
		Console.WriteLine(string.Join("\n", Enumerable.Range(0, d1.Length).Where(i => d1[i] == d2[i]).Select(i => d1[i])));
	}
}
