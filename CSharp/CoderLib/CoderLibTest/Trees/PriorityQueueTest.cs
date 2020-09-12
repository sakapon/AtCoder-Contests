using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.Trees;
using KLibrary.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Trees
{
	[TestClass]
	public class PriorityQueueTest
	{
		[TestMethod]
		public void Sort()
		{
			var values = RandomHelper.CreateData(100000).ToArray();
			var actual = TimeHelper.Measure(() => PQ<int>.Create(values));
			var a = new int[values.Length];
			TimeHelper.Measure(() => { for (var i = 0; i < a.Length; i++) a[i] = actual.Pop(); });
			var e = (int[])values.Clone();
			TimeHelper.Measure(() => Array.Sort(e));
			CollectionAssert.AreEqual(e, a);
		}

		[TestMethod]
		public void SortDescending()
		{
			var values = RandomHelper.CreateData(100000).ToArray();
			var actual = TimeHelper.Measure(() => PQ<int>.Create(values, true));
			var a = new int[values.Length];
			TimeHelper.Measure(() => { for (var i = 0; i < a.Length; i++, actual.Pop()) a[i] = actual.First; });
			var e = TimeHelper.Measure(() => values.OrderByDescending(x => x).ToArray());
			CollectionAssert.AreEqual(e, a);
		}

		[TestMethod]
		public void SortDescending_String()
		{
			var values = RandomHelper.CreateData(100000).ToArray();
			var actual = TimeHelper.Measure(() => PQ<int>.Create(x => x.ToString(), values, true));
			var a = new List<int>();
			TimeHelper.Measure(() => { while (actual.Any()) a.Add(actual.Pop()); });
			var e = TimeHelper.Measure(() => values.OrderByDescending(x => x.ToString()).ToArray());
			CollectionAssert.AreEqual(e, a);
		}

		// 少しだけ取り出す場合、PQ を使うほうが速いです。
		[TestMethod]
		public void SortTake()
		{
			var values = RandomHelper.CreateData(100000).ToArray();
			var actual = TimeHelper.Measure(() =>
			{
				var pq = PQ<int>.Create(values);
				var a = new int[100];
				for (var i = 0; i < a.Length; i++) a[i] = pq.Pop();
				return a;
			});
			var expected = TimeHelper.Measure(() => values.OrderBy(x => x).Take(100).ToArray());
			CollectionAssert.AreEqual(expected, actual);
		}
	}
}
