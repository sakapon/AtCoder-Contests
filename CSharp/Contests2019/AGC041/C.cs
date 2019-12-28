using System;
using System.Linq;

class C
{
	static void Main()
	{
		var p3 = new[]
		{
			".aa",
			"a..",
			"a..",
		};
		var p4 = new[]
		{
			"aaef",
			"bbef",
			"ghcc",
			"ghdd",
		};
		var n = int.Parse(Console.ReadLine());
		if (n == 2 || n == 5) { Console.WriteLine(-1); return; }

		var c3 = n / 3 - n % 3;
		var c4 = n % 3;

		var r = new int[n].Select(_ => Enumerable.Repeat('.', n).ToArray()).ToArray();
		for (int k = 0; k < 3 * c3; k += 3)
			for (int i = 0; i < 3; i++)
				for (int j = 0; j < 3; j++)
					r[k + i][k + j] = p3[i][j];
		for (int k = 0; k < 4 * c4; k += 4)
			for (int i = 0; i < 4; i++)
				for (int j = 0; j < 4; j++)
					r[3 * c3 + k + i][3 * c3 + k + j] = p4[i][j];
		Console.WriteLine(string.Join("\n", r.Select(x => new string(x))));
	}
}
