﻿using System;
using System.Collections.Generic;

class A
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var set = new BinarySearchTree<int>();

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		while (n-- > 0)
		{
			var q = Console.ReadLine().Split();
			if (q[0] == "insert")
			{
				var v = int.Parse(q[1]);
				set.Add(v);
			}
			else
			{
				Console.WriteLine(" " + string.Join(" ", set.GetItems()));
				set.Root.WalkByPreorder(v => Console.Write($" {v}"));
				Console.WriteLine();
			}
		}
		Console.Out.Flush();
	}
}
