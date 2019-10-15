using static System.Console;

class D
{
	static void Main()
	{
		var n = int.Parse(ReadLine());

		for (var i = 0; i < n; i++)
		{
			for (var j = i + 1; j < n; j++) Write($"{FindDiff(i, j) + 1} ");
			WriteLine();
		}
	}

	static int FindDiff(int x, int y)
	{
		var z = x ^ y;
		for (var i = 0; ; i++) if ((z >>= 1) == 0) return i;
	}
}
