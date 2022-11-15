using System;
using System.Collections.Generic;

namespace EulerLib8
{
	public static class BinarySearch
	{
		/// <summary>
		/// 条件 f を満たす最初の値を探索します。
		/// [l, x) 上で false、[x, r) 上で true となる x を返します。
		/// f(l) が true のとき、l を返します。
		/// f(r - 1) が false のとき、r を返します。
		/// f(r) は評価されません。
		/// </summary>
		public static int First(int l, int r, Func<int, bool> f)
		{
			int m;
			while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
			return r;
		}

		/// <summary>
		/// 条件 f を満たす最後の値を探索します。
		/// (l, x] 上で true、(x, r] 上で false となる x を返します。
		/// f(r) が true のとき、r を返します。
		/// f(l + 1) が false のとき、l を返します。
		/// f(l) は評価されません。
		/// </summary>
		public static int Last(int l, int r, Func<int, bool> f)
		{
			int m;
			while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
			return l;
		}

		public static long First(long l, long r, Func<long, bool> f)
		{
			long m;
			while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
			return r;
		}

		public static long Last(long l, long r, Func<long, bool> f)
		{
			long m;
			while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
			return l;
		}

		/// <summary>
		/// 条件 f を満たす最初の値を指定された誤差の範囲内で探索します。
		/// (l, x) 上で false、[x, r) 上で true となる x を返します。
		/// r 近傍で false のとき、r を返します。
		/// </summary>
		public static double First(double l, double r, Func<double, bool> f, int digits = 9)
		{
			double m;
			while (Math.Round(r - l, digits) > 0) if (f(m = l + (r - l) / 2)) r = m; else l = m;
			return r;
		}

		/// <summary>
		/// 条件 f を満たす最後の値を指定された誤差の範囲内で探索します。
		/// (l, x] 上で true、(x, r) 上で false となる x を返します。
		/// l 近傍で false のとき、l を返します。
		/// </summary>
		public static double Last(double l, double r, Func<double, bool> f, int digits = 9)
		{
			double m;
			while (Math.Round(r - l, digits) > 0) if (f(m = r - (r - l) / 2)) l = m; else r = m;
			return l;
		}
	}
}
