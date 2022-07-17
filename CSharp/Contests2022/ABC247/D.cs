using System;
using System.Collections.Generic;

class D
{
	class BallSet
	{
		public long x, c;
	}

	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => ReadL());

		var q = new Queue<BallSet>();

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var z in qs)
		{
			if (z[0] == 1)
			{
				q.Enqueue(new BallSet { x = z[1], c = z[2] });
			}
			else
			{
				var c = z[1];
				var r = 0L;

				while (c > 0)
				{
					var bs = q.Peek();

					if (c < bs.c)
					{
						r += bs.x * c;
						bs.c -= c;
						c = 0;
					}
					else
					{
						r += bs.x * bs.c;
						q.Dequeue();
						c -= bs.c;
					}
				}

				Console.WriteLine(r);
			}
		}
		Console.Out.Flush();
	}
}
