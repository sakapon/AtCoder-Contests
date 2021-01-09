using System;

class B
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		Console.WriteLine(h[0] <= h[3] && h[2] <= h[1] ? "Yes" : "No");
	}
}
