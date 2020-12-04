using System;
using System.Collections.Generic;

class AD
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (h, w) = Read2();
		var sv = Read2() - new P(1, 1);
		var gv = Read2() - new P(1, 1);
		var c = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var dp = new DP2<int>(h, w, MergeOp.Min(1 << 30));
		dp.a.SetByP(sv, 0);
		dp.AddTransition(p => c[p.i][p.j] == '#' ? 1 << 30 : dp.GetValue(p, new P(-1, 0)) + 1);
		dp.AddTransition(p => c[p.i][p.j] == '#' ? 1 << 30 : dp.GetValue(p, new P(1, 0)) + 1);
		dp.AddTransition(p => c[p.i][p.j] == '#' ? 1 << 30 : dp.GetValue(p, new P(0, -1)) + 1);
		dp.AddTransition(p => c[p.i][p.j] == '#' ? 1 << 30 : dp.GetValue(p, new P(0, 1)) + 1);

		for (int k = 0; k < h * w; k++)
			dp.Execute(0, h, 0, w);
		Console.WriteLine(dp.a.GetByP(gv));
	}
}

static class MergeOp
{
	public static MergeOp<int> Add => new MergeOp<int>((x, y) => x + y, 0);
	public static MergeOp<long> AddL => new MergeOp<long>((x, y) => x + y, 0);
	public static MergeOp<int> Max(int v0 = int.MinValue) => new MergeOp<int>(Math.Max, v0);
	public static MergeOp<long> MaxL(long v0 = long.MinValue) => new MergeOp<long>(Math.Max, v0);
	public static MergeOp<int> Min(int v0 = int.MaxValue) => new MergeOp<int>(Math.Min, v0);
	public static MergeOp<long> MinL(long v0 = long.MaxValue) => new MergeOp<long>(Math.Min, v0);
	public static MergeOp<int> Update(int invalid = int.MinValue) => new MergeOp<int>((x, y) => x == invalid ? y : x, invalid);
	public static MergeOp<long> UpdateL(long invalid = long.MinValue) => new MergeOp<long>((x, y) => x == invalid ? y : x, invalid);
}

struct MergeOp<T>
{
	public Func<T, T, T> Merge;
	public T V0;
	public MergeOp(Func<T, T, T> merge, T v0) { Merge = merge; V0 = v0; }
}

class DP2<T>
{
	public T[][] a;
	int n1, n2;
	MergeOp<T> MergeOp;
	List<Func<P, T>> Transitions = new List<Func<P, T>>();

	public DP2(int n1, int n2, MergeOp<T> mergeOp)
	{
		a = Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => mergeOp.V0));
		this.n1 = n1;
		this.n2 = n2;
		MergeOp = mergeOp;
	}
	public T[] this[int i] => a[i];

	public T GetValue(P p, P delta)
	{
		p += delta;
		if (!p.IsInRange(n1, n2)) return MergeOp.V0;
		return a[p.i][p.j];
	}
	public void PushValue(P p, P delta, T value)
	{
		p += delta;
		a[p.i][p.j] = MergeOp.Merge(value, a[p.i][p.j]);
	}

	public void AddTransition(Func<P, T> getNewValue) => Transitions.Add(getNewValue);

	public void Execute(int si, int ei, int sj, int ej)
	{
		var di = Math.Sign(ei - si);
		var dj = Math.Sign(ej - sj);

		for (int i = si; i != ei; i += di)
			for (int j = sj; j != ej; j += dj)
				foreach (var getNewValue in Transitions)
					a[i][j] = MergeOp.Merge(getNewValue(new P(i, j)), a[i][j]);
	}
}
