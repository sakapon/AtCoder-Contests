using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class DL2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();
		var qc = int.Parse(Console.ReadLine());

		var sb = new StringBuilder();
		var c = new ClearableArray<long>(a, 0);

		while (qc-- > 0)
		{
			var q = ReadL();
			if (q[0] == 1)
			{
				c.Clear(q[1]);
			}
			else if (q[0] == 2)
			{
				var i = (int)q[1] - 1;
				c[i] += q[2];
			}
			else
			{
				var i = (int)q[1] - 1;
				sb.Append(c[i]).AppendLine();
			}
		}
		Console.Write(sb);
	}
}

public class ClearableArray<T>
{
	// クエリ番号を管理します。
	readonly T[] a;
	readonly int[] q;
	T v0;
	int q0, qi;

	public ClearableArray(T[] a, T v0)
	{
		this.a = a;
		q = new int[a.Length];
		this.v0 = v0;
		q0 = -1;
		qi = 0;
	}
	public ClearableArray(int n, T v0)
	{
		a = new T[n];
		q = new int[a.Length];
		this.v0 = v0;
		q0 = 1;
		qi = 1;
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

	public void Clear(T v0)
	{
		this.v0 = v0;
		q0 = ++qi;
	}
}
