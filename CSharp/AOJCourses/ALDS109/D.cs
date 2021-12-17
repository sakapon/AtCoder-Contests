using System;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		// WA
		Array.Sort(a);
		Array.Reverse(a);

		for (int i = 0, l = 1; i < n; i += l, l <<= 1)
		{
			Array.Reverse(a, i, Math.Min(l, n - i));
		}

		Console.WriteLine(string.Join(" ", a));
	}
}
