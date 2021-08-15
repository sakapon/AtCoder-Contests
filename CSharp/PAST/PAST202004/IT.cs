using System;
using System.Collections.Generic;
using CoderLib6.DataTrees.Bsts;

class IT
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var r = Array.ConvertAll(a, _ => n);

		var l = new AvlList<(int v, int i)>();
		for (int i = 0; i < a.Length; i++)
			l.Add((a[i], i));

		for (int k = 1; k < n; k++)
		{
			for (int i = 0; i < l.Count; i++)
			{
				var (v0, i0) = l[i];
				var (v1, i1) = l[i + 1];
				r[v0 < v1 ? i0 : i1] = k;
				l.RemoveAt(v0 < v1 ? i : i + 1);
			}
		}
		Console.WriteLine(string.Join("\n", r));
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
