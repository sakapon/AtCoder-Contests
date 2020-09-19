using System;

namespace CoderLib8.Collections
{
	static class ArrayHelper
	{
		static T[] NewArray<T>(int n, Func<T> newItem)
		{
			var a = new T[n];
			for (int i = 0; i < n; ++i) a[i] = newItem();
			return a;
		}

		static T[][] NewArray0<T>(int n1, int n2) => NewArray(n1, () => new T[n2]);
		static T[][][] NewArray0<T>(int n1, int n2, int n3) => NewArray(n1, () => NewArray0<T>(n2, n3));

		static T[] NewArray<T>(int n, T v)
		{
			var a = new T[n];
			if (!Equals(v, default(T)))
				for (int i = 0; i < n; ++i) a[i] = v;
			return a;
		}

		// 以下は未検証 (Clone を使うかどうか)
		static T[][] CreateArray<T>(int l, T[] v)
		{
			var a = new T[l][];
			for (int i = 0; i < l; ++i) a[i] = (T[])v.Clone();
			return a;
		}

		static T[][] CreateArray<T>(int l1, int l2, T v) => CreateArray(l1, NewArray(l2, v));
		static T[][][] CreateArray<T>(int l1, int l2, int l3, T v) => CreateArray(l1, CreateArray(l2, l3, v));
	}
}
