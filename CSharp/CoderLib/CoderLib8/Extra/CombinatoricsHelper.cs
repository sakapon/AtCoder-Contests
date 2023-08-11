using System;

namespace CoderLib8.Extra
{
	public static class CombinatoricsHelper
	{
		// 2 つの箱から区別できるボールを 1 つずつ取り出す方法の数
		public static long Choose2Boxes(long[] a)
		{
			var (s0, s1) = (0L, 0L);
			for (int i = 1; i < a.Length; i++)
			{
				s1 += a[i - 1];
				s0 += a[i] * s1;
			}
			return s0;
		}

		// 3 つの箱から区別できるボールを 1 つずつ取り出す方法の数
		public static long Choose3Boxes(long[] a)
		{
			var (s0, s1, s2) = (0L, 0L, 0L);
			for (int i = 2; i < a.Length; i++)
			{
				s2 += a[i - 2];
				s1 += a[i - 1] * s2;
				s0 += a[i] * s1;
			}
			return s0;
		}
	}
}
