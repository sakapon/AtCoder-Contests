﻿using System;
using System.Numerics;
using System.Linq;

class D2
{
	static void Main()
	{
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();

		BigInteger c = a[0] - a[1] + 1;
		for (var i = 1; i <= a[1]; Console.WriteLine(c % 1000000007), c = c * (a[0] - a[1] + 2 - ++i) / i * (a[1] - i + 1) / (i - 1)) ;
	}
}
