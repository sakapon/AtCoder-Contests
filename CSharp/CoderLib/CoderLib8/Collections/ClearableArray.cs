using System;

// Test: https://atcoder.jp/contests/abc278/tasks/abc278_d
namespace CoderLib8.Collections
{
	public class ClearableArray<T>
	{
		// クエリ番号を管理します。
		readonly T[] a;
		readonly int[] q;
		T v0;
		int q0, qi;

		public ClearableArray(T[] a, T v0)
		{
			this.a = a;
			q = new int[a.Length];
			this.v0 = v0;
			q0 = -1;
			qi = 0;
		}
		public ClearableArray(int n, T v0)
		{
			a = new T[n];
			q = new int[a.Length];
			this.v0 = v0;
			q0 = 1;
			qi = 1;
		}

		public T this[int i]
		{
			get => q0 < q[i] ? a[i] : v0;
			set
			{
				a[i] = value;
				q[i] = ++qi;
			}
		}

		public void Clear(T v0)
		{
			this.v0 = v0;
			q0 = ++qi;
		}
	}
}
