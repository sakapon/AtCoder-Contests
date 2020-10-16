using System;

namespace CoderLib8.Collections
{
	static class ArrayHelper
	{
		static T[][][] NewArray3<T>(int n1, int n2, int n3, T v = default(T)) => NewArrayF(n1, () => NewArray2(n2, n3, v));
		static T[][] NewArray2<T>(int n1, int n2, T v = default(T)) => NewArrayF(n1, () => NewArray1(n2, v));
		static T[] NewArray1<T>(int n, T v = default(T))
		{
			var a = new T[n];
			for (int i = 0; i < n; ++i) a[i] = v;
			return a;
		}

		static T[] NewArrayF<T>(int n, Func<T> newItem)
		{
			var a = new T[n];
			for (int i = 0; i < n; ++i) a[i] = newItem();
			return a;
		}
	}
}
