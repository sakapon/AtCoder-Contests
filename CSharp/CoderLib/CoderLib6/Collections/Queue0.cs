namespace CoderLib6.Collections
{
	// 制約: Push は size 回まで。
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/3/ALDS1_3_B
	class Queue0<T>
	{
		T[] a;
		int fiIn, liEx;

		public Queue0(int size) { a = new T[size]; }

		public int Length => liEx - fiIn;
		public T First => a[fiIn];
		public T this[int i] => a[fiIn + i];

		public void Push(T v) => a[liEx++] = v;
		public T Pop() => a[fiIn++];
	}
}
