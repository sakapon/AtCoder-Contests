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
			var cd = Array.ConvertAll(a, x => new ComplexD(x, 0));

			var t0 = Dft0.Naive(c);
			var r0 = Dft0.Naive(t0, true).ToInt();
			var t1 = Dft0.Fft(c);
			var r1 = Dft0.Fft(t1, true).ToInt();
			var t2 = DftD.Fft(cd);
			var r2 = DftD.Fft(t2, true).ToInt();

			CollectionAssert.AreEqual(a, r0);
			CollectionAssert.AreEqual(a, r1);
			CollectionAssert.AreEqual(a, r2);
		}

		[TestMethod]
		public void FftN()
		{
			var a = Enumerable.Range(3, 1 << 4).Select(x => (long)x).ToArray();

			var t0 = DftN.Dft0(a);
			var r0 = DftN.Dft0(t0, true);
			var t1 = DftN.Fft(a);
			var r1 = DftN.Fft(t1, true);

			CollectionAssert.AreEqual(a, r0);
			CollectionAssert.AreEqual(a, r1);
		}

		[TestMethod]
		public void Fft_Many()
		{
			var a = Enumerable.Range(3, 1 << 16).ToArray();
			var c = Array.ConvertAll(a, x => new Complex(x, 0));
			var t1 = Dft0.Fft(c);
			var r1 = Dft0.Fft(t1, true).ToInt();
			CollectionAssert.AreEqual(a, r1);
		}

		[TestMethod]
		public void Fft_ManyD()
		{
			var a = Enumerable.Range(3, 1 << 16).ToArray();
			var cd = Array.ConvertAll(a, x => new ComplexD(x, 0));
			var t2 = DftD.Fft(cd);
			var r2 = DftD.Fft(t2, true).ToInt();
			CollectionAssert.AreEqual(a, r2);
		}

		[TestMethod]
		public void Fft_ManyN()
		{
			var a = Enumerable.Range(3, 1 << 16).Select(x => (long)x).ToArray();
			var t1 = DftN.Fft(a);
			var r1 = DftN.Fft(t1, true);
			CollectionAssert.AreEqual(a, r1);
		}

		[TestMethod]
		public void Convolution()
		{
			var a = new long[] { 1, 2, 3, 4 };
			var b = new long[] { 5, 6, 7, 8, 9 };
			var expected = new long[] { 5, 16, 34, 60, 70, 70, 59, 36 };

			var c = Dft0.Convolution(a, b);
			var cn = DftN.Convolution(a, b);
			CollectionAssert.AreEqual(expected, c);
			CollectionAssert.AreEqual(expected, cn);
		}
	}
}
