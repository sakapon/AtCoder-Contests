using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Collections
{
	class DQ<T>
	{
		T[] a;
		int fiIn, liEx;

		public DQ(int size)
		{
			a = new T[2 * size];
			fiIn = liEx = size;
		}

		public int Length => liEx - fiIn;
		public T First => a[fiIn];
		public T Last => a[liEx - 1];
		public T this[int i] => a[fiIn + i];

		public void PushFirst(T v)
		{
			if (Length == 0) PushLast(v);
			else a[--fiIn] = v;
		}
		public void PushLast(T v) => a[liEx++] = v;
		public T PopFirst() => Length == 1 ? PopLast() : a[fiIn++];
		public T PopLast() => a[--liEx];
	}

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
