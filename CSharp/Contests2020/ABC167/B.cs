using System;

class B
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long, long) Read4L() { var a = ReadL(); return (a[0], a[1], a[2], a[3]); }
	static void Main()
	{
		var (a, b, c, k) = Read4L();
		var r = 0L;

		var t = Math.Min(a, k);
		k -= t;
		r += t;

		t = Math.Min(b, k);
		k -= t;

		t = Math.Min(c, k);
		k -= t;
		r -= t;

		Console.WriteLine(r);
	}
}
