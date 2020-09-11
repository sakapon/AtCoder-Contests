using System;

class A2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		Console.WriteLine(string.Join(" ", BucketSort(a, 10000)));
	}

	static int[] Count(int[] a, int max)
	{
		var c = new int[max + 1];
		foreach (var x in a) ++c[x];
		return c;
	}

	static int[] CumSum(int[] a)
	{
		var s = new int[a.Length + 1];
		for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
		return s;
	}

	static int[] BucketSort(int[] a, int max)
	{
		var s = CumSum(Count(a, max));
		var r = new int[a.Length];
		for (int i = 0; i < a.Length; ++i) r[s[a[i]]++] = a[i];
		return r;
	}

	static T[] BucketSort<T>(T[] a, Func<T, int> toKey, int max)
	{
		var keys = Array.ConvertAll(a, x => toKey(x));
		var s = CumSum(Count(keys, max));
		var r = new T[a.Length];
		for (int i = 0; i < a.Length; ++i) r[s[keys[i]]++] = a[i];
		return r;
	}
}
