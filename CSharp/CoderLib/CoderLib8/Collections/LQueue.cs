using System;
using System.Collections.Generic;

namespace CoderLib8.Collections
{
	// singly linked list
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class LQueue<T> : IEnumerable<T>
	{
		[System.Diagnostics.DebuggerDisplay(@"\{{Item}\}")]
		class Node
		{
			public T Item;
			public Node Next;
		}

		Node First;
		Node Last;
		public int Count { get; private set; }

		public T Top => First != null ? First.Item : throw new InvalidOperationException("There are no items.");

		public T Pop()
		{
			var item = Top;
			if ((First = First.Next) == null) Last = null;
			--Count;
			return item;
		}

		public void Add(T item)
		{
			var node = new Node { Item = item };
			Last = (Last == null) ? (First = node) : (Last.Next = node);
			++Count;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<T> GetEnumerator() { for (var n = First; n != null; n = n.Next) yield return n.Item; }
	}
}
