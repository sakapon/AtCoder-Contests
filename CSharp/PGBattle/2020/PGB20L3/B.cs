using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var (n, s) = Read2();
		var sb = new StringBuilder();

		var a = new int[n];
		Dfs(0, 1, s);
		Console.Write(sb);

		// index i 以降で、m 以上の値で、合計 s。終了ならば true。
		void Dfs(int i, int m, int s)
		{
			if (i == n - 1)
			{
				a[^1] = s;
				sb.AppendLine(string.Join(" ", a));
				return;
			}

			for (int x = m; x <= s - x; x++)
			{
				a[i] = x;
				Dfs(i + 1, x, s - x);
			}
		}
	}
}
