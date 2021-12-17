using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();

		var q = new DQ<char>();
		var rev = false;

		foreach (var c in s)
		{
			if (c == 'R')
			{
				rev = !rev;
			}
			else
			{
				if (rev)
				{
					if (q.Length > 0 && q.First == c) q.PopFirst();
					else q.PushFirst(c);
				}
				else
				{
					if (q.Length > 0 && q.Last == c) q.PopLast();
					else q.PushLast(c);
				}
			}
		}

		if (rev) return string.Join("", Enumerable.Range(0, q.Length).Select(i => q[i]).Reverse());
		else return string.Join("", Enumerable.Range(0, q.Length).Select(i => q[i]));
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
