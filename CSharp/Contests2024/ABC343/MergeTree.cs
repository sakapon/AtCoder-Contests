
namespace AlgorithmLib10.SegTrees.SegTrees111
{
	public static class Monoid
	{
		public static Monoid<int> Int32_Add { get; } = new Monoid<int>((x, y) => x + y);
		public static Monoid<long> Int64_Add { get; } = new Monoid<long>((x, y) => x + y);
		public static Monoid<int> Int32_Min { get; } = new Monoid<int>((x, y) => x <= y ? x : y, int.MaxValue);
		public static Monoid<int> Int32_Max { get; } = new Monoid<int>((x, y) => x >= y ? x : y, int.MinValue);

		public static Monoid<int> Int32_ArgMin(int[] a) => new Monoid<int>((x, y) => a[x] <= a[y] ? x : y);

		public static Monoid<int> Int32_Update { get; } = new Monoid<int>((x, y) => x != int.MinValue ? x : y, int.MinValue);
	}

	public struct Monoid<T>
	{
		public Func<T, T, T> Op;
		public T Id;
		public Monoid(Func<T, T, T> op, T id = default(T)) { Op = op; Id = id; }
	}

	public class MergeTree<TValue>
	{
		readonly Func<TValue, TValue, TValue> op;
		readonly TValue iv;
		readonly int n;
		readonly TValue[] values;

		public MergeTree(int size, Monoid<TValue> monoid)
		{
			(op, iv) = (monoid.Op, monoid.Id);
			n = 1;
			while (n < size) n <<= 1;
			values = new TValue[n << 1];
			Clear();
		}
		public void Clear()
		{
			Array.Fill(values, iv);
		}

		public TValue this[int key]
		{
			get => values[n | key];
			set => Set(key, value);
		}
		public TValue this[int l, int r] => Get(l, r);

		public TValue Get(int l, int r)
		{
			if (l < 0) l = 0;
			if (r > n) r = n;
			var (vl, vr) = (iv, iv);
			for (l += n, r += n; l != r; l >>= 1, r >>= 1)
			{
				if ((l & 1) != 0) vl = op(vl, values[l++]);
				if ((r & 1) != 0) vr = op(values[--r], vr);
			}
			return op(vl, vr);
		}

		public void Set(int key, TValue value)
		{
			var i = n | key;
			values[i] = value;
			for (i >>= 1; i != 0; i >>= 1) values[i] = op(values[i << 1], values[(i << 1) | 1]);
		}
	}
}
