using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var a = NewArray2<int>(n, n);

		for (int i = 0; i < n; i++)
		{
			a[i][0] = 1;
		}

		for (int i = 1; i < n; i++)
		{
			for (int j = 1; j < n; j++)
			{
				a[i][j] = a[i - 1][j - 1] + a[i - 1][j];
			}
		}

		for (int i = 0; i < n; i++)
		{
			Console.WriteLine(string.Join(" ", a[i][..(i + 1)]));
		}
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
