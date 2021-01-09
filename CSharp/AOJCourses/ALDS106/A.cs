using System;

class A
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		BucketSort(a, 10000);
		Console.WriteLine(string.Join(" ", a));
	}

	static int[] Count(int[] a, int max)
	{
		var r = new int[max + 1];
		foreach (var x in a) ++r[x];
		return r;
	}

	static void BucketSort(int[] a, int max)
	{
		var b = Count(a, max);
		for (int i = -1, x = 0; x <= max; ++x)
			while (b[x]-- > 0)
				a[++i] = x;
	}
}
