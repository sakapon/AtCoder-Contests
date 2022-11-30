using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using EulerLib8.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EulerTest.Page001
{
	[TestClass]
	public class P021_030
	{
		#region Test Methods
		[TestMethod] public void T021() => TestHelper.Execute();
		[TestMethod] public void T022() => TestHelper.Execute();
		[TestMethod] public void T023() => TestHelper.Execute();
		[TestMethod] public void T024() => TestHelper.Execute();
		[TestMethod] public void T025() => TestHelper.Execute();
		[TestMethod] public void T026() => TestHelper.Execute();
		[TestMethod] public void T027() => TestHelper.Execute();
		[TestMethod] public void T028() => TestHelper.Execute();
		[TestMethod] public void T029() => TestHelper.Execute();
		[TestMethod] public void T030() => TestHelper.Execute();
		#endregion

		public static object P021()
		{
			return 0;
		}

		public static object P022()
		{
			return 0;
		}

		public static object P023()
		{
			return 0;
		}

		public static object P024()
		{
			var r10 = Enumerable.Range(0, 10).Select(i => (char)(i + '0'));
			return new string(r10.PermutationAt(999999));
		}

		public static object P025()
		{
			var d = BigInteger.Pow(10, 999);
			var p = Fibonacci.FibonacciSeq(0, 1).Select((v, i) => (v, i)).First(p => p.v >= d);
			return p.i;
		}

		public static object P026()
		{
			return 0;
		}

		public static object P027()
		{
			return 0;
		}

		public static object P028()
		{
			return 0;
		}

		public static object P029()
		{
			return 0;
		}

		public static object P030()
		{
			return 0;
		}
	}
}
