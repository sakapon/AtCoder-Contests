using System;

class B
{
	static void Main()
	{
		var a = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		var x = new int[11];
		x[0] = a[2];
		for (int i = 1; i <= 10; i++)
		{
			x[i] = a[0] * x[i - 1] - a[1];
			Console.WriteLine(x[i]);
		}
	}
}
