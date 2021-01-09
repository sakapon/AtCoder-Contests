using System;
using System.Collections.Generic;
using System.Linq;

class B2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = new int[n].Select(_ => Console.ReadLine()).Select(s => new { s, v = s.Split() }).ToArray();
		Sort(a, _ => long.Parse(_.v[0]), _ => long.Parse(_.v[1]), _ => _.v[2][0], _ => long.Parse(_.v[3]), _ => ToLong(_.v[4]));
		Console.WriteLine(string.Join("\n", a.Select(_ => _.s)));
	}

	static long ToLong(string s)
	{
		var r = 0L;
		for (int i = 0; i < s.Length && i < 12; i++) r += (long)(s[i] - '`') << (5 * (11 - i));
		return r;
	}

	static void Sort<T, TKey>(T[] a, params Func<T, TKey>[] toKeys)
	{
		var ec = EqualityComparer<TKey>.Default;
		var keys = new TKey[a.Length];

		Action<int, int, int> Dfs = null;
		Dfs = (depth, start, end) =>
		{
			for (int i = start; i < end; ++i) keys[i] = toKeys[depth](a[i]);
			Array.Sort(keys, a, start, end - start);

			if (++depth == toKeys.Length) return;
			for (int s = start, i = start + 1; i <= end; ++i)
			{
				if (i < end && ec.Equals(keys[i - 1], keys[i])) continue;
				if (s != i - 1) Dfs(depth, s, i);
				s = i;
			}
		};
		Dfs(0, 0, a.Length);
	}
}
