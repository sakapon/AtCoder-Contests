using System;

class D3
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var c = new int[n + 1];
		c[0] = 1;
		for (int i = 0; i < n; i++)
			c[int.Parse(Console.ReadLine())]++;
		Console.WriteLine(Array.TrueForAll(c, x => x == 1) ? "Correct" : $"{Array.IndexOf(c, 2)} {Array.IndexOf(c, 0)}");
	}
}
