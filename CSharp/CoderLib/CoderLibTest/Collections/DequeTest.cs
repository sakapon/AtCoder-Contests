using System;
using CoderLib6.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Collections
{
	[TestClass]
	public class DequeTest
	{
		[TestMethod]
		public void PushPop()
		{
			var dq = new DQ<int>(10);

			dq.PushFirst(2);
			dq.PushFirst(3);
			dq.PushFirst(4);
			Assert.AreEqual(2, dq.PopLast());
			Assert.AreEqual(3, dq.PopLast());
			Assert.AreEqual(4, dq.PopLast());
			Assert.AreEqual(0, dq.Length);
		}
	}
}
