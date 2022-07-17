using System;
using System.Collections.Generic;
using System.Numerics;

class G
{
	static AsciiIO io = new AsciiIO();
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = io.Int();
		var a = io.Array(n, () => new BitSet(n));

		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < n; j++)
			{
				a[i][j] = io.Char() == '1';
			}
		}

		var r = 0L;
		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < i; j++) a[i][j] = false;

			for (int j = i + 1; j < n; j++)
			{
				if (!a[i][j]) continue;

				var and = a[i].And(a[j]);
				r += and.PopCount();

				a[i][j] = false;
			}
		}
		return r;
	}
}

[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
public class BitSet : IEnumerable<bool>
{
	int n;
	ulong[] a;

	public BitSet(int count)
	{
		n = count;
		a = new ulong[(n >> 6) + 1];
	}

	public int Count => n;
	public bool this[int i]
	{
		get => (a[i >> 6] & (1UL << i)) != 0;
		set
		{
			if (value)
				a[i >> 6] |= 1UL << i;
			else
				a[i >> 6] &= ~(1UL << i);
		}
	}

	public BitSet And(BitSet other)
	{
		var set = new BitSet(n);
		for (int i = 0; i < a.Length; ++i) set.a[i] = a[i] & other.a[i];
		return set;
	}

	public int PopCount()
	{
		var r = 0;
		for (int i = 0; i < a.Length; ++i) r += BitOperations.PopCount(a[i]);
		return r;
	}

	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
	public IEnumerator<bool> GetEnumerator() { for (var i = 0; i < n; ++i) yield return this[i]; }
}
