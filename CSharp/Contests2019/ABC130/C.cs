using System;

class C
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), double.Parse);
		Console.WriteLine($"{h[0] * h[1] / 2} {(h[0] == 2 * h[2] && h[1] == 2 * h[3] ? 1 : 0)}");
	}
}
