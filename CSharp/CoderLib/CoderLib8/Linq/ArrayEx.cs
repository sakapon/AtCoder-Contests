using System;
using System.Collections.Generic;
using Num = System.Int64;

namespace CoderLib8.Linq
{
	public static class ArrayEx
	{
		#region Initialization
		public static T[] Create1ByFunc<T>(int n1, Func<T> newItem)
		{
			var a = new T[n1];
			for (var i = 0; i < n1; ++i) a[i] = newItem();
			return a;
		}
		public static T[] Create1<T>(int n1, T value = default)
		{
			var a = new T[n1];
			Array.Fill(a, value);
			return a;
		}
		public static T[][] Create2<T>(int n1, int n2, T value = default) => Create1ByFunc(n1, () => Create1(n2, value));
		public static T[][][] Create3<T>(int n1, int n2, int n3, T value = default) => Create1ByFunc(n1, () => Create2(n2, n3, value));

		public static T[] Repeat<T>(T value, int count)
		{
			var a = new T[count];
			Array.Fill(a, value);
			return a;
		}

		public static int[] Range(int start, int count)
		{
			var a = new int[count];
			for (var i = 0; i < count; ++i) a[i] = start++;
			return a;
		}
		public static int[] RangeLR(int l, int r) => Range(l, r - l);
		#endregion

		#region Array
		public static void Clear<T>(this T[] a) => Array.Clear(a, 0, a.Length);
		public static void Fill<T>(this T[] a, T value) => Array.Fill(a, value);
		public static void CopyTo<T>(this T[] a, int index, T[] dest, int destIndex, int length) => Array.Copy(a, index, dest, destIndex, length);

		public static bool ArrayEqual<T>(this T[] a, T[] b)
		{
			if (a.Length != b.Length) return false;
			var c = EqualityComparer<T>.Default;
			for (int i = 0; i < a.Length; ++i) if (!c.Equals(a[i], b[i])) return false;
			return true;
		}
		#endregion

		#region Accumulation
		public static TResult Aggregate<TSource, TResult>(this TSource[] a, TResult iv, Func<TResult, TSource, TResult> f)
		{
			foreach (var v in a) iv = f(iv, v);
			return iv;
		}

		public static bool All<TSource>(this TSource[] a, Func<TSource, bool> predicate)
		{
			foreach (var v in a) if (!predicate(v)) return false;
			return true;
		}
		public static bool Any<TSource>(this TSource[] a, Func<TSource, bool> predicate)
		{
			foreach (var v in a) if (predicate(v)) return true;
			return false;
		}

		public static Num Max<TSource>(this TSource[] a, Func<TSource, Num> selector) => a.Aggregate(0L, (r, v) => Math.Max(r, selector(v)));
		public static Num Min<TSource>(this TSource[] a, Func<TSource, Num> selector) => a.Aggregate(0L, (r, v) => Math.Min(r, selector(v)));
		public static Num Sum<TSource>(this TSource[] a, Func<TSource, Num> selector) => a.Aggregate(0L, (r, v) => r + selector(v));
		#endregion

		#region First, Last
		// 存在しない場合は n
		public static int FirstIndex<T>(this T[] a, Func<T, bool> f)
		{
			for (int i = 0; i < a.Length; ++i) if (f(a[i])) return i;
			return a.Length;
		}
		// 存在しない場合は -1
		public static int LastIndex<T>(this T[] a, Func<T, bool> f)
		{
			for (int i = a.Length; --i >= 0;) if (f(a[i])) return i;
			return -1;
		}

		public static T First<T>(this T[] a, Func<T, bool> f, T v0 = default)
		{
			var i = FirstIndex(a, f);
			return i == a.Length ? v0 : a[i];
		}
		public static T Last<T>(this T[] a, Func<T, bool> f, T v0 = default)
		{
			var i = LastIndex(a, f);
			return i == -1 ? v0 : a[i];
		}

		// 存在しない場合は n
		public static int FirstIndexByBS<T>(this T[] a, Func<T, bool> f)
		{
			int m, l = 0, r = a.Length;
			while (l < r) if (f(a[m = l + (r - l) / 2])) r = m; else l = m + 1;
			return r;
		}
		// 存在しない場合は -1
		public static int LastIndexByBS<T>(this T[] a, Func<T, bool> f)
		{
			int m, l = 0, r = a.Length;
			while (l < r) if (!f(a[m = l + (r - l) / 2])) r = m; else l = m + 1;
			return r - 1;
		}

