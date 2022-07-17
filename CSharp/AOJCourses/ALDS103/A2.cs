using System;

class A2
{
	static void Main()
	{
		var es = Console.ReadLine().Split();
		var s = new ArrayStack<int>(200);

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

public class ArrayStack<T>
{
	T[] a;
	int n;

	public ArrayStack(int capacity) => a = new T[capacity];

	public void Push(T item) => a[n++] = item;
	public T Pop() => a[--n];
}
