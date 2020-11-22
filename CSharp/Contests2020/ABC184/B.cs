using System;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], x = h[1];
		var s = Console.ReadLine();

		foreach (var c in s)
		{
			if (c == 'o')
			{
				x++;
			}
			else
			{
				if (x > 0) x--;
			}
		}
		Console.WriteLine(x);
	}
}
