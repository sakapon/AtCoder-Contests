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

		void Swap(char c, int i)
		{
			var ni = i + c - 'A' + 1;
			(a[i], a[ni]) = (a[ni], a[i]);
			r.Add($"{c} {i + 1}");
		}

		// 偶数を前に移動
		for (int i = 0; i < n; i += 2)
			for (int j = i - 2; j >= 0; j -= 2)
				if ((a[j] & 1) == 1 && (a[j + 2] & 1) == 0) Swap('B', j);
				else break;

		// 奇数を前に移動
		for (int i = 1; i < n; i += 2)
			for (int j = i - 2; j >= 0; j -= 2)
				if ((a[j] & 1) == 0 && (a[j + 2] & 1) == 1) Swap('B', j);
				else break;

		// 偶数と奇数を交換
		for (int i = 0; i < n; i += 2)
			if ((a[i] & 1) == 0) Swap('A', i);
			else break;

		for (int i = 0; i < n; i++)
			for (int j = i - 2; j >= 0; j -= 2)
				if (a[j] > a[j + 2]) Swap('B', j);
				else break;

		r.Insert(0, r.Count.ToString());
		return string.Join("\n", r);
	}
}
