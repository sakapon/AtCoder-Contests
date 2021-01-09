using System;

class A2
{
	static void Main()
	{
		var es = Console.ReadLine().Split();
		var s = new Stack0<int>(200);

		foreach (var e in es)
		{
			switch (e)
			{
				case "+":
					s.Push(s.Pop() + s.Pop());
					break;
				case "-":
					s.Push(-s.Pop() + s.Pop());
					break;
				case "*":
					s.Push(s.Pop() * s.Pop());
					break;
				default:
					s.Push(int.Parse(e));
					break;
			}
		}
		Console.WriteLine(s.Pop());
	}
}

class Stack0<T>
{
	T[] a;
	int liEx;

	public Stack0(int size) { a = new T[size]; }

	public int Length => liEx;
	public T First => a[liEx - 1];
	public T this[int i] => a[i];

	public void Push(T v) => a[liEx++] = v;
	public T Pop() => a[--liEx];
}
