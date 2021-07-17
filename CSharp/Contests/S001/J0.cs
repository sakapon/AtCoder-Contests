using System;
using System.Collections.Generic;
using System.Linq;

class J0
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();

		var r = 0L;
		var st = new SegmentTree(n + 1);
		for (int i = 0; i < n; i++)
		{
			r += i - st.Sum(0, a[i] + 1);
			st.Add(a[i], 1);
		}
		Console.WriteLine(r);
	}
}

class SegmentTree
{
	static int[] p2 = Enumerable.Range(0, 30).Select(i => (int)Math.Pow(2, i)).ToArray();
	static Dictionary<int, int> p2_ = Enumerable.Range(0, 30).ToDictionary(i => p2[i], i => i);

	static int LogFloor(int v)
	{
		for (int i = 1; ; i++) if (p2[i] > v) return i - 1;
	}

	int n2, logn;
	int[] a;
	public SegmentTree(int n)
	{
		logn = (int)Math.Ceiling(Math.Log(n, 2));
		n2 = p2[logn];
		a = new int[2 * n2];
	}

	public void Add(int i, int v)
	{
		for (i += n2; i > 0; i /= 2) a[i] += v;
	}

	// 簡易実装
	public int Sum(int start, int count)
	{
		if (p2_.ContainsKey(count))
		{
			return a[(start + n2) / count];
		}
		else
		{
			var c2 = p2[LogFloor(count)];
			return Sum(start, c2) + Sum(start + c2, count - c2);
		}
	}
}
