using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static EulerLib8.Common;

namespace EulerTest.Page002
{
	[TestClass]
	public class P081_090
	{
		#region Test Methods
		[TestMethod] public void T081() => TestHelper.Execute();
		[TestMethod] public void T082() => TestHelper.Execute();
		[TestMethod] public void T083() => TestHelper.Execute();
		[TestMethod] public void T084() => TestHelper.Execute();
		[TestMethod] public void T085() => TestHelper.Execute();
		[TestMethod] public void T086() => TestHelper.Execute();
		[TestMethod] public void T087() => TestHelper.Execute();
		[TestMethod] public void T088() => TestHelper.Execute();
		[TestMethod] public void T089() => TestHelper.Execute();
		[TestMethod] public void T090() => TestHelper.Execute();
		#endregion

		public static object P081()
		{
			return 0;
		}

		public static object P082()
		{
			return 0;
		}

		public static object P083()
		{
			return 0;
		}

		public static object P084()
		{
			return 0;
		}

		public static object P085()
		{
			const int n = 2000000;
			const int w_max = 2000;

			var r = (area: 0, count: 0L);
			for (int h = 1; h <= w_max; h++)
			{
				for (int w = 1; w <= w_max; w++)
				{
					var count = Count(h, w);
					ChMin(ref r, (h * w, count), p => Math.Abs(p.count - n));
					if (count > n) break;
				}
			}
			return r.area;

			static long Count(long h, long w) => h * (h + 1) / 2 * w * (w + 1) / 2;
		}

		public static object P086()
		{
			return 0;
		}

		public static object P087()
		{
			return 0;
		}

		public static object P088()
		{
			return 0;
		}

		public static object P089()
		{
			return 0;
		}

		public static object P090()
		{
			return 0;
		}
	}
}
