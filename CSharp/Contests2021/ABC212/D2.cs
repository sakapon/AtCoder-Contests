using System;
using System.Collections.Generic;
using System.Linq;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var qc = int.Parse(Console.ReadLine());

		var pq = new BstPQ<long>();
		var d = 0L;

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		while (qc-- > 0)
		{
			var q = Read();
			if (q[0] == 1)
			{
				pq.Push(q[1] - d);
			}
			else if (q[0] == 2)
			{
				d += q[1];
			}
			else
			{
				Console.WriteLine(pq.Pop() + d);
			}
		}
		Console.Out.Flush();
	}
}

class BstPQ<T>
{
	SortedDictionary<T, int> d = new SortedDictionary<T, int>();
	public bool Any => d.Count > 0;
	public T First => d.First().Key;

	public void Push(T v)
	{
		int c;
		d.TryGetValue(v, out c);
		d[v] = c + 1;
	}

	public T Pop()
	{
		var v = First;
		if (d[v] == 1) d.Remove(v);
		else --d[v];
		return v;
	}
}
