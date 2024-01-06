class D
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());

		var s = NewArray2<string>(n, n);
		s[n / 2][n / 2] = "T";
		s[0][0] = "1";

		var (i, j) = (0, 0);
		var (di, dj) = (0, 1);

		for (int k = 2; k < n * n; k++)
		{
			var (ni, nj) = (i + di, j + dj);
			if (!(0 <= ni && ni < n && 0 <= nj && nj < n) || s[ni][nj] != null)
			{
				(di, dj) = Next(di, dj);
				(ni, nj) = (i + di, j + dj);
			}
			(i, j) = (ni, nj);
			s[i][j] = k.ToString();
		}

		return string.Join("\n", s.Select(r => string.Join(" ", r)));

		static (int, int) Next(int di, int dj)
		{
			if ((di, dj) == (0, 1)) return (1, 0);
			if ((di, dj) == (1, 0)) return (0, -1);
			if ((di, dj) == (0, -1)) return (-1, 0);
			return (0, 1);
		}
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
