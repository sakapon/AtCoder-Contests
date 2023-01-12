using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.DataTrees.SBTs;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var p = Read().Prepend(0).ToArray();

		var r = new int[n + 1];
		Array.Fill(r, int.MaxValue);

		{
			// 現在より小さいインデックスかつ小さい値
			var sbt = new MergeSBT<int>(n + 1, Monoid.Int32_Min);
			for (int i = 1; i <= n; i++)
			{
				var v = sbt[1, p[i]];
				if (v != int.MaxValue) ChFirstMin(ref r[i], v + p[i] + i);
				sbt[p[i]] = -p[i] - i;
			}
		}
		{
			// 現在より小さいインデックスかつ大きい値
			var sbt = new MergeSBT<int>(n + 1, Monoid.Int32_Min);
			for (int i = 1; i <= n; i++)
			{
				var v = sbt[p[i] + 1, n + 1];
				if (v != int.MaxValue) ChFirstMin(ref r[i], v - p[i] + i);
				sbt[p[i]] = p[i] - i;
			}
		}
		{
			// 現在より大きいインデックスかつ大きい値
			var sbt = new MergeSBT<int>(n + 1, Monoid.Int32_Min);
			for (int i = n; i >= 1; i--)
			{
				var v = sbt[p[i] + 1, n + 1];
				if (v != int.MaxValue) ChFirstMin(ref r[i], v - p[i] - i);
				sbt[p[i]] = p[i] + i;
			}
		}
		{
			// 現在より大きいインデックスかつ小さい値
			var sbt = new MergeSBT<int>(n + 1, Monoid.Int32_Min);
			for (int i = n; i >= 1; i--)
			{
				var v = sbt[1, p[i]];
				if (v != int.MaxValue) ChFirstMin(ref r[i], v + p[i] - i);
				sbt[p[i]] = -p[i] + i;
			}
		}

		return string.Join(" ", r[1..]);
	}

	public static void ChFirstMin<T>(ref T o1, T o2) where T : IComparable<T> { if (o1.CompareTo(o2) > 0) o1 = o2; }
}
