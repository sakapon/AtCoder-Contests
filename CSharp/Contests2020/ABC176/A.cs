﻿using System;

class A
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		Console.WriteLine((h[0] + h[1] - 1) / h[1] * h[2]);
	}
}
