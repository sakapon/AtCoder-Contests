using System;

class Q053
{
	static int Query(int i)
	{
		Console.WriteLine($"? {i}");
		return int.Parse(Console.ReadLine());
	}
	static void Answer(int x) => Console.WriteLine($"! {x}");

	static void Main() => Array.ConvertAll(new bool[int.Parse(Console.ReadLine())], _ => Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());

		var fi = 17;
		var f = CreateSeq(fi);

		var a = Array.ConvertAll(new bool[f[fi] + 1], _ => -1);
		a[0] = -2;
		for (int i = n + 1; i <= f[fi]; i++)
			a[i] = -i;

		int GetValue(int i)
		{
			if (a[i] == -1) a[i] = Query(i);
			return a[i];
		}

		var l = 0;
		var r = f[fi];
		var m = f[--fi];
		GetValue(m);

		fi -= 2;
		while (fi-- > 0)
		{
			var t = l + r - m;
			if (m - l >= r - m)
			{
				if (GetValue(t) > a[m]) (m, r) = (t, m);
				else l = t;
			}
			else
			{
				if (GetValue(t) > a[m]) (m, l) = (t, m);
				else r = t;
			}
		}

		Answer(a[m]);
		return null;
	}

	static int[] CreateSeq(int nLast)
	{
		var a = new int[nLast + 1];
		a[1] = 1;
		for (int i = 2; i <= nLast; i++)
			a[i] = a[i - 1] + a[i - 2];
		return a;
	}
}
