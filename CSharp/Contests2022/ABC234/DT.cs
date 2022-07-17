using System;

class DT
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var p = Read();

		var r = new int[n - k + 1];
		var set = new Int32BinaryTrie();

		for (int i = 1; i <= n; i++)
		{
			set.Add(p[i - 1]);
			if (i >= k) r[i - k] = set.GetItem(i - k);
		}
		return string.Join("\n", r);
	}
}

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
