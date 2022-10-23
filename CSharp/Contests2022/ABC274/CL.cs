using System;
using System.Collections.Generic;
using System.Linq;

class CL
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var dp = new OffsetArray<int>(1, 2 * n + 1);
		for (int i = 1; i <= n; i++)
		{
			var j = a[i - 1];
			dp[2 * i] = dp[j] + 1;
			dp[2 * i + 1] = dp[j] + 1;
		}
		return string.Join("\n", dp);
	}
}

[System.Diagnostics.DebuggerDisplay(@"Start = {Offset}, Count = {Count}")]
public class OffsetArray<T> : IEnumerable<T>
{
	readonly T[] a;
	readonly int offset;

	public T[] Raw => a;
	public int Offset => offset;
	public int Count => a.Length;

	public T this[int i]
	{
		get => a[i - offset];
		set => a[i - offset] = value;
	}

	public OffsetArray(T[] a, int offset)
	{
		this.a = a;
		this.offset = offset;
	}
	// [start, start + count)
	public OffsetArray(int start, int count) : this(new T[count], start) { }
	// [l, r)
	public static OffsetArray<T> Create(int l, int r) => new OffsetArray<T>(l, r - l);

	public void Clear() => Array.Clear(a, 0, a.Length);
	public OffsetArray<T> Clone() => new OffsetArray<T>((T[])a.Clone(), offset);

	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
	public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)a).GetEnumerator();
}
