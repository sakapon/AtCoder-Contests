using System;
using System.IO;

class B0
{
	static void Main()
	{
		const long max = 1 << 10;
		using var writer = File.CreateText("B0.txt");

		var s = new long[2 * max];
		for (int i = 1; i < s.Length; i++)
			s[i] = s[i - 1] + i;

		for (int n = 1; n <= max; n++)
		{
			for (int k = 1; k < s.Length; k++)
			{
				if (s[k] % n == 0)
				{
					writer.WriteLine($"{n}: {k}");
					break;
				}
			}
		}
	}
}
