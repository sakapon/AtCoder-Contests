using System;
using System.Collections.Generic;
using System.Linq;

class M
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int d = h[0], l = h[1], n = h[2];
		var c = Read();
		var people = new int[n].Select(_ => Read()).ToArray();

		var dishDays = new List<int>[100001];
		for (int i = 0; i < d; i++)
		{
			var ds = dishDays[c[i]];
			if (ds == null) dishDays[c[i]] = ds = new List<int>();
			ds.Add(i + 1);
		}
		for (int i = 0; i <= 100000; i++)
		{
			var ds = dishDays[i];
			if (ds == null) continue;
			ds.AddRange(ds.Select(x => x + d).ToArray());
		}

		// 何回目に好みの料理を食べられるか。
		var dishCounts = new List<int>[100001];
		for (int i = 0; i <= 100000; i++)
		{
			var ds = dishDays[i];
			if (ds == null) continue;
			var dsc = ds.Count / 2;
			dishCounts[i] = new List<int> { (ds[dsc] - ds[dsc - 1] + l - 1) / l };
			for (int j = 1; j < ds.Count; j++)
				dishCounts[i].Add(dishCounts[i].Last() + (ds[j] - ds[j - 1] + l - 1) / l);
		}

		int GetCount(int[] p)
		{
			int k = p[0], f = p[1], t = p[2], r = 0;
			var days = dishDays[k];

			// 好みの料理が提供されない場合。
			if (days == null) return r;

			var likes = days.Count / 2;
			var periodCount = dishCounts[k].Last() / 2;

			// 初めて好みの料理を食べる日まで移動します。
			var i = days.BinarySearch(f);
			if (i < 0)
			{
				t -= (days[i = ~i] - f + l - 1) / l;
				i %= likes;
			}
			if (t <= 0) return r;

			// D 日周期を繰り返します。
			r += t / periodCount * likes;
			t %= periodCount;
			if (t <= 0) return r;

			// 最後の周回。
			t--; r++;
			if (t <= 0) return r;
			var j = dishCounts[k].BinarySearch(dishCounts[k][i] + t);
			if (j < 0) j = ~j - 1;
			return r + j - i;
		}

		Console.WriteLine(string.Join("\n", people.Select(GetCount)));
	}
}
