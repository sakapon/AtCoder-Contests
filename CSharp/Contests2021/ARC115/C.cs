using System;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var a = new int[n + 1];
		for (int i = 1; i <= n; ++i)
		{
			a[i] = a[i - 1] + (a[a[i]] == a[i - 1] ? 1 : 0);

			for (int j = i + i; j <= n; j += i)
				a[j] = i;
		}

		Console.WriteLine(string.Join(" ", a[1..]));
	}
}
