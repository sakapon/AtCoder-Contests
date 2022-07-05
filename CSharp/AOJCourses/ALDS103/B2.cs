using System;

class B2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, qt) = Read2();

		var q = new ArrayQueue<(string, int)>(1000000);
		while (n-- > 0)
		{
			var p = Console.ReadLine().Split();
			q.Push((p[0], int.Parse(p[1])));
		}
		var t = 0;

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		while (q.Count > 0)
		{
			var (name, time) = q.Pop();
			if (time <= qt)
			{
				Console.WriteLine($"{name} {t += time}");
			}
			else
			{
				t += qt;
				q.Push((name, time - qt));
			}
		}
		Console.Out.Flush();
	}
}

public class ArrayQueue<T>
{
	T[] a;
	int fi, li;
	public int Count => li - fi;

	public ArrayQueue(int capacity) => a = new T[capacity];

	public void Push(T item) => a[li++] = item;
	public T Pop() => a[fi++];
}
