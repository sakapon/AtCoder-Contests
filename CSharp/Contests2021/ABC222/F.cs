﻿using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Graphs.Int.Trees.WeightedTree101;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => Read3());
		var d = Read();

		var g = new ListWeightedGraph(2 * n + 1, es, true);
		for (int v = 1; v <= n; v++)
		{
			g.AddEdge(v, n + v, true, d[v - 1]);
		}

		var rn = Enumerable.Range(1, n).ToArray();
		var rn2 = Enumerable.Range(1, n * 2).ToArray();

		var tree1 = g.BFSTree(n + 1);
		var root = rn2.FirstMax(v => tree1[v].Cost);
		tree1 = g.BFSTree(root);
		var end = rn2.FirstMax(v => tree1[v].Cost);
		var tree2 = g.BFSTree(end);

		var r = rn.Select(v =>
		{
			if (n + v == root)
			{
				return tree2[v].Cost;
			}
			else if (n + v == end)
			{
				return tree1[v].Cost;
			}
			else
			{
				return Math.Max(tree1[v].Cost, tree2[v].Cost);
			}
		});

		return string.Join("\n", r);
	}
}

public static class ArgHelper
{
	public static TSource FirstMax<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> toKey) where TKey : IComparable<TKey>
	{
		var e = source.GetEnumerator();
		if (!e.MoveNext()) throw new ArgumentException("The source is empty.", nameof(source));
		var (mo, mkey) = (e.Current, toKey(e.Current));
		while (e.MoveNext())
		{
			var key = toKey(e.Current);
			if (mkey.CompareTo(key) < 0) (mo, mkey) = (e.Current, key);
		}
		return mo;
	}
}
