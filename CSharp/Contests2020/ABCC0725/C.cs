using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void WriteYesNo(bool b) => Console.WriteLine(b ? "Yes" : "No");
	static void Main()
	{
		var (n, k) = Read2();
		var a = ReadL();

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		for (int i = k; i < n; i++)
		{
			WriteYesNo(a[i - k] < a[i]);
		}
		Console.Out.Flush();
	}
}
