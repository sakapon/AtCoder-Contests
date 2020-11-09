using System;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var z = Read();
		int h = z[0], w = z[1];
		var s = Array.ConvertAll(new int[h], _ => Console.ReadLine());

		var c = NewArray2<int>(h + 2, w + 2);
		for (int i = 0; i < h; i++)
		{
			for (int j = 0; j < w; j++)
			{
				if (s[i][j] == '.') continue;

				c[i][j]++;
				c[i][j + 1]++;
				c[i][j + 2]++;
				c[i + 1][j]++;
				c[i + 1][j + 1]++;
				c[i + 1][j + 2]++;
				c[i + 2][j]++;
				c[i + 2][j + 1]++;
				c[i + 2][j + 2]++;
			}
		}

		foreach (var r in c.Skip(1).Take(h))
			Console.WriteLine(string.Join("", r.Skip(1).Take(w)));
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default(T)) => NewArrayF(n1, () => NewArray1(n2, v));
	static T[] NewArray1<T>(int n, T v = default(T))
	{
		var a = new T[n];
		for (int i = 0; i < n; ++i) a[i] = v;
		return a;
	}

	static T[] NewArrayF<T>(int n, Func<T> newItem)
	{
		var a = new T[n];
		for (int i = 0; i < n; ++i) a[i] = newItem();
		return a;
	}
}
