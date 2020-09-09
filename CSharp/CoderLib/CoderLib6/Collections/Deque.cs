using System;

namespace CoderLib6.Collections
{
	class DQ<T>
	{
		T[] a;
		int fiIn, liEx;

		public DQ(int size)
		{
			a = new T[2 * size];
			fiIn = liEx = size;
		}

		public int Length => liEx - fiIn;
		public T First => a[fiIn];
		public T Last => a[liEx - 1];
		public T this[int i] => a[fiIn + i];

		public void PushFirst(T v)
		{
			if (Length == 0) PushLast(v);
			else a[--fiIn] = v;
		}
		public void PushLast(T v) => a[liEx++] = v;
		public T PopFirst() => Length == 1 ? PopLast() : a[fiIn++];
		public T PopLast() => a[--liEx];
	}
}
