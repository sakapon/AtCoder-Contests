using System;

class Q061
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var dq = new DQ<int>(qc);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var (t, x) in qs)
		{
			if (t == 1)
			{
				dq.PushFirst(x);
			}
			else if (t == 2)
			{
				dq.PushLast(x);
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
