using System;
using System.Collections.Generic;

class DD
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var (a, b, c) = Read3();

		var dp = new DP3<double>(101, 101, 101, MergeOp.AddD);
		dp.AddTransition(p =>
		{
			var (i, j, k) = p;
			var all = i + j + k;
			p.Merge((p.Move(1).Value + 1) * i / all);
			p.Merge((p.Move(0, 1).Value + 1) * j / all);
			p.Merge((p.Move(0, 0, 1).Value + 1) * k / all);
		});
		dp.Execute(99, -1, 99, -1, 99, -1);

		Console.WriteLine(dp[a][b][c]);
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

class DP3<T>
{
	int n1, n2, n3;
	MergeOp<T> MergeOp;
	public T[][][] a;
	List<Action<P>> Transitions = new List<Action<P>>();

	public DP3(int n1, int n2, int n3, MergeOp<T> mergeOp)
	{
		this.n1 = n1;
		this.n2 = n2;
		this.n3 = n3;
		MergeOp = mergeOp;
		a = Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => Array.ConvertAll(new bool[n3], ___ => mergeOp.V0)));
	}

	public T[][] this[int i] => a[i];
	public T this[int i, int j, int k]
	{
		get => a[i][j][k];
		set => a[i][j][k] = value;
	}
	public T this[(int i, int j, int k) p]
	{
		get => a[p.i][p.j][p.k];
		set => a[p.i][p.j][p.k] = value;
	}

	public P GetPoint(int i, int j, int k) => new P(i, j, k, this);
	public void AddTransition(Action<P> transition) => Transitions.Add(transition);

	// end を含みません。
	public void Execute(int si, int ei, int sj, int ej, int sk, int ek)
	{
		var di = Math.Sign(ei - si);
		var dj = Math.Sign(ej - sj);
		var dk = Math.Sign(ek - sk);

		for (int i = si; i != ei; i += di)
			for (int j = sj; j != ej; j += dj)
				for (int k = sk; k != ek; k += dk)
					foreach (var t in Transitions)
						t(new P(i, j, k, this));
	}

	public struct P
	{
		public int i, j, k;
		DP3<T> dp;
		public P(int _i, int _j, int _k, DP3<T> _dp) { i = _i; j = _j; k = _k; dp = _dp; }
		public void Deconstruct(out int _i, out int _j, out int _k) => (_i, _j, _k) = (i, j, k);
		public override string ToString() => $"{i} {j} {k}";

		public static P operator +(P v1, (int i, int j, int k) v2) => new P(v1.i + v2.i, v1.j + v2.j, v1.k + v2.k, v1.dp);
		public static P operator -(P v1, (int i, int j, int k) v2) => new P(v1.i - v2.i, v1.j - v2.j, v1.k - v2.k, v1.dp);
		public P Move(int di = 0, int dj = 0, int dk = 0) => this + (di, dj, dk);
		public P Jump(int ni = -1, int nj = -1, int nk = -1) => new P(ni == -1 ? i : ni, nj == -1 ? j : nj, nk == -1 ? k : nk, dp);

		public bool IsInRange() => IsInRange(dp.n1, dp.n2, dp.n3);
		public bool IsInRange(int n1, int n2, int n3) => 0 <= i && i < n1 && 0 <= j && j < n2 && 0 <= k && k < n3;

		public T Value
		{
			get => GetValue();
			// 構造体における set property
			//set => Merge(value);
		}
		public T GetValue()
		{
			if (!IsInRange()) return dp.MergeOp.V0;
			return dp[i][j][k];
		}
		public void Merge(T value)
		{
			if (!IsInRange()) return;
			dp[i][j][k] = dp.MergeOp.Merge(value, dp[i][j][k]);
		}
	}
}