		public static T FirstByBS<T>(this T[] a, Func<T, bool> f, T max = default)
		{
			var i = FirstIndexByBS(a, f);
			return i == a.Length ? max : a[i];
		}
		public static T LastByBS<T>(this T[] a, Func<T, bool> f, T min = default)
		{
			var i = LastIndexByBS(a, f);
			return i == -1 ? min : a[i];
		}
		#endregion

		#region Operations
		public static TSource[] ForEach<TSource>(this TSource[] a, Action<TSource> action)
		{
			Array.ForEach(a, action);
			return a;
		}

		public static T[] Reverse<T>(this T[] a)
		{
			var r = a[..];
			Array.Reverse(r);
			return r;
		}

		public static TResult[] Select<TSource, TResult>(this TSource[] a, Func<TSource, TResult> selector) => Array.ConvertAll(a, v => selector(v));
		public static (TSource value, TResult selected)[] SelectWith<TSource, TResult>(this TSource[] a, Func<TSource, TResult> selector) => Array.ConvertAll(a, v => (v, selector(v)));
		public static TSource[] Where<TSource>(this TSource[] a, Func<TSource, bool> predicate) => Array.FindAll(a, v => predicate(v));

		public static TResult[] Cast<TResult>(this Array a)
		{
			var r = new TResult[a.Length];
			for (int i = 0; i < a.Length; ++i) r[i] = (TResult)Convert.ChangeType(a.GetValue(i), typeof(TResult));
			return r;
		}

		public static TResult[] Cast<TSource, TResult>(this TSource[] a, TResult dummy)
		{
			return Array.ConvertAll(a, v => (TResult)Convert.ChangeType(v, typeof(TResult)));
		}
		#endregion

		#region Sort
		// stable
		public static (TSource value, int index)[] SortWithIndex<TSource>(this TSource[] a)
		{
			var r = new (TSource, int)[a.Length];
			for (int i = 0; i < a.Length; ++i) r[i] = (a[i], i);
			Array.Sort(r);
			return r;
		}

		public static void Sort<TSource, TKey>(this TSource[] a, Func<TSource, TKey> toKey)
		{
			var keys = Array.ConvertAll(a, v => toKey(v));
			Array.Sort(keys, a);
		}
		public static void Sort<TSource, TKey1, TKey2>(this TSource[] a, Func<TSource, TKey1> toKey1, Func<TSource, TKey2> toKey2)
		{
			var keys = Array.ConvertAll(a, v => (toKey1(v), toKey2(v)));
			Array.Sort(keys, a);
		}

		public static void MergeSort<T>(this T[] a, IComparer<T> c = null)
		{
			var n = a.Length;
			var t = new T[n];
			c = c ?? Comparer<T>.Default;

			for (int k = 1; k < n; k <<= 1)
			{
				var ti = 0;
				for (int L = 0; L < n; L += k << 1)
				{
					int R1 = L | k, R2 = R1 + k;
					if (R2 > n) R2 = n;
					int i1 = L, i2 = R1;
					while (ti < R2) t[ti++] = (i2 >= R2 || i1 < R1 && i2 < R2 && c.Compare(a[i1], a[i2]) <= 0) ? a[i1++] : a[i2++];
				}
				Array.Copy(t, a, n);
			}
		}

		public static int[] MergeSortForIndex<T>(this T[] a, IComparer<T> c = null)
		{
			var n = a.Length;
			var b = Range(0, n);
			var t = new int[n];
			c = c ?? Comparer<T>.Default;

			for (int k = 1; k < n; k <<= 1)
			{
				var ti = 0;
				for (int L = 0; L < n; L += k << 1)
				{
					int R1 = L | k, R2 = R1 + k;
					if (R2 > n) R2 = n;
					int i1 = L, i2 = R1;
					while (ti < R2) t[ti++] = (i2 >= R2 || i1 < R1 && i2 < R2 && c.Compare(a[b[i1]], a[b[i2]]) <= 0) ? b[i1++] : b[i2++];
				}
				Array.Copy(t, b, n);
			}
			return b;
		}

