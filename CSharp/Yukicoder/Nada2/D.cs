﻿using System;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		//var (n, m) = Read2();
		var s = Console.ReadLine();
		var a = Read();
		var ps = Array.ConvertAll(new bool[n], _ => Read());

		Console.WriteLine(string.Join(" ", a));
	}
}
