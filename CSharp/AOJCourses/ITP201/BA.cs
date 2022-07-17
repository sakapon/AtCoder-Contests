using System;

class BA
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var qc = Read()[0];

		var dq = new ArrayDeque<int>(qc);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		while (qc-- > 0)
		{
			var q = Read();
			if (q[0] == 0)
			{
				if (q[1] == 0) dq.PushFirst(q[2]);
				else dq.PushLast(q[2]);
			}
			else if (q[0] == 1)
			{
				Console.WriteLine(dq[q[1]]);
			}
			else
			{
				if (q[1] == 0) dq.PopFirst();
				else dq.PopLast();
			}
		}
		Console.Out.Flush();
	}
}

public class ArrayDeque<T>
{
	T[] a;
	int fi, li;

	public ArrayDeque(int capacity) => a = new T[(fi = li = capacity) << 1];

	public T this[int i] => a[fi + i];
	public void PushFirst(T item) => a[--fi] = item;
	public void PushLast(T item) => a[li++] = item;
	public T PopFirst() => a[fi++];
	public T PopLast() => a[--li];
}
