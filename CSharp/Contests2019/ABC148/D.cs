using System;
using System.Linq;

class D
{
	static void Main()
	{
		Console.ReadLine();
		int r = 0, t = 1;
		foreach (var v in Console.ReadLine().Split().Select(int.Parse))
			if (v == t) t++;
			else r++;
		Console.WriteLine(t == 1 ? -1 : r);
	}
}
