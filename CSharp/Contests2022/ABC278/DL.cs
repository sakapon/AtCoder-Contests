using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class DL
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();
		var qc = int.Parse(Console.ReadLine());

		var sb = new StringBuilder();
		var c = new ClearableArray0<long>(a, 0);

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

public class ClearableArray0<T>
{
	readonly T[] a;
	T v0;
	readonly HashSet<int> u = new HashSet<int>();

	public ClearableArray0(T[] a, T v0)
	{
		this.a = a;
		this.v0 = v0;
		for (int i = 0; i < a.Length; ++i) u.Add(i);
	}
	public ClearableArray0(int n, T v0)
	{
		a = new T[n];
		this.v0 = v0;
	}

	public T this[int i]
	{
		get => u.Contains(i) ? a[i] : v0;
		set
		{
			u.Add(i);
			a[i] = value;
		}
	}

	public void Clear(T v0)
	{
		u.Clear();
		this.v0 = v0;
	}
}
