using System;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, k) = Read2();
		var p = Read();

		Array.Sort(p);
		Array.Reverse(p);
		while (0 < k && k < n && p[k - 1] == p[k]) k--;
		Console.WriteLine(k);
	}
}
