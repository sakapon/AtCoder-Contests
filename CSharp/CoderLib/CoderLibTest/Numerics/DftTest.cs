using System;
using System.Linq;
using System.Numerics;
using CoderLib8.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Numerics
{
	[TestClass]
	public class DftTest
	{
		[TestMethod]
		public void Fft()
		{
			var a = Enumerable.Range(3, 1 << 4).ToArray();
			var c = Array.ConvertAll(a, x => new Complex(x, 0));

			var t0 = Dft.Dft0(c);
			var r0 = Dft.Dft0(t0, true).ToInt();
			var t1 = Dft.Fft(c);
			var r1 = Dft.Fft(t1, true).ToInt();

			CollectionAssert.AreEqual(a, r0);
			CollectionAssert.AreEqual(a, r1);
		}

		[TestMethod]
		public void Convolution()
		{
			var a = new long[] { 1, 2, 3, 4 };
			var b = new long[] { 5, 6, 7, 8, 9 };
			var expected = new long[] { 5, 16, 34, 60, 70, 70, 59, 36 };

			var c = Dft.Convolution(a, b);
			CollectionAssert.AreEqual(expected, c);
		}
	}
}
