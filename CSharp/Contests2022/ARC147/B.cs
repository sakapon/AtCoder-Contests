using System;
using System.Collections.Generic;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var r = new List<string>();

		// 偶数を前に移動
		for (int i = 0; i < n; i += 2)
			for (int j = i - 2; j >= 0; j -= 2)
				if ((a[j] & 1) == 1 && (a[j + 2] & 1) == 0)
				{
					(a[j], a[j + 2]) = (a[j + 2], a[j]);
					r.Add($"B {j + 1}");
				}
				else break;

		// 奇数を前に移動
		for (int i = 1; i < n; i += 2)
			for (int j = i - 2; j >= 0; j -= 2)
				if ((a[j] & 1) == 0 && (a[j + 2] & 1) == 1)
				{
					(a[j], a[j + 2]) = (a[j + 2], a[j]);
					r.Add($"B {j + 1}");
				}
				else break;

		// 偶数と奇数を交換
		for (int i = 0; i < n; i += 2)
			if ((a[i] & 1) == 0)
			{
				(a[i], a[i + 1]) = (a[i + 1], a[i]);
				r.Add($"A {i + 1}");
			}
			else break;

		for (int i = 0; i < n; i++)
			for (int j = i - 2; j >= 0; j -= 2)
				if (a[j] > a[j + 2])
				{
					(a[j], a[j + 2]) = (a[j + 2], a[j]);
					r.Add($"B {j + 1}");
				}
				else break;

		r.Insert(0, r.Count.ToString());
		return string.Join("\n", r);
	}
}
