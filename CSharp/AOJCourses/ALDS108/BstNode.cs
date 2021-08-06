using System;
using System.Collections.Generic;

[System.Diagnostics.DebuggerDisplay(@"\{{Key}\}")]
public class BstNode<TKey>
{
	public TKey Key { get; set; }
	public BstNode<TKey> Parent { get; set; }

	BstNode<TKey> _left;
	public BstNode<TKey> Left
	{
		get { return _left; }
		set
		{
			_left = value;
			if (value != null) value.Parent = this;
		}
	}

	BstNode<TKey> _right;
	public BstNode<TKey> Right
	{
		get { return _right; }
		set
		{
			_right = value;
			if (value != null) value.Parent = this;
		}
	}
}

public static class BstNode
{
	public static BstNode<TKey> SearchNode<TKey>(this BstNode<TKey> node, TKey key, Comparison<TKey> comparison)
	{
		if (node == null) return null;
		var d = comparison(key, node.Key);
		if (d == 0) return node;
		if (d < 0) return SearchNode(node.Left, key, comparison);
		else return SearchNode(node.Right, key, comparison);
	}

	public static BstNode<TKey> SearchMinNode<TKey>(this BstNode<TKey> node)
	{
		if (node == null) return null;
		return SearchMinNode(node.Left) ?? node;
	}

	public static BstNode<TKey> SearchMaxNode<TKey>(this BstNode<TKey> node)
	{
		if (node == null) return null;
		return SearchMaxNode(node.Right) ?? node;
	}

	public static BstNode<TKey> SearchMinNode<TKey>(this BstNode<TKey> node, Func<TKey, bool> predicate)
	{
		if (node == null) return null;
		if (predicate(node.Key)) return SearchMinNode(node.Left, predicate) ?? node;
		else return SearchMinNode(node.Right, predicate);
	}

	public static BstNode<TKey> SearchMaxNode<TKey>(this BstNode<TKey> node, Func<TKey, bool> predicate)
	{
		if (node == null) return null;
		if (predicate(node.Key)) return SearchMaxNode(node.Right, predicate) ?? node;
		else return SearchMaxNode(node.Left, predicate);
	}

	public static BstNode<TKey> SearchPreviousNode<TKey>(this BstNode<TKey> node)
	{
		if (node == null) return null;
		return SearchMaxNode(node.Left) ?? SearchPreviousAncestor(node);
	}

	public static BstNode<TKey> SearchNextNode<TKey>(this BstNode<TKey> node)
	{
		if (node == null) return null;
		return SearchMinNode(node.Right) ?? SearchNextAncestor(node);
	}

	static BstNode<TKey> SearchPreviousAncestor<TKey>(this BstNode<TKey> node)
	{
		if (node?.Parent == null) return null;
		if (node.Parent.Right == node) return node.Parent;
		else return SearchPreviousAncestor(node.Parent);
	}

	static BstNode<TKey> SearchNextAncestor<TKey>(this BstNode<TKey> node)
	{
		if (node?.Parent == null) return null;
		if (node.Parent.Left == node) return node.Parent;
		else return SearchNextAncestor(node.Parent);
	}

	public static IEnumerable<TKey> GetKeys<TKey>(this BstNode<TKey> node)
	{
		for (var n = SearchMinNode(node); n != null; n = SearchNextNode(n))
		{
			yield return n.Key;
		}
	}

	public static IEnumerable<TKey> GetKeys<TKey>(this BstNode<TKey> node, Func<TKey, bool> predicateForMin, Func<TKey, bool> predicateForMax)
	{
		for (var n = SearchMinNode(node, predicateForMin); n != null && predicateForMax(n.Key); n = SearchNextNode(n))
		{
			yield return n.Key;
		}
	}

	public static void WalkByPreorder<TKey>(this BstNode<TKey> node, Action<TKey> action)
	{
		if (node == null) return;
		action(node.Key);
		WalkByPreorder(node.Left, action);
		WalkByPreorder(node.Right, action);
	}
}
