﻿using System;
using System.Collections.Generic;

[System.Diagnostics.DebuggerDisplay(@"\{{Key}\}")]
public abstract class BstNodeBase<TKey>
{
	public TKey Key { get; set; }
	public abstract BstNodeBase<TKey> Parent { get; set; }
	public abstract BstNodeBase<TKey> Left { get; set; }
	public abstract BstNodeBase<TKey> Right { get; set; }
}

public static class BstNode
{
	public static BstNodeBase<TKey> SearchNode<TKey>(this BstNodeBase<TKey> node, TKey key, Comparison<TKey> comparison)
	{
		if (node == null) return null;
		var d = comparison(key, node.Key);
		if (d == 0) return node;
		if (d < 0) return SearchNode(node.Left, key, comparison);
		else return SearchNode(node.Right, key, comparison);
	}

	public static BstNodeBase<TKey> SearchMinNode<TKey>(this BstNodeBase<TKey> node)
	{
		if (node == null) return null;
		return SearchMinNode(node.Left) ?? node;
	}

	public static BstNodeBase<TKey> SearchMaxNode<TKey>(this BstNodeBase<TKey> node)
	{
		if (node == null) return null;
		return SearchMaxNode(node.Right) ?? node;
	}

	public static BstNodeBase<TKey> SearchMinNode<TKey>(this BstNodeBase<TKey> node, Func<TKey, bool> predicate)
	{
		if (node == null) return null;
		if (predicate(node.Key)) return SearchMinNode(node.Left, predicate) ?? node;
		else return SearchMinNode(node.Right, predicate);
	}

	public static BstNodeBase<TKey> SearchMaxNode<TKey>(this BstNodeBase<TKey> node, Func<TKey, bool> predicate)
	{
		if (node == null) return null;
		if (predicate(node.Key)) return SearchMaxNode(node.Right, predicate) ?? node;
		else return SearchMaxNode(node.Left, predicate);
	}

	public static BstNodeBase<TKey> SearchPreviousNode<TKey>(this BstNodeBase<TKey> node)
	{
		if (node == null) return null;
		return SearchMaxNode(node.Left) ?? SearchPreviousAncestor(node);
	}

	public static BstNodeBase<TKey> SearchNextNode<TKey>(this BstNodeBase<TKey> node)
	{
		if (node == null) return null;
		return SearchMinNode(node.Right) ?? SearchNextAncestor(node);
	}

	static BstNodeBase<TKey> SearchPreviousAncestor<TKey>(this BstNodeBase<TKey> node)
	{
		if (node?.Parent == null) return null;
		if (node.Parent.Right == node) return node.Parent;
		else return SearchPreviousAncestor(node.Parent);
	}

	static BstNodeBase<TKey> SearchNextAncestor<TKey>(this BstNodeBase<TKey> node)
	{
		if (node?.Parent == null) return null;
		if (node.Parent.Left == node) return node.Parent;
		else return SearchNextAncestor(node.Parent);
	}

	public static IEnumerable<TKey> GetKeys<TKey>(this BstNodeBase<TKey> node)
	{
		for (var n = SearchMinNode(node); n != null; n = SearchNextNode(n))
		{
			yield return n.Key;
		}
	}

	public static IEnumerable<TKey> GetKeys<TKey>(this BstNodeBase<TKey> node, Func<TKey, bool> predicateForMin, Func<TKey, bool> predicateForMax)
	{
		for (var n = SearchMinNode(node, predicateForMin); n != null && predicateForMax(n.Key); n = SearchNextNode(n))
		{
			yield return n.Key;
		}
	}

	public static BstNodeBase<TKey> RotateToRight<TKey>(this BstNodeBase<TKey> node)
	{
		if (node == null) throw new ArgumentNullException();
		var p = node.Left;
		node.Left = p.Right;
		p.Right = node;
		return p;
	}

	public static BstNodeBase<TKey> RotateToLeft<TKey>(this BstNodeBase<TKey> node)
	{
		if (node == null) throw new ArgumentNullException();
		var p = node.Right;
		node.Right = p.Left;
		p.Left = node;
		return p;
	}

	public static void WalkByPreorder<TKey>(this BstNodeBase<TKey> node, Action<TKey> action)
	{
		if (node == null) return;
		action(node.Key);
		WalkByPreorder(node.Left, action);
		WalkByPreorder(node.Right, action);
	}
}

[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
public abstract class BstBase<T, TNode> : IEnumerable<T> where TNode : BstNodeBase<T>
{
	TNode _root;
	public TNode Root
	{
		get { return _root; }
		protected set
		{
			_root = value;
			if (value != null) value.Parent = null;
		}
	}

	protected Comparison<T> compare;
	public int Count { get; protected set; }

	protected BstBase(Comparison<T> comparison = null)
	{
		compare = comparison ?? Comparer<T>.Default.Compare;
	}

	public bool Contains(T item)
	{
		return Root.SearchNode(item, compare) != null;
	}

	public T GetMin()
	{
		if (Root == null) throw new InvalidOperationException("The tree is empty.");
		return Root.SearchMinNode().Key;
	}

	public T GetMax()
	{
		if (Root == null) throw new InvalidOperationException("The tree is empty.");
		return Root.SearchMaxNode().Key;
	}

	public T GetMin(Func<T, bool> predicate)
	{
		if (Root == null) throw new InvalidOperationException("The tree is empty.");
		return Root.SearchMinNode(predicate).Key;
	}

	public T GetMax(Func<T, bool> predicate)
	{
		if (Root == null) throw new InvalidOperationException("The tree is empty.");
		return Root.SearchMaxNode(predicate).Key;
	}

	public T GetPrevious(T item, T defaultValue = default(T))
	{
		var node = Root.SearchNode(item, compare);
		if (node == null) throw new InvalidOperationException("The item is not found.");
		node = node.SearchPreviousNode();
		if (node == null) return defaultValue;
		return node.Key;
	}

	public T GetNext(T item, T defaultValue = default(T))
	{
		var node = Root.SearchNode(item, compare);
		if (node == null) throw new InvalidOperationException("The item is not found.");
		node = node.SearchNextNode();
		if (node == null) return defaultValue;
		return node.Key;
	}

	public IEnumerable<T> GetItems() => Root.GetKeys();
	public IEnumerable<T> GetItems(Func<T, bool> predicateForMin, Func<T, bool> predicateForMax) => Root.GetKeys(predicateForMin, predicateForMax);

	public IEnumerator<T> GetEnumerator() => GetItems().GetEnumerator();
	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetItems().GetEnumerator();

	public abstract bool Add(T item);
	public abstract bool Remove(T item);
}

public static class ComparisonHelper
{
	public static Comparison<T> Create<T>(bool descending = false)
	{
		var c = Comparer<T>.Default;
		if (descending) return (x, y) => c.Compare(y, x);
		else return c.Compare;
	}

	public static Comparison<T> Create<T, TKey>(Func<T, TKey> keySelector, bool descending = false)
	{
		if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
		var c = Comparer<TKey>.Default;
		if (descending) return (x, y) => c.Compare(keySelector(y), keySelector(x));
		else return (x, y) => c.Compare(keySelector(x), keySelector(y));
	}
}
