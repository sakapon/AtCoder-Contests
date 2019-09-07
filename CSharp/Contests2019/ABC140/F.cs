using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = new Queue<int>(Console.ReadLine().Split().Select(int.Parse).OrderByDescending(x => x));

		var q = new int[n + 1].Select(_ => new List<int>()).ToArray();
		q[n].Add(s.Dequeue());
		for (var i = n; i > 0; i--)
			while (q[i].Any())
			{
				for (var j = i - 1; j >= 0; j--)
				{
					var hp = s.Dequeue();
					if (hp >= q[i][0]) { Console.WriteLine("No"); return; }
					if (j > 0) q[j].Add(hp);
				}
				q[i].RemoveAt(0);
			}
		Console.WriteLine("Yes");
	}
}
