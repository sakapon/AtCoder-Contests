using System;

namespace CoderLib8.Collections
{
	static class ArrayHelper
	{
		static T[] CreateArray<T>(int l, T v)
		{
			var a = new T[l];
			if (!Equals(v, default(T)))
				for (int i = 0; i < l; ++i) a[i] = v;
			return a;
		}

		static T[][] CreateArray<T>(int l, T[] v)
		{
			var a = new T[l][];
			for (int i = 0; i < l; ++i) a[i] = (T[])v.Clone();
			return a;
		}

		static T[][] CreateArray<T>(int l1, int l2, T v) => CreateArray(l1, CreateArray(l2, v));
		static T[][][] CreateArray<T>(int l1, int l2, int l3, T v) => CreateArray(l1, CreateArray(l2, l3, v));
	}
}
