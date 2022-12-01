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
			const int w = 1001;

			var r = 1;
			for (int i = 3; i <= w; i += 2)
			{
				r += (i * i - 3 * (i - 1)) * 2;
				r += i * i * 2;
			}
			return r;
		}

		public static object P029()
		{
			const int n = 100;

			var set = new HashSet<BigInteger>();
			for (int a = 2; a <= n; a++)
				for (int b = 2; b <= n; b++)
					set.Add(BigInteger.Pow(a, b));
			return set.Count;
		}

		public static object P030()
		{
			const int n = 5;

			var p = new int[10];
			for (int i = 0; i < p.Length; ++i)
				p[i] = (int)Math.Pow(i, n);

			var r = 0;
			for (int i = 10; i < 500000; i++)
			{
				var s = i.ToString().Sum(c => p[c - '0']);
				if (s == i) r += i;
			}
			return r;
		}
	}
}
