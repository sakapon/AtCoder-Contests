namespace CoderLib6.Collections
{
	class Seq
	{
		int[] a;
		long[] s;
		public Seq(int[] _a) { a = _a; }

		public long Sum(int minIn, int maxEx)
		{
			if (s == null)
			{
				s = new long[a.Length + 1];
				for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
			}
			return s[maxEx] - s[minIn];
		}

		// C# 8.0
		//public long Sum(Range r) => Sum(r.Start.GetOffset(a.Length), r.End.GetOffset(a.Length));
	}
}