		public static (T value, int index)[] MergeSortWithIndex<T>(this T[] a, IComparer<T> c = null) => Array.ConvertAll(a.MergeSortForIndex(c), i => (a[i], i));

		public static TSource[] MergeSort<TSource, TKey>(this TSource[] a, Func<TSource, TKey> toKey)
		{
			var keys = Array.ConvertAll(a, v => toKey(v));
			return Array.ConvertAll(keys.MergeSortForIndex(), i => a[i]);
		}
		public static TSource[] MergeSort<TSource, TKey1, TKey2>(this TSource[] a, Func<TSource, TKey1> toKey1, Func<TSource, TKey2> toKey2)
		{
			var keys = Array.ConvertAll(a, v => (toKey1(v), toKey2(v)));
			return Array.ConvertAll(keys.MergeSortForIndex(), i => a[i]);
		}
		#endregion

		#region Convert
		public static (T, T) ToTuple2<T>(this T[] a) => (a[0], a[1]);
		public static (T, T, T) ToTuple3<T>(this T[] a) => (a[0], a[1], a[2]);

		public static (T1, T2) ToTuple2<T1, T2>(this Array a) => ((T1)Convert.ChangeType(a.GetValue(0), typeof(T1)), (T2)Convert.ChangeType(a.GetValue(1), typeof(T2)));
		public static (T1, T2, T3) ToTuple3<T1, T2, T3>(this Array a) => ((T1)Convert.ChangeType(a.GetValue(0), typeof(T1)), (T2)Convert.ChangeType(a.GetValue(1), typeof(T2)), (T3)Convert.ChangeType(a.GetValue(2), typeof(T3)));

		public static T[] Clone<T>(this T[] a, int newSize = -1)
		{
			if (newSize == -1) newSize = a.Length;
			if (newSize <= a.Length) return a[..newSize];

			var r = new T[newSize];
			Array.Copy(a, r, a.Length);
			return r;
		}

		public static TSource[][] Chunk<TSource>(this TSource[] a, int size)
		{
			var q = Math.DivRem(a.Length, size, out var rem);
			var r = new TSource[rem == 0 ? q : q + 1][];
			for (var i = 0; i < q; ++i)
				r[i] = a[(size * i)..(size * (i + 1))];
			if (rem != 0) r[q] = a[(size * q)..];
			return r;
		}

		public static (TKey key, TSource[] chunk)[] Chunk<TSource, TKey>(this TSource[] a, Func<TSource, TKey> toKey)
		{
			var c = EqualityComparer<TKey>.Default;
			var l = new List<(TKey, TSource[])>();
			if (a.Length == 0) return l.ToArray();
			TKey key = toKey(a[0]), k;
			var si = 0;
			for (var i = 1; i < a.Length; ++i)
			{
				if (c.Equals(k = toKey(a[i]), key)) continue;
				l.Add((key, a[si..i]));
				key = k;
				si = i;
			}
			l.Add((key, a[si..]));
			return l.ToArray();
		}

		public static (TKey key, int size)[] ChunkSizes<TSource, TKey>(this TSource[] a, Func<TSource, TKey> toKey)
		{
			var c = EqualityComparer<TKey>.Default;
			var l = new List<(TKey, int)>();
			if (a.Length == 0) return l.ToArray();
			TKey key = toKey(a[0]), k;
			var si = 0;
			for (var i = 1; i < a.Length; ++i)
			{
				if (c.Equals(k = toKey(a[i]), key)) continue;
				l.Add((key, i - si));
				key = k;
				si = i;
			}
			l.Add((key, a.Length - si));
			return l.ToArray();
		}

		public static TSource[] Concat<TSource>(this TSource[][] a)
		{
			var r = new TSource[a.Sum(b => b.Length)];
			for (int i = 0, s = 0; i < a.Length; ++i)
			{
				Array.Copy(a[i], 0, r, s, a[i].Length);
				s += a[i].Length;
			}
			return r;
		}

		public static (T1, T2)[] Zip<T1, T2>(this T1[] a, T2[] b)
		{
			var r = new (T1, T2)[a.Length <= b.Length ? a.Length : b.Length];
			for (int i = 0; i < r.Length; ++i) r[i] = (a[i], b[i]);
			return r;
		}
		#endregion
	}
}
