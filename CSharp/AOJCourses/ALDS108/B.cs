using System;
using System.Collections.Generic;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var set = new BSTree<int>();

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		while (n-- > 0)
		{
			var q = Console.ReadLine().Split();
			if (q[0] == "insert")
			{
				var v = int.Parse(q[1]);
				set.Add(v);
			}
			else if (q[0] == "find")
			{
				var v = int.Parse(q[1]);
				Console.WriteLine(set.Contains(v) ? "yes" : "no");
			}
			else
			{
				Console.WriteLine(" " + string.Join(" ", set.GetItems()));
				var r = new List<int>();
				set.Root.WalkByPreorder(r.Add);
				Console.WriteLine(" " + string.Join(" ", r));
			}
		}
		Console.Out.Flush();
	}
}
