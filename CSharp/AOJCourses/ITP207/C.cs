using System;
using System.Collections.Generic;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var set = new SortedSet<int>();

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		while (n-- > 0)
		{
			var q = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
			if (q[0] == 0)
			{
				set.Add(q[1]);
				Console.WriteLine(set.Count);
			}
			else if (q[0] == 1)
				Console.WriteLine(set.Contains(q[1]) ? 1 : 0);
			else if (q[0] == 2)
				set.Remove(q[1]);
			else
				foreach (var x in set.GetViewBetween(q[1], q[2]))
					Console.WriteLine(x);
		}
		Console.Out.Flush();
	}
}
