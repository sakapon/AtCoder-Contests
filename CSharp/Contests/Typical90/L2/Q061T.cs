using System;
using System.Collections.Generic;
using CoderLib6.DataTrees.Bsts;

class Q061T
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var l = new AvlList<int>();

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var (t, x) in qs)
		{
			if (t == 1)
			{
				l.Prepend(x);
			}
			else if (t == 2)
			{
				l.Add(x);
			}
			else
			{
				Console.WriteLine(l[x - 1]);
			}
		}
		Console.Out.Flush();
	}
}

namespace CoderLib6.DataTrees.Bsts
{
	public class AvlList<T>
	{
		public int Count { get; }

		public T this[int index]
		{
			get => throw new NotImplementedException();
			set => throw new NotImplementedException();
		}
		public void Prepend(T item) => throw new NotImplementedException();
		public void Add(T item) => throw new NotImplementedException();
		public void Insert(int index, T item) => throw new NotImplementedException();
		public T RemoveAt(int index) => throw new NotImplementedException();
	}
}
