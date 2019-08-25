using System;
using System.Collections;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		for (var i = 0; i < n; i++)
		{
			for (var j = i + 1; j < n; j++) Console.Write($"{FindDiff(i, j) + 1} ");
			Console.WriteLine();
		}
	}

	static int FindDiff(int x, int y)
	{
		var z = new BitArray(new[] { x ^ y });
		for (var i = 0; ; i++) if (z[i]) return i;
	}
}
