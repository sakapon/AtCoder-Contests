using System;
using System.Collections.Generic;
using System.Linq;

class A2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();
		Sort(a, v => v[0], v => v[1]);
		Console.WriteLine(string.Join("\n", a.Select(v => $"{v[0]} {v[1]}")));
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
