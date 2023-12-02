using System;
using System.Collections.Generic;
using System.Linq;

class EL
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var a = Read();
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var set = new MexMultiset();
		foreach (var x in a)
		{
			set.Add(x);
		}

		var r = new List<int>();

		foreach (var q in qs)
		{
			var i = q.Item1 - 1;
			var x = q.Item2;

			set.Remove(a[i]);
			a[i] = x;
			set.Add(a[i]);
			r.Add(set.Mex);
		}
		return string.Join("\n", r);
	}
}

[System.Diagnostics.DebuggerDisplay(@"Mex = {Mex}")]
public class MexMultiset
{
	readonly int max;
	readonly int[] counts;
	readonly SortedSet<int> set;

	public MexMultiset(int max = 1 << 18)
	{
		this.max = max;
		counts = new int[max];
		set = new SortedSet<int>(Enumerable.Range(0, max));
	}

	public int Mex => set.Count == 0 ? max : set.Min;

	public bool Add(int value)
	{
		if (value < 0 || max <= value) return false;
		if (counts[value]++ == 0) set.Remove(value);
		return true;
	}

	public bool Remove(int value)
	{
		if (value < 0 || max <= value) return false;
		if (counts[value] == 0) return false;
		if (--counts[value] == 0) set.Add(value);
		return true;
	}

	public int GetCount(int value)
	{
		if (value < 0 || max <= value) return -1;
		return counts[value];
	}
}
