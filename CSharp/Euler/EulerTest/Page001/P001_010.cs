using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EulerTest.Page001
{
	[TestClass]
	public class P001_010
	{
		#region Test Methods
		[TestMethod]
		public void Test001()
		{
			Console.WriteLine(P001());
		}
		#endregion

		static object P001()
		{
			const int n = 1000;
			return Enumerable.Range(0, n).Where(x => x % 3 == 0 || x % 5 == 0).Sum();
		}
	}
}
