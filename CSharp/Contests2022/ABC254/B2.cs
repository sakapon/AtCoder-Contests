using System;

class B2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var a = new int[n][];
		a[0] = new[] { 1 };

		for (int i = 1; i < n; i++)
		{
			a[i] = new int[i + 1];
			a[i][0] = a[i][i] = 1;

			for (int j = 1; j < i; j++)
				a[i][j] = a[i - 1][j - 1] + a[i - 1][j];
		}

		for (int i = 0; i < n; i++)
			Console.WriteLine(string.Join(" ", a[i]));
	}
}
