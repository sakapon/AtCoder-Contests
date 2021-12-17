using System;

class A
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Array.ConvertAll(("0 " + Console.ReadLine()).Split(), int.Parse);

		for (int i = 1; i <= n; i++)
		{
			Console.Write($"node {i}: ");
			Console.Write($"key = {a[i]}, ");
			if (i / 2 > 0) Console.Write($"parent key = {a[i / 2]}, ");
			if (2 * i <= n) Console.Write($"left key = {a[2 * i]}, ");
			if (2 * i + 1 <= n) Console.Write($"right key = {a[2 * i + 1]}, ");
			Console.WriteLine();
		}
	}
}
