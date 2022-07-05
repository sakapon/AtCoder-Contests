using System;

class AA
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var qc = Read()[0];

		var l = new ArrayList<int>(qc);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		while (qc-- > 0)
		{
			var q = Read();
			if (q[0] == 0)
			{
				l.Add(q[1]);
			}
			else if (q[0] == 1)
			{
				Console.WriteLine(l[q[1]]);
			}
			else
			{
				l.Pop();
			}
		}
		Console.Out.Flush();
	}
}

public class ArrayList<T>
{
	T[] a;
	int n;
	public ArrayList(int capacity) => a = new T[capacity];

	public T this[int i] => a[i];
	public void Add(T item) => a[n++] = item;
	public T Pop() => a[--n];
}
