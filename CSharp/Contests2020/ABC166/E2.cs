using System;

class E2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

		var r = 0L;
		var c = new int[n];
		for (int i = 0; i < n; i++)
		{
			if (i - a[i] >= 0) r += c[i - a[i]];
			if (i + a[i] < n) c[i + a[i]]++;
		}
		Console.WriteLine(r);
	}
}
