namespace CoderLib6.Collections
{
	// 制約: PushFirst, PushLast はそれぞれ size 回まで。
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/1/ITP2_1_B
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
		public T this[int i]
		{
			get { return a[fiIn + i]; }
			set { a[fiIn + i] = value; }
		}

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
