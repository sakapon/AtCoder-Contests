using System;

class B
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		while (true)
		{
			if ((h[2] -= h[1]) <= 0) { Console.WriteLine("Yes"); return; }
			if ((h[0] -= h[3]) <= 0) { Console.WriteLine("No"); return; }
		}
	}
}
