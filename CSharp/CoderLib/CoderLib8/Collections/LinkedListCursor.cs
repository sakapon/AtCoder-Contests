using System;
using System.Collections.Generic;

// https://github.com/sakapon/Samples-2020/blob/master/AlgorithmSample/AlgorithmLab/Collections/LinkedListCursor.cs
namespace CoderLib8.Collections
{
	public static class LinkedListCursor
	{
		public static LinkedListCursor<T> CreateCursor<T>(this LinkedList<T> list) => new LinkedListCursor<T>(list);
	}

	[System.Diagnostics.DebuggerDisplay(@"Index = {Index}, IsAtEnd = {IsAtEnd}")]
	public class LinkedListCursor<T>
	{
		readonly LinkedList<T> l;
		int i;
		LinkedListNode<T> node;

		public LinkedListCursor(LinkedList<T> list)
		{
			l = list;
			JumpToFirst();
		}

		public LinkedList<T> List => l;

		public int Index
		{
			get => i;
			set => Jump(value);
		}
		public bool IsAtEnd => i >= l.Count;

		public T Item
		{
			get => node.Value;
			set => node.Value = value;
		}

		public void JumpToFirst()
		{
			i = 0;
			node = l.First;
		}

		public void JumpToEnd()
		{
			i = l.Count;
			node = null;
		}

		public bool MoveNext()
		{
			if (i >= l.Count) return false;
			++i;
			node = node.Next;
			return true;
		}

		public bool MovePrevious()
		{
			if (i <= 0) return false;
			--i;
			node = node?.Previous ?? l.Last;
			return true;
		}

		void MoveOnly(int delta)
		{
			i += delta;
			if (delta >= 0) while (delta-- > 0) node = node.Next;
			else while (delta++ < 0) node = node?.Previous ?? l.Last;
		}

		public void Repoint(int index = -1)
		{
			if (index == -1) index = i;
			if (index < 0 || l.Count < index) throw new ArgumentOutOfRangeException(nameof(index));
			if (index <= l.Count >> 1) JumpToFirst();
			else JumpToEnd();
			MoveOnly(index - i);
		}

		void Jump(int index)
		{
			if (index < 0 || l.Count < index) throw new ArgumentOutOfRangeException(nameof(index));
			if (index < i >> 1) JumpToFirst();
			else if ((i + l.Count) >> 1 < index) JumpToEnd();
			MoveOnly(index - i);
		}
		public void MoveDelta(int delta) => Jump(i + delta);

		public void Add(T item) => node = IsAtEnd ? l.AddLast(item) : l.AddBefore(node, item);
		public T Pop()
		{
			if (IsAtEnd) throw new InvalidOperationException();
			var tn = node;
			node = node.Next;
			l.Remove(tn);
			return tn.Value;
		}
	}
}
