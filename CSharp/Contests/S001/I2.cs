using System;
using System.Linq;

class I2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();

		int c = 0, s = 0;
		for (int l = 0, r = -1; l < n; l++)
		{
			while (s < n && r < n - 1) s += a[++r];
			if (s == n) c++;
			s -= a[l];
		}
		Console.WriteLine(c);
	}
}
