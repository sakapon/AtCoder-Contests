using System;
using System.Collections.Generic;

namespace CoderLib8.Collections
{
	public static class LinkedListCursor
	{
		public static LinkedListCursor<T> CreateCursor<T>(this LinkedList<T> list) => new LinkedListCursor<T>(list);
	}

	public class LinkedListCursor<T>
	{
		int i;
		LinkedList<T> l;
		LinkedListNode<T> n;

		public int Index
		{
			get => i;
			set => Move(value - i);
		}
		public bool IsAtEnd => i == l.Count;

		public T Item
		{
			get => n.Value;
			set => n.Value = value;
		}

		public LinkedListCursor(LinkedList<T> list)
		{
			l = list ?? throw new ArgumentNullException(nameof(list));
			n = l.First;
		}

		public bool MoveNext()
		{
			if (i == l.Count) return false;
			++i;
			n = n.Next;
			return true;
		}

		public bool MovePrevious()
		{
			if (i == 0) return false;
			--i;
			n = n?.Previous ?? l.Last;
			return true;
		}

		public bool Move(int delta)
		{
			var ti = i + delta;
			if (ti < 0 || l.Count < ti) return false;
			i = ti;
			if (delta >= 0) while (delta-- > 0) n = n.Next;
			else while (delta++ < 0) n = n?.Previous ?? l.Last;
			return true;
		}

		public void Repoint(int index = -1)
		{
			if (index != -1) i = index;

			if (i == l.Count)
			{
				n = null;
			}
			else if (i < l.Count >> 1)
			{
				n = l.First;
				var delta = i;
				while (delta-- > 0) n = n.Next;
			}
			else
			{
				n = l.Last;
				var delta = i - l.Count + 1;
				while (delta++ < 0) n = n.Previous;
			}
		}

		public void Insert(T item)
		{
			n = IsAtEnd ? l.AddLast(item) : l.AddBefore(n, item);
		}

		public bool Remove()
		{
			if (IsAtEnd) return false;
			var tn = n.Next;
			l.Remove(n);
			n = tn;
			return true;
		}
	}
}
