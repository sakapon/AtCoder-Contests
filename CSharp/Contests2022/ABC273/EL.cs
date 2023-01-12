using System;
using System.Collections.Generic;

class EL
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var qc = int.Parse(Console.ReadLine());

		var d = new Dictionary<int, ArrayPersistentStack<int>>();
		var a = new ArrayPersistentStack<int>(qc + 1);
		a.Add(-1);

		var r = new int[qc];

		for (int k = 0; k < qc; k++)
		{
			var q = Console.ReadLine().Split();
			var c = q[0][0];

			if (c == 'A')
			{
				var x = int.Parse(q[1]);
				a.Add(x);
			}
			else if (c == 'D')
			{
				if (a.CurrentIndex != 0) a.Pop();
			}
			else if (c == 'S')
			{
				var y = int.Parse(q[1]);
				d[y] = a.Clone();
			}
			else
			{
				var z = int.Parse(q[1]);
				if (d.ContainsKey(z))
				{
					a = d[z].Clone();
				}
				else
				{
					a = a.Clone(0);
				}
			}

			r[k] = a.CurrentValue;
		}

		return string.Join(" ", r);
	}
}

public class ArrayPersistentStack<T>
{
	class Data
	{
		internal readonly T[] a;
		internal readonly int[] p;
		int newIndex;

		public Data(int size)
		{
			a = new T[size];
			p = new int[size];
		}

		public int Add(T item, int prevIndex)
		{
			a[newIndex] = item;
			p[newIndex] = prevIndex;
			return newIndex++;
		}
	}

	readonly Data data;
	int currentIndex = -1;

	public ArrayPersistentStack(int size)
	{
		data = new Data(size);
	}

	ArrayPersistentStack(Data data, int currentIndex)
	{
		this.data = data;
		this.currentIndex = currentIndex;
	}

	public ArrayPersistentStack<T> Clone() => new ArrayPersistentStack<T>(data, currentIndex);
	public ArrayPersistentStack<T> Clone(int currentIndex) => new ArrayPersistentStack<T>(data, currentIndex);

	public int CurrentIndex => currentIndex;
	public T CurrentValue => data.a[currentIndex];

	public void Add(T item)
	{
		currentIndex = data.Add(item, currentIndex);
	}

	public T Pop()
	{
		var item = data.a[currentIndex];
		currentIndex = data.p[currentIndex];
		return item;
	}
}
