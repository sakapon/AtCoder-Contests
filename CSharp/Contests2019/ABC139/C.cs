using System;
using System.Linq;

class C
{
	static void Main()
	{
		Console.ReadLine();
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();

		int c = 0, r = 0;
		for (int i = 0; i < h.Length - 1; i++)
		{
			if (h[i] >= h[i + 1])
			{
				c++;
			}
			else
			{
				r = Math.Max(r, c);
				c = 0;
			}
		}
		Console.WriteLine(Math.Max(r, c));
	}
}
