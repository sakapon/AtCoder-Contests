using System;
using System.Linq;

class A
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var rn = Enumerable.Range(0, n).ToArray();
		var s = rn.Select(_ => Console.ReadLine().Select(c => c - '0').ToArray()).ToArray();

		for (int i = 0; i < n; i++)
			s[i][i] = 1;

		for (int k = 0; k < n; k++)
			for (int i = 0; i < n; i++)
				for (int j = 0; j < n; j++)
					s[i][j] |= s[i][k] & s[k][j];

		Console.WriteLine(rn.Sum(j => 1.0 / rn.Sum(i => s[i][j])));
	}
}
