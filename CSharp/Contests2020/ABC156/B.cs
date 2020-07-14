using System;

class B
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		Console.WriteLine(Math.Floor(Math.Log(h[0], h[1])) + 1);
	}
}
