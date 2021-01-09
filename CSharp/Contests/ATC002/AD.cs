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

	public P GetPoint(int i, int j) => new P(i, j, this);
	public void AddTransition(Action<P> t) => Transitions.Add(t);

	// end を含みません。
	public void Execute(int si, int ei, int sj, int ej)
	{
		var di = Math.Sign(ei - si);
		var dj = Math.Sign(ej - sj);

		for (int i = si; i != ei; i += di)
			for (int j = sj; j != ej; j += dj)
				foreach (var t in Transitions)
					t(new P(i, j, this));
	}

	public struct P
	{
		public int i, j;
		DP2<T> dp;
		public P(int _i, int _j, DP2<T> _dp) { i = _i; j = _j; dp = _dp; }
		public void Deconstruct(out int _i, out int _j) => (_i, _j) = (i, j);
		public override string ToString() => $"{i} {j}";

		public static P operator +(P v1, (int i, int j) v2) => new P(v1.i + v2.i, v1.j + v2.j, v1.dp);
		public static P operator -(P v1, (int i, int j) v2) => new P(v1.i - v2.i, v1.j - v2.j, v1.dp);
		public P Move(int di = 0, int dj = 0) => this + (di, dj);
		public P Jump(int ni = -1, int nj = -1) => new P(ni == -1 ? i : ni, nj == -1 ? j : nj, dp);

		public bool IsInRange() => IsInRange(dp.n1, dp.n2);
		public bool IsInRange(int n1, int n2) => 0 <= i && i < n1 && 0 <= j && j < n2;

		public T Value
		{
			get => GetValue();
			// 構造体における set property
			//set => Merge(value);
		}
		public T GetValue()
		{
			if (!IsInRange()) return dp.MergeOp.V0;
			return dp[i][j];
		}
		public void Merge(T value)
		{
			if (!IsInRange()) return;
			dp[i][j] = dp.MergeOp.Merge(value, dp[i][j]);
		}
	}
}
