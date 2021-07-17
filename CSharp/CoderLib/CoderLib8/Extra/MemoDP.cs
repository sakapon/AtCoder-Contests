using System;

namespace CoderLib8.Extra
{
	// Test: https://codeforces.com/contest/1511/problem/E
	// Test: https://codeforces.com/contest/1513/problem/C
	class MemoDP1<T>
	{
		static readonly Func<T, T, bool> TEquals = System.Collections.Generic.EqualityComparer<T>.Default.Equals;
		public T[] Raw { get; }
		T iv;
		Func<MemoDP1<T>, int, T> rec;

		public MemoDP1(int n, T iv, Func<MemoDP1<T>, int, T> rec)
		{
			Raw = Array.ConvertAll(new bool[n], _ => iv);
			this.iv = iv;
			this.rec = rec;
		}

		public T this[int i]
		{
			get => TEquals(Raw[i], iv) ? Raw[i] = rec(this, i) : Raw[i];
			set => Raw[i] = value;
		}
	}

	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_s
	class MemoDP2<T>
	{
		static readonly Func<T, T, bool> TEquals = System.Collections.Generic.EqualityComparer<T>.Default.Equals;
		public T[,] Raw { get; }
		T iv;
		Func<MemoDP2<T>, int, int, T> rec;

		public MemoDP2(int n1, int n2, T iv, Func<MemoDP2<T>, int, int, T> rec)
		{
			Raw = new T[n1, n2];
			for (int i = 0; i < n1; ++i)
				for (int j = 0; j < n2; ++j)
					Raw[i, j] = iv;
			this.iv = iv;
			this.rec = rec;
		}

		public T this[int i, int j]
		{
			get => TEquals(Raw[i, j], iv) ? Raw[i, j] = rec(this, i, j) : Raw[i, j];
			set => Raw[i, j] = value;
		}
	}

	// Test: https://atcoder.jp/contests/abc184/tasks/abc184_d
	class MemoDP3<T>
	{
		static readonly Func<T, T, bool> TEquals = System.Collections.Generic.EqualityComparer<T>.Default.Equals;
		public T[,,] Raw { get; }
		T iv;
		Func<MemoDP3<T>, int, int, int, T> rec;

		public MemoDP3(int n1, int n2, int n3, T iv, Func<MemoDP3<T>, int, int, int, T> rec)
		{
			Raw = new T[n1, n2, n3];
			for (int i = 0; i < n1; ++i)
				for (int j = 0; j < n2; ++j)
					for (int k = 0; k < n3; ++k)
						Raw[i, j, k] = iv;
			this.iv = iv;
			this.rec = rec;
		}

		public T this[int i, int j, int k]
		{
			get => TEquals(Raw[i, j, k], iv) ? Raw[i, j, k] = rec(this, i, j, k) : Raw[i, j, k];
			set => Raw[i, j, k] = value;
		}
	}
}
