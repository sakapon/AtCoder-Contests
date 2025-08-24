using System;

// Test: https://atcoder.jp/contests/abc278/tasks/abc278_d
// Clear, Fill が O(1) でできる配列
namespace CoderLib8.Collections
{
	public class ClearableArray<T>
	{
		readonly T[] a;
		// クエリ番号を管理します。
		readonly int[] q;
		T v0;
		int q0 = -1, qi;

		public ClearableArray(T[] a)
		{
			this.a = a;
			q = new int[a.Length];
		}
		public ClearableArray(int n, T iv) : this(new T[n])
		{
			Fill(iv);
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

		public void Clear() => Fill(default(T));
		public void Fill(T v)
		{
			v0 = v;
			q0 = ++qi;
		}
	}
}
