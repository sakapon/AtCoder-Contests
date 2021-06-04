using System;

class Q044L
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var (n, qc) = Read2();
		var a = Read();
		var qs = Array.ConvertAll(new bool[qc], _ => Read3());

		var dq = new DQ<int>(n + qc);
		foreach (var x in a)
			dq.PushLast(x);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var (t, x, y) in qs)
		{
			if (t == 1)
			{
				(dq[x - 1], dq[y - 1]) = (dq[y - 1], dq[x - 1]);
			}
			else if (t == 2)
			{
				dq.PushFirst(dq.PopLast());
			}
			else
			{
				Console.WriteLine(dq[x - 1]);
			}
		}
		Console.Out.Flush();
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
	public T this[int i]
	{
		get { return a[fiIn + i]; }
		set { a[fiIn + i] = value; }
	}

	public void PushFirst(T v)
	{
		if (Length == 0) PushLast(v);
		else a[--fiIn] = v;
	}
	public void PushLast(T v) => a[liEx++] = v;
	public T PopFirst() => Length == 1 ? PopLast() : a[fiIn++];
	public T PopLast() => a[--liEx];
}
