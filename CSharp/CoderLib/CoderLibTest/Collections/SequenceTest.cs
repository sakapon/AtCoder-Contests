using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Collections
{
	class Seq
	{
		int[] a;
		long[] s;
		public Seq(int[] _a) => a = _a;

		public long this[int minIn, int maxEx]
		{
			get
			{
				if (s == null)
				{
					s = new long[a.Length + 1];
					for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
				}
				return s[maxEx] - s[minIn];
			}
		}

		// C# 8.0
		//public long this[Range r] => this[r.Start.GetOffset(a.Length), r.End.GetOffset(a.Length)];
	}

	[TestClass]
	public class SequenceTest
	{
		[TestMethod]
		public void Subsum()
		{
			var seq = new Seq(new[] { 1, 2, 3, 4, 5 });

			Assert.AreEqual(1, seq[0, 1]);
			Assert.AreEqual(3, seq[2, 3]);
			Assert.AreEqual(9, seq[1, 4]);
			Assert.AreEqual(14, seq[1, 5]);
			Assert.AreEqual(15, seq[0, 5]);
		}
	}
}
