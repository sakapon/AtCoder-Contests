﻿using System;
using System.Linq;

class A
{
	static void Main()
	{
		var k = int.Parse(Console.ReadLine());
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		int a = h[0], b = h[1];
		Console.WriteLine(Enumerable.Range(1, 1000).Any(x => a <= k * x && k * x <= b) ? "OK" : "NG");
	}
}
