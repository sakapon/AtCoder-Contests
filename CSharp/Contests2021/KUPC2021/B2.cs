using System;

class B2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var c = NewArray2(n, n, '.');

		for (int i = n - 2; i >= 0; i -= 2)
			for (int j = 0; j <= i; j++)
				c[i][j + 1] = c[j][i + 1] = '#';

		foreach (var r in c)
			Console.WriteLine(new string(r));
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
