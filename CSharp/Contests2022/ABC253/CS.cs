using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class CS
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var qc = Read()[0];
		var qs = Array.ConvertAll(new bool[qc], _ => Read());
		var sb = new StringBuilder();

		var map = new IdMap<int>();
		var xs = qs.Where(q => q[0] <= 2).Select(q => q[1]).ToArray();
		Array.Sort(xs);
		foreach (var x in xs) map.Add(x);

		var set = new IntSegmentMultiSet(map.Count);

		foreach (var q in qs)
		{
			if (q[0] == 1)
			{
				set.Add(map.GetId(q[1]));
			}
			else if (q[0] == 2)
			{
				var x = map.GetId(q[1]);
				var d = Math.Min(q[2], set.GetCount(x));
				set.Add(x, -d);
			}
			else
			{
				sb.Append(map[set.GetAt(set.Count - 1)] - map[set.GetAt(0)]).AppendLine();
			}
		}
		Console.Write(sb);
	}
}

public class IdMap<T>
{
	List<T> l = new List<T>();
	Dictionary<T, int> map = new Dictionary<T, int>();

	public int Count => l.Count;
	public T this[int id] => l[id];
	public int GetId(T item) => map.ContainsKey(item) ? map[item] : -1;
	public bool Contains(T item) => map.ContainsKey(item);
	public int Add(T item)
	{
		if (map.ContainsKey(item)) return map[item];
		l.Add(item);
		return map[item] = l.Count - 1;
	}
}

public class IntSegmentMultiSet
{
	readonly int n = 1;
	readonly long[] c;

	public IntSegmentMultiSet(int itemsCount)
	{
		while (n < itemsCount) n <<= 1;
		c = new long[n << 1];
	}

	public int ItemsCount => n;
	public long Count => c[1];

	public long GetCount(int i) => c[n | i];

	public int GetAt(long index)
	{
		if (index < 0) return -1;
		if (c[1] <= index) return n;
		var i = 1;
		while ((i & n) == 0) if (c[i <<= 1] <= index) index -= c[i++];
		return i & ~n;
	}

	public void Add(int i, long delta = 1) { for (i |= n; i > 0; i >>= 1) c[i] += delta; }
}
