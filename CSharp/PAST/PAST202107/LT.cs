using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.DataTrees.Bsts;

class LT
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var (n, qc) = Read2();
		var a = Read();
		var qs = Array.ConvertAll(new bool[qc], _ => Read3());

		// a の値となりうるインデックスのセット
		var map = new Dictionary<int, AvlSortedSet<int>>();
		void AddPair(int i, int v)
		{
			if (!map.ContainsKey(v)) map[v] = new AvlSortedSet<int>();
			map[v].Add(i);
		}

		for (int i = 0; i < n; i++)
		{
			AddPair(i, a[i]);
		}
		foreach (var (t, x, y) in qs)
		{
			if (t == 1) AddPair(x - 1, y);
		}

		var st = new ST1<int>(n, Math.Min, int.MaxValue, a);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var q in qs)
		{
			var (t, x, y) = q;
			x--;

			if (t == 1)
			{
				a[x] = y;
				st.Set(x, y);
			}
			else
			{
				var p = st.Get(x, y);

				var r = map[p].GetItems(v => v >= x, v => v < y).Where(i => a[i] == p).Select(i => i + 1).ToArray();
				Console.WriteLine($"{r.Length} " + string.Join(" ", r));
			}
		}
		Console.Out.Flush();
	}
}

namespace CoderLib6.DataTrees.Bsts
{
	public class AvlSortedSet<T>
	{
		public IEnumerable<T> GetItems(Func<T, bool> predicateForMin, Func<T, bool> predicateForMax) => throw new NotImplementedException();
		public bool Add(T item) => throw new NotImplementedException();
	}
}
