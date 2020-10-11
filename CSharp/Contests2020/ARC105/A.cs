using System;
using System.Linq;

class A
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		if (h.Sum() % 2 == 1) { Console.WriteLine("No"); return; }
		var s = h.Sum() / 2;

		if (h.Any(x => x == s)) { Console.WriteLine("Yes"); return; }

		for (int i = 0; i < 4; i++)
			for (int j = i + 1; j < 4; j++)
				if (h[i] + h[j] == s) { Console.WriteLine("Yes"); return; }
		Console.WriteLine("No");
	}
}
