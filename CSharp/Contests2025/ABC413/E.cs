class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var p = Read();

		for (int len = 1 << n; len > 1; len >>= 1)
		{
			var sec = (1 << n) / len;
			for (int a = 0; a < sec; a++)
			{
				var mi = Enumerable.Range(a * len, len).MinBy(i => p[i]);
				if (mi >= a * len + len / 2)
					Array.Reverse(p, a * len, len);
			}
		}
		return string.Join(" ", p);
	}
}
