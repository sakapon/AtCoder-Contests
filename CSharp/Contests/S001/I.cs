using System;
using System.Linq;

class I
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();

		var s = CumSum(a);
		Console.WriteLine(s.Count(x => Array.BinarySearch(s, x + n) >= 0));
	}

	static long[] CumSum(int[] a)
	{
		var s = new long[a.Length + 1];
		for (int i = 0; i < a.Length; i++) s[i + 1] = s[i] + a[i];
		return s;
	}
}
