using System;
using System.Collections.Generic;

class DL2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();
		var qc = int.Parse(Console.ReadLine());

		var r = new List<long>();
		var c = new ClearableArray<long>(a);

		while (qc-- > 0)
		{
			var q = ReadL();

			if (q[0] == 1)
			{
				c.Fill(q[1]);
			}
			else if (q[0] == 2)
			{
				var i = (int)q[1] - 1;
				c[i] += q[2];
			}
			else
			{
				var i = (int)q[1] - 1;
				r.Add(c[i]);
			}
		}
		return string.Join("\n", r);
	}
}

// Clear, Fill が O(1) でできる配列
public class ClearableArray<T>
{
	readonly T[] a;
	// クエリ番号を管理します。
	readonly int[] q;
	T v0;
	int q0 = -1, qi;

	public ClearableArray(T[] a)
	{
		this.a = a;
		q = new int[a.Length];
	}
	public ClearableArray(int n, T iv) : this(new T[n])
	{
		Fill(iv);
	}

	public T this[int i]
	{
		get => q0 < q[i] ? a[i] : v0;
		set
		{
			a[i] = value;
			q[i] = ++qi;
		}
	}

	public void Clear() => Fill(default(T));
	public void Fill(T v)
	{
		v0 = v;
		q0 = ++qi;
	}
}
