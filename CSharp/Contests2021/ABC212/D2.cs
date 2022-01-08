using System;
using System.Collections.Generic;
using System.Linq;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var qc = int.Parse(Console.ReadLine());

		var pq = new BstPriorityQueue<long>();
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

// 要素が重複する場合も利用できます (一般的な優先度付きキュー)。
public class BstPriorityQueue<T>
{
	// 要素をそのままキーとして使用します。
	SortedDictionary<T, int> sd;

	public BstPriorityQueue(IComparer<T> comparer = null)
	{
		sd = new SortedDictionary<T, int>(comparer ?? Comparer<T>.Default);
	}

	public int Count { get; private set; }

	public T Peek()
	{
		if (Count == 0) throw new InvalidOperationException("The container is empty.");

		return sd.First().Key;
	}

	public T Pop()
	{
		if (Count == 0) throw new InvalidOperationException("The container is empty.");

		Count--;
		var (item, count) = sd.First();
		if (count == 1) sd.Remove(item);
		else sd[item] = count - 1;
		return item;
	}

	public void Push(T item)
	{
		Count++;
		sd.TryGetValue(item, out var count);
		sd[item] = count + 1;
	}
}
