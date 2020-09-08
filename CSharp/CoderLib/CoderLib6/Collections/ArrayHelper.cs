using System;

namespace CoderLib6.Collections
{
	static class ArrayHelper
	{
		static void Swap<T>(T[] a, int i, int j) { var o = a[i]; a[i] = a[j]; a[j] = o; }
	}
}
