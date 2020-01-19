using System;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var p = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

		int r = 0, m = n + 1;
		for (int i = 0; i < n; i++)
			if (p[i] <= (m = Math.Min(m, p[i]))) r++;
		Console.WriteLine(r);
	}
}
