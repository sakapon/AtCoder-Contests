using System;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = new int[n].Select(_ => Console.ReadLine().ToArray()).ToArray();

		for (int i = n - 2; i >= 0; i--)
			for (int j = n - 1 - i; j <= n - 1 + i; j++)
				if (s[i + 1][j - 1] == 'X' || s[i + 1][j] == 'X' || s[i + 1][j + 1] == 'X')
					s[i][j] = 'X';
		Console.WriteLine(string.Join("\n", s.Select(cs => new string(cs))));
	}
}
