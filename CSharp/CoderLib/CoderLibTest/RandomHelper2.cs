using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CoderLibTest
{
	// https://hello-world-494ec.firebaseapp.com/
	public static class RandomHelper2
	{
		static readonly Random random = new Random();

		public static IEnumerable<TSource> Permutation<TSource>(this IEnumerable<TSource> source)
		{
			var a = source?.ToArray() ?? throw new ArgumentNullException(nameof(source));

			for (int i = a.Length - 1; i >= 0; --i)
			{
				int index = random.Next(i + 1);
				yield return a[index];
				(a[i], a[index]) = (a[index], a[i]);
			}
		}

		public static IEnumerable<(int lv, int uv)> TreeFrom1(int vCount, double starRate = 0.5)
		{
			var p = Enumerable.Range(1, vCount).Permutation().GetEnumerator();
			p.MoveNext();
			var v = p.Current;

			var q = new Queue<int>();
			var r = (int)(starRate * 1000);

			while (p.MoveNext())
			{
				var v2 = p.Current;
				yield return v < v2 ? (v, v2) : (v2, v);
				q.Enqueue(v2);
				if (random.Next(1000) >= r) v = q.Dequeue();
			}
		}

		public static IEnumerable<(int lv, int uv)> GraphFrom1(int vCount, int eCount)
		{
			var l = new List<(int, int)>();
			for (int i = 1; i <= vCount; ++i)
				for (int j = i + 1; j <= vCount; ++j)
					l.Add((i, j));
			return l.Permutation().Take(eCount);
		}

		public static IEnumerable<(int lv, int uv)> ConnectedGraphFrom1(int vCount, int eCount, double starRate = 0.5)
		{
			var l = new List<(int, int)>();
			for (int i = 1; i <= vCount; ++i)
				for (int j = i + 1; j <= vCount; ++j)
					l.Add((i, j));
			var tree = TreeFrom1(vCount, starRate).ToArray();
			return l.Except(tree).Permutation().Take(eCount - tree.Length).Concat(tree);
		}

		public static string ToString(int vCount, int eCount, IEnumerable<(int lv, int uv)> edges)
		{
			return $"{vCount} {eCount}\n{string.Join("\n", edges.Select(e => $"{e.lv} {e.uv}"))}";
		}

		public static void SetInputToConsole(string input)
		{
			Console.SetIn(new StringReader(input));
		}
	}
}
