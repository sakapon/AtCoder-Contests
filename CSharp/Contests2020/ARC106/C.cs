using System;

class C
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		int n = h[0], m = h[1];

		if (m < 0 || n > 1 && m >= n - 1)
		{
			Console.WriteLine(-1);
			return;
		}

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		if (m == 0)
		{
			for (int i = 1; i <= n; i++)
			{
				Console.WriteLine($"{2 * i} {2 * i + 1}");
			}
		}
		else
		{
			for (int i = 1; i < n; i++)
			{
				Console.WriteLine($"{3 * i} {3 * i + 2}");
			}
			Console.WriteLine($"{1} {3 * m + 4}");
		}

		Console.Out.Flush();
	}
}
