using System;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var a = new int[n];
		a[0] = 1;

		for (int i = 1; i <= n; i++)
		{
			Console.WriteLine(string.Join(" ", a[..i]));

			for (int j = n - 1; j > 0; j--)
				a[j] += a[j - 1];
		}
	}
}
