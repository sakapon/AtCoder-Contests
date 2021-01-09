using System;

class B
{
	static void Main()
	{
		long x = long.Parse(Console.ReadLine()), y = 0, c = 100;
		while (c < x)
		{
			y++;
			c += c / 100;
		}
		Console.WriteLine(y);
	}
}
