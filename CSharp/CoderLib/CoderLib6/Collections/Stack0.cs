namespace CoderLib6.Collections
{
	// 制約: Push は size 回まで。
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/3/ALDS1_3_A
	class Stack0<T>
	{
		T[] a;
		int liEx;

		public Stack0(int size) { a = new T[size]; }

		public int Length => liEx;
		public T First => a[liEx - 1];
		public T this[int i] => a[i];

		public void Push(T v) => a[liEx++] = v;
		public T Pop() => a[--liEx];
	}
}
