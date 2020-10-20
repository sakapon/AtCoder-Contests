using System;

namespace CoderLib8.Collections
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/7/DPL/1
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/7/DPL/2
	static class ArrayHelper
	{
		//const long max = 1L << 60;
		//const long min = -1L << 60;
		const int max = 1 << 30;
		const int min = -1 << 30;

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

		static (T, T) ToTuple2<T>(T[] a) => (a[0], a[1]);
		static (T, T, T) ToTuple3<T>(T[] a) => (a[0], a[1], a[2]);
	}
}
