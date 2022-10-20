using System;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ =>
		{
			var p = Read();
			return (x: p[0] + p[1] + 5, y: p[1] - p[0] + 3005, d: p[2]);
		});

		var a = new int[6010, 6010];
		foreach (var (x, y, _) in ps)
		{
			a[x, y] += 1;
		}
		var rsq = new StaticRSQ2C(a);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var (x, y, d) in ps)
		{
			var x1 = Math.Max(x - d - 1, 0);
			var y1 = Math.Max(y - d - 1, 0);
			var x2 = Math.Min(x + d, 6008);
			var y2 = Math.Min(y + d, 6008);
			Console.WriteLine(rsq.GetSum(x1, y1, x2, y2));
		}
		Console.Out.Flush();
	}
}

// 改造
// int 型、(-1, -1) が原点
public class StaticRSQ2C
{
	readonly int[,] a;
	public StaticRSQ2C(int[,] a)
	{
		this.a = a;
		var n1 = a.GetLength(0);
		var n2 = a.GetLength(1);

		for (int i = 0; i < n1; ++i)
		{
			var t = 0;
			for (int j = 0; j < n2; ++j)
			{
				a[i, j] = t += a[i, j];
				if (i != 0) a[i, j] += a[i - 1, j];
			}
		}
	}

	public long GetSum(int l1, int l2, int r1, int r2) => a[r1, r2] - a[l1, r2] - a[r1, l2] + a[l1, l2];
}
