using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, qc) = Read2();

		var sb = new StringBuilder();

		var ballsCount = n + qc + 10;
		var rn = Enumerable.Range(0, n + 1).ToArray();
		var rb = Enumerable.Range(0, ballsCount).ToArray();

		// <ball, box>
		var uf = new UF<int>(ballsCount, (x, y) => x, rb);

		// box -> 代表元の ball
		var boxes = rn;

		var k = n;
		while (qc-- > 0)
		{
			var q = Read();
			var x = q[1];

			if (q[0] == 1)
			{
				var y = q[2];

				var bx = boxes[x];
				var by = boxes[y];
				if (by == -1) continue;

				if (bx == -1)
				{
					rb[uf.GetRoot(by)] = x;
					boxes[x] = by;
					boxes[y] = -1;
				}
				else
				{
					uf.Unite(bx, by);
					rb[uf.GetRoot(by)] = x;
					boxes[y] = -1;
				}
			}
			else if (q[0] == 2)
			{
				var y = ++k;

				var bx = boxes[x];
				var by = y;
				if (by == -1) continue;

				if (bx == -1)
				{
					rb[uf.GetRoot(by)] = x;
					boxes[x] = by;
					//boxes[y] = -1;
				}
				else
				{
					uf.Unite(bx, by);
					rb[uf.GetRoot(by)] = x;
					//boxes[y] = -1;
				}
			}
			else
			{
				var root = uf.GetRoot(x);
				sb.AppendLine(uf.GetValue(root).ToString());
			}
		}

		Console.Write(sb);
	}
}

public class UF
{
	int[] p, sizes;
	public int GroupsCount { get; private set; }

	public UF(int n)
	{
		p = Array.ConvertAll(new bool[n], _ => -1);
		sizes = Array.ConvertAll(p, _ => 1);
		GroupsCount = n;
	}

	public int GetRoot(int x) => p[x] == -1 ? x : p[x] = GetRoot(p[x]);
	public bool AreUnited(int x, int y) => GetRoot(x) == GetRoot(y);
	public int GetSize(int x) => sizes[GetRoot(x)];

	public bool Unite(int x, int y)
	{
		if ((x = GetRoot(x)) == (y = GetRoot(y))) return false;

		if (sizes[x] < sizes[y]) Merge(y, x);
		else Merge(x, y);
		return true;
	}
	protected virtual void Merge(int x, int y)
	{
		p[y] = x;
		sizes[x] += sizes[y];
		--GroupsCount;
	}
	public ILookup<int, int> ToGroups() => Enumerable.Range(0, p.Length).ToLookup(GetRoot);
}

public class UF<T> : UF
{
	T[] a;
	// (parent, child) => result
	Func<T, T, T> MergeData;
	public UF(int n, Func<T, T, T> merge, T[] a0) : base(n)
	{
		a = a0;
		MergeData = merge;
	}

	public T GetValue(int x) => a[GetRoot(x)];
	protected override void Merge(int x, int y)
	{
		base.Merge(x, y);
		a[x] = MergeData(a[x], a[y]);
	}
}
