using System;
using System.Collections.Generic;
using System.Linq;

class G
{
	class CX
	{
		public char c;
		public long x;
	}

	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var r = new List<long>();
		var q = new Queue<CX>();

		for (int i = 0; i < n; i++)
		{
			var s = Console.ReadLine().Split();
			if (s[0] == "1")
			{
				q.Enqueue(new CX { c = s[1][0], x = long.Parse(s[2]) });
			}
			else
			{
				var d = long.Parse(s[1]);
				var del = new long[26];

				while (d > 0 && q.Any())
				{
					var cx = q.Peek();
					if (cx.x <= d)
					{
						q.Dequeue();
						del[cx.c - 'a'] += cx.x;
						d -= cx.x;
					}
					else
					{
						cx.x -= d;
						del[cx.c - 'a'] += d;
						d = 0;
					}
				}
				r.Add(del.Sum(x => x * x));
			}
		}
		Console.WriteLine(string.Join("\n", r));
	}
}
