using System;
using System.Collections.Generic;
using System.Linq;

class E2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var q = new DQ<int>();
		var stack = new Stack<int>();

		int RemoveAndGetFirst(int k)
		{
			for (int i = 0; i < k; i++)
			{
				stack.Push(q.PopFirst());
			}
			var r = q.PopFirst();

			for (int i = 0; i < k; i++)
			{
				q.PushFirst(stack.Pop());
			}
			return r;
		}

		int RemoveAndGetLast(int k)
		{
			for (int i = 0; i < k; i++)
			{
				stack.Push(q.PopLast());
			}
			var r = q.PopLast();

			for (int i = 0; i < k; i++)
			{
				q.PushLast(stack.Pop());
			}
			return r;
		}

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		for (int i = 0; i < n; i++)
		{
			var c = s[i];

			if (c == 'L')
			{
				q.PushFirst(i + 1);
			}
			else if (c == 'R')
			{
				q.PushLast(i + 1);
			}
			else if (c < 'D')
			{
				var k = c - 'A';
				if (k < q.Length)
					Console.WriteLine(RemoveAndGetFirst(k));
				else
					Console.WriteLine("ERROR");
			}
			else
			{
				var k = c - 'D';
				if (k < q.Length)
					Console.WriteLine(RemoveAndGetLast(k));
				else
					Console.WriteLine("ERROR");
			}
		}
		Console.Out.Flush();
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
