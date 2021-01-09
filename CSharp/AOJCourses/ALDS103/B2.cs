using System;
using System.Collections.Generic;
using System.Linq;

class B2
{
	static void Main()
	{
		var r = new List<string>();
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int n = h[0], q = h[1];
		var ps = new int[h[0]].Select(_ => Console.ReadLine().Split()).Select(x => Tuple.Create(x[0], int.Parse(x[1])));

		var queue = new Queue0<Tuple<string, int>>(1000000);
		foreach (var p in ps) queue.Push(p);

		var t = 0;
		while (queue.Length > 0)
		{
			var v = queue.Pop();
			if (v.Item2 <= q)
			{
				r.Add($"{v.Item1} {t += v.Item2}");
			}
			else
			{
				t += q;
				queue.Push(Tuple.Create(v.Item1, v.Item2 - q));
			}
		}
		Console.WriteLine(string.Join("\n", r));
	}
}

class Queue0<T>
{
	T[] a;
	int fiIn, liEx;

	public Queue0(int size) { a = new T[size]; }

	public int Length => liEx - fiIn;
	public T First => a[fiIn];
	public T this[int i] => a[fiIn + i];

	public void Push(T v) => a[liEx++] = v;
	public T Pop() => a[fiIn++];
}
