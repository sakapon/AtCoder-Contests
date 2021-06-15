namespace CoderLib6.Collections
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/1/ITP2_1_B
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_ar
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_bi
	class DQ<T>
	{
		T[] a;
		int fiIn, liEx;

		public DQ(int size = 8)
		{
			a = new T[size << 1];
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
			if (fiIn == 0) Expand();
			a[--fiIn] = v;
		}
		public void PushLast(T v)
		{
			if (liEx == a.Length) Expand();
			a[liEx++] = v;
		}

		public T PopFirst() => a[fiIn++];
		public T PopLast() => a[--liEx];

		void Expand()
		{
			var b = new T[a.Length << 1];
			var d = a.Length >> 1;
			a.CopyTo(b, d);
			a = b;
			fiIn += d;
			liEx += d;
		}
	}
}
