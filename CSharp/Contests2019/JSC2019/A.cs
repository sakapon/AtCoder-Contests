using System;
using System.Linq;

class A
{
	static void Main()
	{
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int m = a[0], d = a[1];

		var c = 0;
		for (int i = 4; i <= m; i++)
			for (int j = 1; j <= d; j++)
			{
				int d1 = j % 10, d2 = j / 10;
				if (d1 >= 2 && d2 >= 2 && i == d1 * d2) c++;
			}
		Console.WriteLine(c);
	}
}
