using System;
using System.Collections.Generic;

class B2
{
	static void Main()
	{
		int n = int.Parse(Console.ReadLine());

		var r = new List<int>();
		var dq = new DQ<int>(n);

		for (int k = n; k > 0; k--)
		{
			var q = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
			if (q[0] == 0)
			{
				if (q[1] == 0) dq.PushFirst(q[2]);
				else dq.PushLast(q[2]);
			}
			else if (q[0] == 1)
			{
				r.Add(dq[q[1]]);
			}
			else
			{
				if (q[1] == 0) dq.PopFirst();
				else dq.PopLast();
			}
		}
		Console.WriteLine(string.Join("\n", r));
	}
}

class DQ<T>
{
	T[] a;
	int fiIn, liEx;

	public DQ(int size)
	{
		a = new T[2 * size];
		fiIn = liEx = size;
	}

	public int Length => liEx - fiIn;
	public T First => a[fiIn];
	public T Last => a[liEx - 1];
	public T this[int i] => a[fiIn + i];

	public void PushFirst(T v)
	{
		if (Length == 0) PushLast(v);
		else a[--fiIn] = v;
	}
	public void PushLast(T v) => a[liEx++] = v;
	public T PopFirst() => Length == 1 ? PopLast() : a[fiIn++];
	public T PopLast() => a[--liEx];
}
