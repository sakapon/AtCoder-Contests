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
		public void Dft_Fft()
		{
			var n = 1 << 4;
			var a = Enumerable.Range(3, n).ToArray();
			var c = Array.ConvertAll(a, x => new Complex(x, 0));

			var t0 = Dft0.Naive(c);
			var r0 = Dft0.Naive(t0, true).ToInt();
			var t1 = Dft0.Fft(c);
			var r1 = Dft0.Fft(t1, true).ToInt();

			var dft = new Dft(n);
			var t2 = dft.Fft(c);
			var r2 = dft.Fft(t2, true).ToInt();

			CollectionAssert.AreEqual(a, r0);
			CollectionAssert.AreEqual(a, r1);
			CollectionAssert.AreEqual(a, r2);
		}

		[TestMethod]
		public void Ntt_Fft()
		{
			var n = 1 << 4;
			var a = Enumerable.Range(3, n).Select(x => (long)x).ToArray();

			var t0 = Ntt0.Naive(a);
			var r0 = Ntt0.Naive(t0, true);
			var t1 = Ntt0.Fft(a);
			var r1 = Ntt0.Fft(t1, true);

			var ntt = new Ntt(n);
			var t2 = ntt.Fft(a);
			var r2 = ntt.Fft(t2, true);

			CollectionAssert.AreEqual(t0, t1);
			CollectionAssert.AreEqual(t0, t2);
			CollectionAssert.AreEqual(a, r0);
			CollectionAssert.AreEqual(a, r1);
			CollectionAssert.AreEqual(a, r2);
		}

		[TestMethod]
		public void Dft_Fft_Many()
		{
			var n = 1 << 16;
			var a = Enumerable.Range(3, n).ToArray();
			var c = Array.ConvertAll(a, x => new Complex(x, 0));

			var dft = new Dft(n);
			var t = dft.Fft(c);
			var r = dft.Fft(t, true).ToInt();
			CollectionAssert.AreEqual(a, r);
		}

		[TestMethod]
		public void Ntt_Fft_Many()
		{
			var n = 1 << 16;
			var a = Enumerable.Range(3, n).Select(x => (long)x).ToArray();

			var ntt = new Ntt(n);
			var t = ntt.Fft(a);
			var r = ntt.Fft(t, true);
			CollectionAssert.AreEqual(a, r);
		}

		[TestMethod]
		public void Convolution()
		{
			var a = new long[] { 1, 2, 3, 4 };
			var b = new long[] { 5, 6, 7, 8, 9 };
			var expected = new long[] { 5, 16, 34, 60, 70, 70, 59, 36 };

			CollectionAssert.AreEqual(expected, Dft.Convolution(a, b));
			CollectionAssert.AreEqual(expected, Dft0.Convolution(a, b));
			CollectionAssert.AreEqual(expected, Ntt.Convolution(a, b));
			CollectionAssert.AreEqual(expected, Ntt0.Convolution(a, b));
		}

		[TestMethod]
		public void Dft_Convolution_Many()
		{
			var a = Enumerable.Range(3, 1 << 16).Select(x => (long)x).ToArray();
			var b = Enumerable.Range(7, 1 << 16).Select(x => (long)x).ToArray();

			Dft.Convolution(a, b);
		}

		[TestMethod]
		public void Ntt_Convolution_Many()
		{
			var a = Enumerable.Range(3, 1 << 16).Select(x => (long)x).ToArray();
			var b = Enumerable.Range(7, 1 << 16).Select(x => (long)x).ToArray();

			Ntt.Convolution(a, b);
		}
	}
}
