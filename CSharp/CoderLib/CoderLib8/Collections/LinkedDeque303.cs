using System;
using System.Collections.Generic;

// https://github.com/sakapon/Samples-2020/tree/master/AlgorithmSample/AlgorithmLab/Collections/Linked
namespace CoderLib8.Collections
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class LinkedDeque<T> : IEnumerable<T>
	{
		[System.Diagnostics.DebuggerDisplay(@"\{{Item}\}")]
		internal class Node
		{
			public T Item;
			public Node Next, Previous;

			public static void Link(Node previous, Node next)
			{
				previous.Next = next;
				next.Previous = previous;
			}

			public Node AddPrevious(T item)
			{
				var node = new Node { Item = item };
				Link(Previous, node);
				Link(node, this);
				return node;
			}

			public Node Remove()
			{
				Link(Previous, Next);
				return Next;
			}
		}

		internal readonly Node root = new Node();
		int n;

		public LinkedDeque(IEnumerable<T> items = null)
		{
			Clear();
			if (items != null) foreach (var item in items) AddLast(item);
		}

		public int Count => n;
		public T First
		{
			get => root.Next.Item;
			set => root.Next.Item = value;
		}
		public T Last
		{
			get => root.Previous.Item;
			set => root.Previous.Item = value;
		}

		public void Clear()
		{
			Node.Link(root, root);
			n = 0;
		}

		public void AddFirst(T item)
		{
			root.Next.AddPrevious(item);
			++n;
		}

		public void AddLast(T item)
		{
			root.AddPrevious(item);
			++n;
		}

		public T PopFirst()
		{
			var item = root.Next.Item;
			root.Next.Remove();
			--n;
			return item;
		}

		public T PopLast()
		{
			var item = root.Previous.Item;
			root.Previous.Remove();
			--n;
			return item;
		}

		internal Node AddBefore(Node node, T item)
		{
			++n;
			return node.AddPrevious(item);
		}

		internal Node Remove(Node node)
		{
			--n;
			return node.Remove();
		}

		public void ConcatFirst(LinkedDeque<T> other)
		{
			Node.Link(other.root.Previous, root.Next);
			Node.Link(root, other.root.Next);
			n += other.n;
			other.Clear();
		}

		public void ConcatLast(LinkedDeque<T> other)
		{
			Node.Link(root.Previous, other.root.Next);
			Node.Link(other.root.Previous, root);
			n += other.n;
			other.Clear();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<T> GetEnumerator() { for (var t = root.Next; t != root; t = t.Next) yield return t.Item; }

		public T[] ToArray()
		{
			var r = new T[n];
			for (var (t, i) = (root.Next, 0); t != root; t = t.Next, ++i) r[i] = t.Item;
			return r;
		}

		public LinkedDequeCursor<T> CreateCursor() => new LinkedDequeCursor<T>(this);
	}

	[System.Diagnostics.DebuggerDisplay(@"Index = {Index}, IsAtEnd = {IsAtEnd}")]
	public class LinkedDequeCursor<T>
	{
		readonly LinkedDeque<T> q;
		int i;
		LinkedDeque<T>.Node node;

		internal LinkedDequeCursor(LinkedDeque<T> deque)
		{
			q = deque;
			JumpToFirst();
		}

		public LinkedDeque<T> Deque => q;

		public int Index
		{
			get => i;
			set => Jump(value);
		}
		public bool IsAtEnd => i >= q.Count;

		public T Item
		{
			get => node.Item;
			set => node.Item = value;
		}

		public void JumpToFirst()
		{
			i = 0;
			node = q.root.Next;
		}

		public void JumpToEnd()
		{
			i = q.Count;
			node = q.root;
		}

		public bool MoveNext()
		{
			if (i >= q.Count) return false;
			++i;
			node = node.Next;
			return true;
		}

		public bool MovePrevious()
		{
			if (i <= 0) return false;
			--i;
			node = node.Previous;
			return true;
		}

		void MoveOnly(int delta)
		{
			i += delta;
			if (delta >= 0) while (delta-- > 0) node = node.Next;
			else while (delta++ < 0) node = node.Previous;
		}

		public void Repoint(int index = -1)
		{
			if (index == -1) index = i;
			if (index < 0 || q.Count < index) throw new ArgumentOutOfRangeException(nameof(index));
			if (index <= q.Count >> 1) JumpToFirst();
			else JumpToEnd();
			MoveOnly(index - i);
		}

		void Jump(int index)
		{
			if (index < 0 || q.Count < index) throw new ArgumentOutOfRangeException(nameof(index));
			if (index < i >> 1) JumpToFirst();
			else if ((i + q.Count) >> 1 < index) JumpToEnd();
			MoveOnly(index - i);
		}
		public void MoveDelta(int delta) => Jump(i + delta);

		public void Add(T item) => node = q.AddBefore(node, item);
		public T Pop()
		{
			if (IsAtEnd) throw new InvalidOperationException();
			var item = node.Item;
			node = q.Remove(node);
			return item;
		}
	}
}
