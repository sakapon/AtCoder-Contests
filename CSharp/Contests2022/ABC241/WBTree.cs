﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TreesLab.WBTrees
{
	[System.Diagnostics.DebuggerDisplay(@"\{{Item}\}")]
	public class Node<T>
	{
		#region Properties

		public T Item { get; internal set; }
		public Node<T> Parent { get; internal set; }
		public Node<T> Left { get; private set; }
		public Node<T> Right { get; private set; }

		internal void SetLeft(Node<T> node)
		{
			Left = node;
			if (node != null) node.Parent = this;
		}

		internal void SetRight(Node<T> node)
		{
			Right = node;
			if (node != null) node.Parent = this;
		}

		public int Count { get; private set; } = 1;
		public int LeftCount => Left?.Count ?? 0;
		public int RightCount => Right?.Count ?? 0;

		internal void UpdateCount(bool recursive = false)
		{
			Count = LeftCount + RightCount + 1;
			if (recursive) Parent?.UpdateCount(true);
		}

		#endregion

		#region Get Node

		public Node<T> GetPrevious() => Left?.GetLast() ?? GetPreviousAncestor();
		public Node<T> GetNext() => Right?.GetFirst() ?? GetNextAncestor();

		Node<T> GetPreviousAncestor() => Parent?.Right == this ? Parent : Parent?.GetPreviousAncestor();
		Node<T> GetNextAncestor() => Parent?.Left == this ? Parent : Parent?.GetNextAncestor();

		public Node<T> GetFirst() => Left?.GetFirst() ?? this;
		public Node<T> GetLast() => Right?.GetLast() ?? this;

		public Node<T> GetFirst(Func<T, bool> predicate)
		{
			if (predicate?.Invoke(Item) ?? true) return Left?.GetFirst(predicate) ?? this;
			else return Right?.GetFirst(predicate);
		}

		public Node<T> GetLast(Func<T, bool> predicate)
		{
			if (predicate?.Invoke(Item) ?? true) return Right?.GetLast(predicate) ?? this;
			else return Left?.GetLast(predicate);
		}

		public Node<T> GetFirst(T item, IComparer<T> comparer)
		{
			if (comparer == null) comparer = Comparer<T>.Default;
			var d = comparer.Compare(item, Item);
			if (d == 0) return Left?.GetFirst(item, comparer) ?? this;
			else if (d < 0) return Left?.GetFirst(item, comparer);
			else return Right?.GetFirst(item, comparer);
		}

		public Node<T> GetLast(T item, IComparer<T> comparer)
		{
			if (comparer == null) comparer = Comparer<T>.Default;
			var d = comparer.Compare(item, Item);
			if (d == 0) return Right?.GetLast(item, comparer) ?? this;
			else if (d > 0) return Right?.GetLast(item, comparer);
			else return Left?.GetLast(item, comparer);
		}

		#endregion

		#region Get Node (by Index)

		// out of range: null
		public Node<T> GetAt(int index)
		{
			var d = index - LeftCount;
			if (d == 0) return this;
			else if (d < 0) return Left?.GetAt(index);
			else return Right?.GetAt(d - 1);
		}

		#endregion

		#region Get Index

		public int GetIndex()
		{
			if (Parent == null) return LeftCount;
			else if (Parent.Left == this) return Parent.GetIndex() - RightCount - 1;
			else return Parent.GetIndex() + LeftCount + 1;
		}

		// not found: Count
		public int GetFirstIndex(Func<T, bool> predicate)
		{
			if (predicate?.Invoke(Item) ?? true) return Left?.GetFirstIndex(predicate) ?? 0;
			else return (Right?.GetFirstIndex(predicate) ?? 0) + LeftCount + 1;
		}

		// not found: -1
		public int GetLastIndex(Func<T, bool> predicate)
		{
			if (predicate?.Invoke(Item) ?? true) return (Right?.GetLastIndex(predicate) ?? -1) + LeftCount + 1;
			else return Left?.GetLastIndex(predicate) ?? -1;
		}

		#endregion

		public void Walk(Action<Node<T>> preorder, Action<Node<T>> inorder, Action<Node<T>> postorder)
		{
			preorder?.Invoke(this);
			Left?.Walk(preorder, inorder, postorder);
			inorder?.Invoke(this);
			Right?.Walk(preorder, inorder, postorder);
			postorder?.Invoke(this);
		}
	}

	public static class NodeHelper
	{
		public static bool Exists<T>(this Node<T> node) => node != null;

		public static T GetItemOrDefault<T>(this Node<T> node, T defaultItem = default(T)) => node != null ? node.Item : defaultItem;
		public static bool TryGetItem<T>(this Node<T> node, out T item)
		{
			item = node != null ? node.Item : default(T);
			return node != null;
		}

		public static TValue GetValueOrDefault<TKey, TValue>(this Node<KeyValuePair<TKey, TValue>> node, TValue defaultValue = default(TValue)) => node != null ? node.Item.Value : defaultValue;
		public static bool TryGetValue<TKey, TValue>(this Node<KeyValuePair<TKey, TValue>> node, out TValue value)
		{
			value = node != null ? node.Item.Value : default(TValue);
			return node != null;
		}
	}

	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class WBList<T> : IEnumerable<T>
	{
		#region Properties

		public Node<T> Root { get; private set; }
		public int Count => Root?.Count ?? 0;

		// Root を変更するには、このメソッドを経由します。
		protected void SetRoot(Node<T> node)
		{
			Root = node;
			if (node != null) node.Parent = null;
		}

		#endregion

		#region Initialize Tree

		public void Clear() => SetRoot(null);

		public void Initialize(IEnumerable<T> items)
		{
			if (items == null) throw new ArgumentNullException(nameof(items));
			var a = items as T[] ?? items.ToArray();
			SetRoot(CreateSubtree(a, 0, a.Length));
		}

		static Node<T> CreateSubtree(T[] items, int l, int r)
		{
			if (r == l) return null;
			if (r == l + 1) return new Node<T> { Item = items[l] };

			var m = (l + r) / 2;
			var node = new Node<T> { Item = items[m] };
			node.SetLeft(CreateSubtree(items, l, m));
			node.SetRight(CreateSubtree(items, m + 1, r));
			node.UpdateCount();
			return node;
		}

		#endregion

		#region Get/Remove Items

		public IEnumerator<T> GetEnumerator() => GetItems().GetEnumerator();
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetItems().GetEnumerator();
		public IEnumerable<T> GetItems()
		{
			for (var n = Root?.GetFirst(); n != null; n = n.GetNext())
				yield return n.Item;
		}

		public IEnumerable<T> GetItemsDescending()
		{
			for (var n = Root?.GetLast(); n != null; n = n.GetPrevious())
				yield return n.Item;
		}

		#endregion

		#region Get/Remove Items (by Index)

		public IEnumerable<T> GetItems(int startIndex, int endIndex)
		{
			for (var n = Root?.GetAt(startIndex); n != null && startIndex < endIndex; n = n.GetNext(), ++startIndex)
				yield return n.Item;
		}

		public int RemoveItems(int startIndex, int endIndex)
		{
			var nodes = new List<Node<T>>();
			for (var n = Root?.GetAt(startIndex); n != null && startIndex < endIndex; n = n.GetNext(), ++startIndex)
				nodes.Add(n);

			for (int i = nodes.Count - 1; i >= 0; --i) RemoveNode(nodes[i]);
			return nodes.Count;
		}

		#endregion

		#region Get/Remove Node

		public Node<T> GetFirst() => Root?.GetFirst();
		public Node<T> GetLast() => Root?.GetLast();
		public Node<T> RemoveFirst() => RemoveNode(GetFirst());
		public Node<T> RemoveLast() => RemoveNode(GetLast());

		#endregion

		#region Get/Remove Node (by Index)

		public Node<T> GetAt(int index) => Root?.GetAt(index);
		public Node<T> RemoveAt(int index) => RemoveNode(GetAt(index));

		public T this[int index]
		{
			get
			{
				var node = GetAt(index) ?? throw new ArgumentOutOfRangeException(nameof(index));
				return node.Item;
			}
			set
			{
				var node = GetAt(index) ?? throw new ArgumentOutOfRangeException(nameof(index));
				node.Item = value;
			}
		}

		#endregion

		#region Insert Item(s)

		public Node<T> Insert(int index, T item) => InsertNode(index, item);
		public Node<T> Prepend(T item) => InsertNode(0, item);
		public Node<T> Add(T item) => InsertNode(Count, item);

		public int InsertItems(int index, IEnumerable<T> items)
		{
			if (items == null) throw new ArgumentNullException(nameof(items));
			var c = Count;
			foreach (var x in items) InsertNode(index++, x);
			return Count - c;
		}
		public int PrependItems(IEnumerable<T> items) => InsertItems(0, items);
		public int AddItems(IEnumerable<T> items) => InsertItems(Count, items);

		#endregion

		#region Protected Methods

		protected Node<T> InsertNode(int index, T item)
		{
			if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
			if (index > Count) throw new ArgumentOutOfRangeException(nameof(index));

			if (Root == null)
			{
				SetRoot(new Node<T> { Item = item });
				return Root;
			}

			Node<T> node = Root, newNode;
			while (true)
			{
				var d = index - node.LeftCount - 1;
				if (d < 0)
				{
					if (node.Left == null)
					{
						node.SetLeft(newNode = new Node<T> { Item = item });
						break;
					}
					node = node.Left;
				}
				else
				{
					if (node.Right == null)
					{
						node.SetRight(newNode = new Node<T> { Item = item });
						break;
					}
					node = node.Right;
					index = d;
				}
			}

			for (; node != null; node = node.Parent)
				node = Balance(node);
			return newNode;
		}

		// この実装では、引数で指定されたインスタンスが削除されます。
		protected Node<T> RemoveNode(Node<T> node)
		{
			if (node == null) return null;

			Node<T> dirty;
			if (node.Left == null || node.Right == null)
			{
				UpdateChild(node, node.Left ?? node.Right);
				dirty = node.Parent;
			}
			else
			{
				var node2 = dirty = node.Right.GetFirst();
				if (node2 != node.Right)
				{
					dirty = node2.Parent;
					UpdateChild(node2, node2.Right);
					node2.SetRight(node.Right);
				}
				node2.SetLeft(node.Left);
				UpdateChild(node, node2);
			}
			dirty?.UpdateCount(true);
			return node;
		}

		#endregion

		#region Private Methods

		// Suppose node != null.
		void UpdateChild(Node<T> node, Node<T> newNode)
		{
			var parent = node.Parent;
			if (parent == null) SetRoot(newNode);
			else if (parent.Left == node) parent.SetLeft(newNode);
			else parent.SetRight(newNode);
		}

		// Suppose t != null.
		Node<T> Balance(Node<T> t)
		{
			var lc = t.LeftCount + 1;
			var rc = t.RightCount + 1;
			if (lc > 2 * rc)
			{
				t = RotateToRight(t);
			}
			else if (rc > 2 * lc)
			{
				t = RotateToLeft(t);
			}
			t.UpdateCount();
			return t;
		}

		// Suppose t != null.
		Node<T> RotateToRight(Node<T> t)
		{
			var p = t.Left;
			UpdateChild(t, p);
			t.SetLeft(p.Right);
			p.SetRight(t);
			t.UpdateCount();
			return p;
		}

		// Suppose t != null.
		Node<T> RotateToLeft(Node<T> t)
		{
			var p = t.Right;
			UpdateChild(t, p);
			t.SetRight(p.Left);
			p.SetLeft(t);
			t.UpdateCount();
			return p;
		}

		#endregion
	}

	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public abstract class WBTreeBase<T> : IEnumerable<T>
	{
		#region Properties

		public Node<T> Root { get; private set; }
		public int Count => Root?.Count ?? 0;
		public IComparer<T> Comparer { get; }
		protected abstract bool IsDistinct { get; }

		// Root を変更するには、このメソッドを経由します。
		protected void SetRoot(Node<T> node)
		{
			Root = node;
			if (node != null) node.Parent = null;
		}

		#endregion

		protected WBTreeBase(IComparer<T> comparer = null)
		{
			Comparer = comparer ?? Comparer<T>.Default;
		}

		#region Initialize Tree

		public void Clear() => SetRoot(null);

		public void Initialize(IEnumerable<T> items, bool assertsItems = true)
		{
			if (items == null) throw new ArgumentNullException(nameof(items));
			T[] a;
			if (assertsItems)
			{
				// stable sort
				a = items.OrderBy(x => x, Comparer).ToArray();
				if (IsDistinct)
					for (int i = 1; i < a.Length; ++i)
						if (Comparer.Compare(a[i - 1], a[i]) == 0) throw new ArgumentException("The keys must be unique.", nameof(items));
			}
			else
			{
				a = items as T[] ?? items.ToArray();
			}
			SetRoot(CreateSubtree(a, 0, a.Length));
		}

		static Node<T> CreateSubtree(T[] items, int l, int r)
		{
			if (r == l) return null;
			if (r == l + 1) return new Node<T> { Item = items[l] };

			var m = (l + r) / 2;
			var node = new Node<T> { Item = items[m] };
			node.SetLeft(CreateSubtree(items, l, m));
			node.SetRight(CreateSubtree(items, m + 1, r));
			node.UpdateCount();
			return node;
		}

		#endregion

		#region Get/Remove Items

		public IEnumerator<T> GetEnumerator() => GetItems().GetEnumerator();
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetItems().GetEnumerator();
		public IEnumerable<T> GetItems()
		{
			for (var n = Root?.GetFirst(); n != null; n = n.GetNext())
				yield return n.Item;
		}

		public IEnumerable<T> GetItemsDescending()
		{
			for (var n = Root?.GetLast(); n != null; n = n.GetPrevious())
				yield return n.Item;
		}

		public IEnumerable<T> GetItems(Func<T, bool> startPredicate, Func<T, bool> endPredicate)
		{
			for (var n = Root?.GetFirst(startPredicate); n != null && (endPredicate?.Invoke(n.Item) ?? true); n = n.GetNext())
				yield return n.Item;
		}

		public IEnumerable<T> GetItemsDescending(Func<T, bool> startPredicate, Func<T, bool> endPredicate)
		{
			for (var n = Root?.GetLast(endPredicate); n != null && (startPredicate?.Invoke(n.Item) ?? true); n = n.GetPrevious())
				yield return n.Item;
		}

		public int RemoveItems(Func<T, bool> startPredicate, Func<T, bool> endPredicate)
		{
			var nodes = new List<Node<T>>();
			for (var n = Root?.GetFirst(startPredicate); n != null && (endPredicate?.Invoke(n.Item) ?? true); n = n.GetNext())
				nodes.Add(n);

			for (int i = nodes.Count - 1; i >= 0; --i) RemoveNode(nodes[i]);
			return nodes.Count;
		}

		#endregion

		#region Get/Remove Items (by Index)

		public IEnumerable<T> GetItems(int startIndex, int endIndex)
		{
			for (var n = Root?.GetAt(startIndex); n != null && startIndex < endIndex; n = n.GetNext(), ++startIndex)
				yield return n.Item;
		}

		public int RemoveItems(int startIndex, int endIndex)
		{
			var nodes = new List<Node<T>>();
			for (var n = Root?.GetAt(startIndex); n != null && startIndex < endIndex; n = n.GetNext(), ++startIndex)
				nodes.Add(n);

			for (int i = nodes.Count - 1; i >= 0; --i) RemoveNode(nodes[i]);
			return nodes.Count;
		}

		#endregion

		#region Get/Remove Node

		public Node<T> GetFirst() => Root?.GetFirst();
		public Node<T> GetLast() => Root?.GetLast();
		public Node<T> RemoveFirst() => RemoveNode(GetFirst());
		public Node<T> RemoveLast() => RemoveNode(GetLast());

		public Node<T> GetFirst(Func<T, bool> predicate) => Root?.GetFirst(predicate);
		public Node<T> GetLast(Func<T, bool> predicate) => Root?.GetLast(predicate);
		public Node<T> RemoveFirst(Func<T, bool> predicate) => RemoveNode(GetFirst(predicate));
		public Node<T> RemoveLast(Func<T, bool> predicate) => RemoveNode(GetLast(predicate));

		#endregion

		#region Get/Remove Node (by Index)

		public Node<T> GetAt(int index) => Root?.GetAt(index);
		public Node<T> RemoveAt(int index) => RemoveNode(GetAt(index));

		#endregion

		#region Get Index

		public int GetFirstIndex(Func<T, bool> predicate) => Root?.GetFirstIndex(predicate) ?? 0;
		public int GetLastIndex(Func<T, bool> predicate) => Root?.GetLastIndex(predicate) ?? -1;

		public int GetCount(Func<T, bool> startPredicate, Func<T, bool> endPredicate)
		{
			var c = GetLastIndex(endPredicate) - GetFirstIndex(startPredicate) + 1;
			return c >= 0 ? c : 0;
		}

		#endregion

		#region Add Item(s)

		public Node<T> Add(T item)
		{
			var c = Count;
			var node = AddOrGetNode(item);
			return Count != c ? node : null;
		}

		public int AddItems(IEnumerable<T> items)
		{
			if (items == null) throw new ArgumentNullException(nameof(items));
			var c = Count;
			foreach (var x in items) AddOrGetNode(x);
			return Count - c;
		}

		#endregion

		#region Protected Methods

		// IsDistinct かつ既に要素が存在する場合、ノードを追加しません。
		protected Node<T> AddOrGetNode(T item)
		{
			if (Root == null)
			{
				SetRoot(new Node<T> { Item = item });
				return Root;
			}

			Node<T> node = Root, newNode;
			while (true)
			{
				var d = Comparer.Compare(item, node.Item);
				if (IsDistinct && d == 0) return node;

				if (d < 0)
				{
					if (node.Left == null)
					{
						node.SetLeft(newNode = new Node<T> { Item = item });
						break;
					}
					node = node.Left;
				}
				else
				{
					if (node.Right == null)
					{
						node.SetRight(newNode = new Node<T> { Item = item });
						break;
					}
					node = node.Right;
				}
			}

			for (; node != null; node = node.Parent)
				node = Balance(node);
			return newNode;
		}

		// この実装では、引数で指定されたインスタンスが削除されます。
		protected Node<T> RemoveNode(Node<T> node)
		{
			if (node == null) return null;

			Node<T> dirty;
			if (node.Left == null || node.Right == null)
			{
				UpdateChild(node, node.Left ?? node.Right);
				dirty = node.Parent;
			}
			else
			{
				var node2 = dirty = node.Right.GetFirst();
				if (node2 != node.Right)
				{
					dirty = node2.Parent;
					UpdateChild(node2, node2.Right);
					node2.SetRight(node.Right);
				}
				node2.SetLeft(node.Left);
				UpdateChild(node, node2);
			}
			dirty?.UpdateCount(true);
			return node;
		}

		#endregion

		#region Private Methods

		// Suppose node != null.
		void UpdateChild(Node<T> node, Node<T> newNode)
		{
			var parent = node.Parent;
			if (parent == null) SetRoot(newNode);
			else if (parent.Left == node) parent.SetLeft(newNode);
			else parent.SetRight(newNode);
		}

		// Suppose t != null.
		Node<T> Balance(Node<T> t)
		{
			var lc = t.LeftCount + 1;
			var rc = t.RightCount + 1;
			if (lc > 2 * rc)
			{
				t = RotateToRight(t);
			}
			else if (rc > 2 * lc)
			{
				t = RotateToLeft(t);
			}
			t.UpdateCount();
			return t;
		}

		// Suppose t != null.
		Node<T> RotateToRight(Node<T> t)
		{
			var p = t.Left;
			UpdateChild(t, p);
			t.SetLeft(p.Right);
			p.SetRight(t);
			t.UpdateCount();
			return p;
		}

		// Suppose t != null.
		Node<T> RotateToLeft(Node<T> t)
		{
			var p = t.Right;
			UpdateChild(t, p);
			t.SetRight(p.Left);
			p.SetLeft(t);
			t.UpdateCount();
			return p;
		}

		#endregion
	}

	public abstract class WBSetBase<T> : WBTreeBase<T>
	{
		protected WBSetBase(IComparer<T> comparer = null) : base(comparer) { }

		public Node<T> GetFirst(T item) => Root?.GetFirst(item, Comparer);
		public Node<T> GetLast(T item) => Root?.GetLast(item, Comparer);
		public bool Remove(T item) => RemoveNode(GetLast(item)) != null;

		public int GetFirstIndex(T item) => GetFirst(item)?.GetIndex() ?? -1;
		public int GetLastIndex(T item) => GetLast(item)?.GetIndex() ?? -1;

		public bool Contains(T item) => GetFirst(item) != null;
		public int GetCount(T item) => GetCount(x => Comparer.Compare(x, item) >= 0, x => Comparer.Compare(x, item) <= 0);
	}

	public class WBSet<T> : WBSetBase<T>
	{
		protected override bool IsDistinct => true;
		public WBSet(IComparer<T> comparer = null) : base(comparer) { }
		public Node<T> Get(T item) => GetFirst(item);
		public int GetIndex(T item) => GetFirstIndex(item);
	}

	public class WBMultiSet<T> : WBSetBase<T>
	{
		protected override bool IsDistinct => false;
		public WBMultiSet(IComparer<T> comparer = null) : base(comparer) { }
		public int RemoveAll(T item) => RemoveItems(x => Comparer.Compare(x, item) >= 0, x => Comparer.Compare(x, item) <= 0);
	}

	public abstract class WBMapBase<TKey, TValue> : WBTreeBase<KeyValuePair<TKey, TValue>>
	{
		public IComparer<TKey> KeyComparer { get; }

		protected WBMapBase(IComparer<TKey> keyComparer = null) : base(CreateComparer(keyComparer))
		{
			KeyComparer = keyComparer ?? Comparer<TKey>.Default;
		}

		static IComparer<KeyValuePair<TKey, TValue>> CreateComparer(IComparer<TKey> keyComparer)
		{
			if (keyComparer == null) keyComparer = Comparer<TKey>.Default;
			return Comparer<KeyValuePair<TKey, TValue>>.Create((x, y) => keyComparer.Compare(x.Key, y.Key));
		}

		public Node<KeyValuePair<TKey, TValue>> GetFirst(TKey key) => Root?.GetFirst(new KeyValuePair<TKey, TValue>(key, default(TValue)), Comparer);
		public Node<KeyValuePair<TKey, TValue>> GetLast(TKey key) => Root?.GetLast(new KeyValuePair<TKey, TValue>(key, default(TValue)), Comparer);
		public Node<KeyValuePair<TKey, TValue>> RemoveFirst(TKey key) => RemoveNode(GetFirst(key));
		public Node<KeyValuePair<TKey, TValue>> RemoveLast(TKey key) => RemoveNode(GetLast(key));

		public int GetFirstIndex(TKey key) => GetFirst(key)?.GetIndex() ?? -1;
		public int GetLastIndex(TKey key) => GetLast(key)?.GetIndex() ?? -1;

		public bool ContainsKey(TKey key) => GetFirst(key) != null;
		public int GetCount(TKey key) => GetCount(p => KeyComparer.Compare(p.Key, key) >= 0, p => KeyComparer.Compare(p.Key, key) <= 0);

		public Node<KeyValuePair<TKey, TValue>> Add(TKey key, TValue value) => Add(new KeyValuePair<TKey, TValue>(key, value));
		public void Initialize(IEnumerable<(TKey key, TValue value)> items) => Initialize(items?.Select(p => new KeyValuePair<TKey, TValue>(p.key, p.value)));
		public int AddItems(IEnumerable<(TKey key, TValue value)> items) => AddItems(items?.Select(p => new KeyValuePair<TKey, TValue>(p.key, p.value)));
	}

	public class WBMap<TKey, TValue> : WBMapBase<TKey, TValue>
	{
		protected override bool IsDistinct => true;
		public WBMap(IComparer<TKey> keyComparer = null) : base(keyComparer) { }

		public Node<KeyValuePair<TKey, TValue>> Get(TKey key) => GetFirst(key);
		public Node<KeyValuePair<TKey, TValue>> Remove(TKey key) => RemoveLast(key);
		public int GetIndex(TKey key) => GetFirstIndex(key);

		public TValue this[TKey key]
		{
			get
			{
				var node = GetFirst(key) ?? throw new KeyNotFoundException("The specified key is not found.");
				return node.Item.Value;
			}
			set
			{
				var item = new KeyValuePair<TKey, TValue>(key, value);
				AddOrGetNode(item).Item = item;
			}
		}

		public void SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			if (items == null) throw new ArgumentNullException(nameof(items));
			foreach (var p in items) AddOrGetNode(p).Item = p;
		}
	}

	public class WBMultiMap<TKey, TValue> : WBMapBase<TKey, TValue>
	{
		protected override bool IsDistinct => false;
		public WBMultiMap(IComparer<TKey> keyComparer = null) : base(keyComparer) { }

		public int RemoveAll(TKey key) => RemoveItems(p => KeyComparer.Compare(p.Key, key) >= 0, p => KeyComparer.Compare(p.Key, key) <= 0);

		public IEnumerable<TValue> this[TKey key]
		{
			get
			{
				for (var n = Root?.GetFirst(p => KeyComparer.Compare(p.Key, key) >= 0); n != null && KeyComparer.Compare(n.Item.Key, key) == 0; n = n.GetNext())
					yield return n.Item.Value;
			}
		}
	}
}
