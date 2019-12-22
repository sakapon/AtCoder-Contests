using System;
using System.Linq;

class D
{
	static void Main()
	{
		Console.ReadLine();
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();

		int r = 0, t = 1;
		foreach (var v in a)
		{
			if (v == t)
			{
				r++;
				t++;
			}
		}
		Console.WriteLine(r == 0 ? -1 : a.Length - r);
	}
}
