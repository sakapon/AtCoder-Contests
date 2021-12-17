using System;
using System.Collections.Generic;
using CoderLib6.DataTrees.Bsts;

class ET
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var l = new AvlList<int>();

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		for (int i = 0; i < n; i++)
		{
			var c = s[i];

			if (c == 'L')
			{
				l.Prepend(i + 1);
			}
			else if (c == 'R')
			{
				l.Add(i + 1);
			}
			else if (c < 'D')
			{
				var k = c - 'A';
				if (k < l.Count)
					Console.WriteLine(l.RemoveAt(k));
				else
					Console.WriteLine("ERROR");
			}
			else
			{
				var k = c - 'D';
				if (k < l.Count)
					Console.WriteLine(l.RemoveAt(l.Count - 1 - k));
				else
					Console.WriteLine("ERROR");
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
