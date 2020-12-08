using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class D0
{
	static void Main()
	{
		var n = 9;
		var dp = new DP3<int>(n, n, 4, MergeOp.Min());
		dp[n / 2][n / 2][0] = 0;

		dp.AddTransition(p =>
		{
			if (p.Value == int.MaxValue) return;
			var nv = p.Value + 1;

			p.Move(0, 0, -3).Merge(nv);
			p.Move(0, 0, -1).Merge(nv);
			p.Move(0, 0, 1).Merge(nv);
			p.Move(0, 0, 3).Merge(nv);

			switch (p.k)
			{
				case 0:
					p.Move(1, 0, 1).Merge(nv);
					p.Move(1, 0, 2).Merge(nv);
					p.Move(0, 1, 3).Merge(nv);
					p.Move(0, 1, 2).Merge(nv);
					p.Move(1, 1, 2).Merge(nv);
					break;
				case 1:
					p.Move(-1, 0, -1).Merge(nv);
					p.Move(-1, 0, 2).Merge(nv);
					p.Move(0, 1, 1).Merge(nv);
					p.Move(0, 1, 2).Merge(nv);
					p.Move(-1, 1, 2).Merge(nv);
					break;
				case 2:
					p.Move(-1, 0, 1).Merge(nv);
					p.Move(-1, 0, -2).Merge(nv);
					p.Move(0, -1, -1).Merge(nv);
					p.Move(0, -1, -2).Merge(nv);
					p.Move(-1, -1, -2).Merge(nv);
					break;
				case 3:
					p.Move(1, 0, -1).Merge(nv);
					p.Move(1, 0, -2).Merge(nv);
					p.Move(0, -1, -3).Merge(nv);
					p.Move(0, -1, -2).Merge(nv);
					p.Move(1, -1, -2).Merge(nv);
					break;
				default:
					break;
			}
		});

		for (int k = 0; k < n * n; k++)
			dp.Execute(0, n, 0, n, 0, 4);

		using (var writer = File.CreateText("D0.txt"))
		{
			var rn = Enumerable.Range(0, n).ToArray();
			for (int k = 0; k < 4; k++)
			{
				for (int j = n - 1; j >= 0; j--)
					writer.WriteLine(string.Join("", rn.Select(i => dp[i, j, k] % 10)));
				writer.WriteLine();
			}
		}
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

	internal T GetValue(P p)
	{
		if (!p.IsInRange()) return MergeOp.V0;
		return a[p.i][p.j][p.k];
	}
	internal void MergeValue(P p, T value)
	{
		if (!p.IsInRange()) return;
		a[p.i][p.j][p.k] = MergeOp.Merge(value, a[p.i][p.j][p.k]);
	}

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
		public override string ToString() => $"{i} {j} {k}";

		public static P operator +(P v1, (int i, int j, int k) v2) => new P(v1.i + v2.i, v1.j + v2.j, v1.k + v2.k, v1.dp);
		public static P operator -(P v1, (int i, int j, int k) v2) => new P(v1.i - v2.i, v1.j - v2.j, v1.k - v2.k, v1.dp);

		public bool IsInRange() => IsInRange(dp.n1, dp.n2, dp.n3);
		public bool IsInRange(int h, int w, int d) => 0 <= i && i < h && 0 <= j && j < w && 0 <= k && k < d;

		public T Value
		{
			get => dp.GetValue(this);
			set => dp.MergeValue(this, value);
		}

		public P Move(int di, int dj, int dk) => this + (di, dj, dk);

		public T GetValue() => dp.GetValue(this);
		public void Merge(T value) => dp.MergeValue(this, value);
	}
}
