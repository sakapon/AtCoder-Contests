using System;

class A2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		Console.WriteLine(string.Join(" ", BucketSort(a, 10000)));
	}

	static int[] Tally(int[] a, int max)
	{
		var c = new int[max + 1];
		foreach (var x in a) ++c[x];
		return c;
	}

	static int[] BucketSort(int[] a, int max)
	{
		var s = Tally(a, max);
		for (int i = 0; i < max; ++i) s[i + 1] += s[i];
		var r = new int[a.Length];
		for (int i = a.Length - 1; i >= 0; --i) r[--s[a[i]]] = a[i];
		return r;
	}

	static T[] BucketSort<T>(T[] a, Func<T, int> toKey, int max)
	{
		var keys = Array.ConvertAll(a, x => toKey(x));
		var s = Tally(keys, max);
		for (int i = 0; i < max; ++i) s[i + 1] += s[i];
		var r = new T[a.Length];
		for (int i = a.Length - 1; i >= 0; --i) r[--s[keys[i]]] = a[i];
		return r;
	}
}
