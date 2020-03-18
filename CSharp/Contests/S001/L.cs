using System;
using System.Linq;

class L
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();

		var c = 0;
		for (int i = 0; i < n; i++)
		{
			while (a[i] != i + 1)
			{
				Swap(a, i, a[i] - 1);
				c++;
			}
		}
		Console.WriteLine((n - c) % 2 == 0 ? "YES" : "NO");
	}

	static void Swap<T>(T[] a, int i, int j) { var o = a[i]; a[i] = a[j]; a[j] = o; }
}
