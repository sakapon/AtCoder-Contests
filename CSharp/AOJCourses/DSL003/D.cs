using System;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], l = h[1];
		var a = Read();

		Console.WriteLine(string.Join(" ", SlideMin(a, l)));
	}

	static int[] SlideMin(int[] a, int k)
	{
		var r = new int[a.Length - k + 1];
		var q = new int[a.Length];
		for (int i = 1 - k, j = 0, s = 0, t = -1; j < a.Length; i++, j++)
		{
			while (s <= t && a[q[t]] >= a[j]) t--;
			q[++t] = j;
			if (i < 0) continue;
			r[i] = a[q[s]];
			if (q[s] == i) s++;
		}
		return r;
	}
}
