using System;
using System.Linq;

class E
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int n = h[0], k = h[1], c = h[2];
		var s = Console.ReadLine();

		var d1 = new int[k];
		for (int j = 0, i = 0; j < k; j++)
		{
			while (i < n && s[i] == 'x') i++;
			d1[j] = i;
			i += c + 1;
		}

		var d2 = new int[k];
		for (int j = k - 1, i = n - 1; j >= 0; j--)
		{
			while (i >= 0 && s[i] == 'x') i--;
			d2[j] = i;
			i -= c + 1;
		}

		Console.WriteLine(string.Join("\n", d1.Zip(d2, (x, y) => x == y ? x + 1 : 0).Where(x => x > 0)));
	}
}
