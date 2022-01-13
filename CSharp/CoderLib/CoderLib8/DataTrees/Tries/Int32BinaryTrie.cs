using System;

// Test: https://atcoder.jp/contests/abc234/tasks/abc234_d
namespace CoderLib8.DataTrees.Tries
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class TrieNode
	{
		public TrieNode Left { get; internal set; }
		public TrieNode Right { get; internal set; }
		public int Count { get; internal set; }
		public int LeftCount => Left?.Count ?? 0;
		public int RightCount => Right?.Count ?? 0;
	}

	public class Int32BinaryTrie : TrieNode
	{
		const int MaxDigit = 30;

		public void Add(int item)
		{
			++Count;
			TrieNode node = this;
			for (int k = MaxDigit; k >= 0; --k)
			{
				if ((item & (1 << k)) == 0)
					node = node.Left ??= new TrieNode();
				else
					node = node.Right ??= new TrieNode();
				++node.Count;
			}
		}

		public int GetItem(int index)
		{
			var item = 0;
			TrieNode node = this;
			for (int k = MaxDigit; k >= 0; --k)
			{
				var d = index - node.LeftCount;
				if (d < 0)
				{
					node = node.Left;
				}
				else
				{
					node = node.Right;
					index = d;
					item |= 1 << k;
				}
			}
			return item;
		}
	}
}
