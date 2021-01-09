using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.Trees;
using KLibrary.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Trees
{
	[TestClass]
	public class SegmentTreeTest
	{
		[TestMethod]
		public void Subsum_Random()
		{
			for (int k = 0; k < 10; k++)
			{
				for (int n = 0; n < 10; n++) Test(n);
				for (int n = 250; n < 260; n++) Test(n);
			}

			void Test(int n)
			{
				var a = RandomHelper.CreateData(n);
				var s = new int[n + 1];
				var st = new ST_Subsum(n);

				for (int i = 0; i < n; i++)
				{
					s[i + 1] = s[i] + a[i];
					st.Add(i, a[i]);
				}

				for (int i = 0; i < n; i++)
					for (int j = i; j <= n; j++)
						Assert.AreEqual(s[j] - s[i], st.Subsum(i, j));
			}
		}

		[TestMethod]
		public void Inversion_Random()
		{
			for (int k = 0; k < 10; k++)
			{
				for (int n = 0; n < 10; n++) Test(n);
				for (int n = 250; n < 260; n++) Test(n);
			}

			void Test(int n)
			{
				var a = RandomHelper.ShuffleRange(1, n);

				var expected = 0;
				for (int i = 0; i < n; i++)
					for (int j = i + 1; j < n; j++)
						if (a[i] > a[j]) expected++;

				var st = new ST_Subsum(n + 1);
				var r = 0L;
				for (int i = 0; i < n; i++)
				{
					r += st.Subsum(a[i], n + 1);
					st.Add(a[i], 1);
				}
				Assert.AreEqual(expected, r);
			}
		}
	}
}
