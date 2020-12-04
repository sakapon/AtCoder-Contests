using System;
using System.Collections.Generic;

class AD
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (h, w) = Read2();
		var (ti, tj) = Read2();
		var sv = (ti - 1, tj - 1);
		(ti, tj) = Read2();
		var gv = (ti - 1, tj - 1);
		var c = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var dp = new DP2<int>(h, w, MergeOp.Min(1 << 30));
		dp[sv] = 0;
		dp.AddTransition(p =>
		{
			if (c[p.i][p.j] == '#') return;
			var nv = p.Value + 1;
			p.Move(-1, 0).Merge(nv);
			p.Move(1, 0).Merge(nv);
			p.Move(0, -1).Merge(nv);
			p.Move(0, 1).Merge(nv);
		});

		for (int k = 0; k < h * w; k++)
			dp.Execute(0, h, 0, w);
		Console.WriteLine(dp[gv]);
	}
}

static class MergeOp
{
	public static MergeOp<int> Add => new MergeOp<int>((x, y) => x + y, 0);
	public static MergeOp<long> AddL => new MergeOp<long>((x, y) => x + y, 0);
	public static MergeOp<double> AddD => new MergeOp<double>((x, y) => x + y, 0);
	public static MergeOp<int> Max(int v0 = int.MinValue) => new MergeOp<int>(Math.Max, v0);
	public static MergeOp<long> MaxL(long v0 = long.MinValue) => new MergeOp<long>(Math.Max, v0);
	public static MergeOp<int> Min(int v0 = int.MaxValue) => new MergeOp<int>(Math.Min, v0);
	public static MergeOp<long> MinL(long v0 = long.MaxValue) => new MergeOp<long>(Math.Min, v0);
	public static MergeOp<int> Update(int invalid = int.MinValue) => new MergeOp<int>((x, y) => x == invalid ? y : x, invalid);
	public static MergeOp<long> UpdateL(long invalid = long.MinValue) => new MergeOp<long>((x, y) => x == invalid ? y : x, invalid);
	public static MergeOp<double> UpdateD(double invalid = double.MinValue) => new MergeOp<double>((x, y) => x == invalid ? y : x, invalid);
}

struct MergeOp<T>
{
	public Func<T, T, T> Merge;
	public T V0;
	public MergeOp(Func<T, T, T> merge, T v0) { Merge = merge; V0 = v0; }
}

class DP2<T>
{
	int n1, n2;
	MergeOp<T> MergeOp;
	public T[][] a;
	List<Action<P>> Transitions = new List<Action<P>>();

	public DP2(int n1, int n2, MergeOp<T> mergeOp)
	{
		this.n1 = n1;
		this.n2 = n2;
		MergeOp = mergeOp;
		a = Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => mergeOp.V0));
	}
	public T[] this[int i] => a[i];
	public T this[int i, int j]
	{
		get => a[i][j];
		set => a[i][j] = value;
	}
	public T this[(int i, int j) p]
	{
		get => a[p.i][p.j];
		set => a[p.i][p.j] = value;
	}

	internal T GetValue(P p)
	{
		if (!p.IsInRange(n1, n2)) return MergeOp.V0;
		return a[p.i][p.j];
	}
	internal void MergeValue(P p, T value)
	{
		if (!p.IsInRange(n1, n2)) return;
		a[p.i][p.j] = MergeOp.Merge(value, a[p.i][p.j]);
	}

	public void AddTransition(Action<P> transition) => Transitions.Add(transition);

	public void Execute(int si, int ei, int sj, int ej)
	{
		var di = Math.Sign(ei - si);
		var dj = Math.Sign(ej - sj);

		for (int i = si; i != ei; i += di)
			for (int j = sj; j != ej; j += dj)
				foreach (var transition in Transitions)
					transition(new P(i, j, this));
	}

	public struct P
	{
		public int i, j;
		DP2<T> dp;
		public P(int _i, int _j, DP2<T> _dp) { i = _i; j = _j; dp = _dp; }
		public override string ToString() => $"{i} {j}";

		public static explicit operator (int, int)(P v) => (v.i, v.j);

		public static P operator +(P v1, (int i, int j) v2) => new P(v1.i + v2.i, v1.j + v2.j, v1.dp);
		public static P operator -(P v1, (int i, int j) v2) => new P(v1.i - v2.i, v1.j - v2.j, v1.dp);

		public bool IsInRange(int h, int w) => 0 <= i && i < h && 0 <= j && j < w;

		public T Value
		{
			get => dp.GetValue(this);
			set => dp.MergeValue(this, value);
		}

		public P Move(int di, int dj) => this + (di, dj);

		public T GetValue() => dp.GetValue(this);
		//public T GetValue(int di, int dj) => dp.GetValue(this + (di, dj));
		public void Merge(T value) => dp.MergeValue(this, value);
		//public void Merge(int di, int dj, T value) => dp.MergeValue(this + (di, dj), value);
	}
}
