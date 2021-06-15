using System;
using System.Collections.Generic;

class B2
{
	static void Main()
	{
		int n = int.Parse(Console.ReadLine());

		var r = new List<int>();
		var dq = new DQ<int>();

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

	public DQ(int size = 8)
	{
		a = new T[size << 1];
		fiIn = liEx = size;
	}

	public int Length => liEx - fiIn;
	public T First => a[fiIn];
	public T Last => a[liEx - 1];
	public T this[int i]
	{
		get { return a[fiIn + i]; }
		set { a[fiIn + i] = value; }
	}

	public void PushFirst(T v)
	{
		if (fiIn == 0) Expand();
		a[--fiIn] = v;
	}
	public void PushLast(T v)
	{
		if (liEx == a.Length) Expand();
		a[liEx++] = v;
	}

	public T PopFirst() => a[fiIn++];
	public T PopLast() => a[--liEx];

	void Expand()
	{
		var b = new T[a.Length << 1];
		var d = a.Length >> 1;
		a.CopyTo(b, d);
		a = b;
		fiIn += d;
		liEx += d;
	}
}
