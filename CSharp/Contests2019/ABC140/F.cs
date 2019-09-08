using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine().Split().Select(int.Parse).OrderByDescending(x => x).ToArray();

		var q = new int[n + 1].Select(_ => new List<int>()).ToList();
		q[n].Add(int.MaxValue);
		foreach (var hp in s)
		{
			var match = false;
			for (var i = q.Count - 1; i >= 0; i--)
				if (hp < q[i][0])
				{
					for (var j = i - 1; j >= 0; j--) q[j].Add(hp);
					q[i].RemoveAt(0);
					if (!q[i].Any()) q.RemoveAt(i);
					match = true;
					break;
				}
			if (!match) { Console.WriteLine("No"); return; }
		}
		Console.WriteLine("Yes");
	}
}
