using System;
using System.Collections.Generic;

class Q027
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var set = new HashSet<string>();

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		for (int i = 1; i <= n; i++)
		{
			var s = Console.ReadLine();
			if (set.Add(s)) Console.WriteLine(i);
		}
		Console.Out.Flush();
	}
}
