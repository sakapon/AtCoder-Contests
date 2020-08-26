using System;
using System.Collections.Generic;
using System.Linq;

class C2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var qs = new int[n].Select(_ => Console.ReadLine().Split());

		var dq = new DQ<int>(n);

		var actions = new Dictionary<string, Action<string[]>>();
		actions["insert"] = q => dq.PushFirst(int.Parse(q[1]));
		actions["delete"] = q => dq.RemoveFirst(int.Parse(q[1]));
		actions["deleteFirst"] = q => dq.PopFirst();
		actions["deleteLast"] = q => dq.PopLast();

		foreach (var q in qs)
			actions[q[0]](q);
		Console.WriteLine(string.Join(" ", Enumerable.Range(0, dq.Length).Select(i => dq[i])));
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

	// この問題用に追加。
	public void RemoveFirst(T v)
	{
		var c = EqualityComparer<T>.Default;
		for (int i = fiIn; i < liEx; i++)
		{
			if (c.Equals(a[i], v))
			{
				for (int j = i; j > fiIn; j--)
					a[j] = a[j - 1];
				fiIn++;
				break;
			}
		}
	}
}
