using System;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, x) = Read2();
		var s = Console.ReadLine();

		foreach (var c in s)
			if (c == 'o') x++;
			else if (x > 0) x--;
		Console.WriteLine(x);
	}
}
