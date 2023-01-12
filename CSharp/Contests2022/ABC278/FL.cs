using System;
using System.Collections.Generic;
using System.Linq;

class FL
{
	static void Main() => Console.WriteLine(Solve() ? "First" : "Second");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ss = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		var map = NewArray2<bool>(n, n);
		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < n; j++)
			{
				if (i == j) continue;
				if (ss[i][^1] == ss[j][0])
				{
					map[i][j] = true;
				}
			}
		}

		// x: 使った単語の集合、i: 最後に使った単語
		var dp = NewArray2<bool>(1 << n, n);
		for (BitSet32 x = (1U << n) - 2; x.Value > 0; x--)
		{
			for (int i = 0; i < n; i++)
			{
				if (!x.Contains(i)) continue;

				for (int j = 0; j < n; j++)
				{
					if (!map[i][j]) continue;

					var nx = x.SuperSet(j).Value;
					if (nx != x.Value && !dp[nx][j])
					{
						dp[x.Value][i] = true;
						break;
					}
				}
			}
		}

		return Enumerable.Range(0, n).Any(i => !dp[1 << i][i]);
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}

public struct BitSet32
{
	public uint Value;

	public bool Contains(int index) => (Value & (1U << index)) != 0;
	public bool this[int index]
	{
		get => (Value & (1U << index)) != 0;
		set
		{
			if (value) Value |= 1U << index;
			else Value &= ~(1U << index);
		}
	}

	public BitSet32(uint value) => Value = value;
	public override string ToString() => Convert.ToString(Value, 2);
	public static BitSet32 Parse(string s) => Convert.ToUInt32(s, 2);

	public static implicit operator BitSet32(uint v) => new BitSet32(v);
	public static explicit operator uint(BitSet32 v) => v.Value;

	public static BitSet32 operator ++(BitSet32 v) => v.Value + 1;
	public static BitSet32 operator --(BitSet32 v) => v.Value - 1;

	public static BitSet32 operator &(BitSet32 v1, BitSet32 v2) => v1.Value & v2.Value;
	public static BitSet32 operator |(BitSet32 v1, BitSet32 v2) => v1.Value | v2.Value;
	public static BitSet32 operator ^(BitSet32 v1, BitSet32 v2) => v1.Value ^ v2.Value;

	public BitSet32 SuperSet(int index) => Value | (1U << index);
	public BitSet32 SubSet(int index) => Value & ~(1U << index);

	public IEnumerable<BitSet32> NextSuperSets(int n)
	{
		for (var f = 1U; f < (1U << n); f <<= 1)
		{
			var nv = Value | f;
			if (nv != Value) yield return nv;
		}
	}

	public IEnumerable<BitSet32> NextSubSets(int n)
	{
		for (var f = 1U; f < (1U << n); f <<= 1)
		{
			var nv = Value & ~f;
			if (nv != Value) yield return nv;
		}
	}
}
