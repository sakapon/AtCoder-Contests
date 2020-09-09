using System;

namespace CoderLib6.Collections
{
	static class ArrayHelper
	{
		static void Swap<T>(T[] a, int i, int j) { var o = a[i]; a[i] = a[j]; a[j] = o; }

		static T[] CreateArray<T>(int l, T v)
		{
			var a = new T[l];
			if (!Equals(v, default(T)))
				for (int i = 0; i < l; ++i)
					a[i] = v;
			return a;
		}

		static T[,] CreateArray<T>(int l1, int l2, T v)
		{
			var a = new T[l1, l2];
			if (!Equals(v, default(T)))
				for (int i = 0; i < l1; ++i)
					for (int j = 0; j < l2; ++j)
						a[i, j] = v;
			return a;
		}

		static T[,,] CreateArray<T>(int l1, int l2, int l3, T v)
		{
			var a = new T[l1, l2, l3];
			if (!Equals(v, default(T)))
				for (int i = 0; i < l1; ++i)
					for (int j = 0; j < l2; ++j)
						for (int k = 0; k < l3; ++k)
							a[i, j, k] = v;
			return a;
		}
	}
}
