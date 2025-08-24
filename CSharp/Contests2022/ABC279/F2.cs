using System;
using System.Collections.Generic;

class F2
{
	class Box
	{
		public int Id;
		public HashSet<int> Balls = new HashSet<int>();
	}

	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var r = new List<int>();

		var boxes = new Box[n + 1];
		// ball -> box
		var balls = new List<Box>();

		for (int i = 0; i <= n; i++)
		{
			var box = new Box { Id = i, Balls = new HashSet<int> { i } };
			boxes[i] = box;
			balls.Add(box);
		}

		foreach (var q in qs)
		{
			var x = q[1];

			if (q[0] == 1)
			{
				var y = q[2];

				if (boxes[x].Balls.Count < boxes[y].Balls.Count)
				{
					(boxes[x], boxes[y]) = (boxes[y], boxes[x]);
					boxes[x].Id = x;
					boxes[y].Id = y;
				}

				foreach (var ballId in boxes[y].Balls)
				{
					balls[ballId] = boxes[x];
				}

				Move(ref boxes[y].Balls, ref boxes[x].Balls);
			}
			else if (q[0] == 2)
			{
				var box = boxes[x];
				box.Balls.Add(balls.Count);
				balls.Add(box);
			}
			else
			{
				r.Add(balls[x].Id);
			}
		}
		return string.Join("\n", r);
	}

	public static void Move<T>(ref HashSet<T> from, ref HashSet<T> to)
	{
		if (to.Count < from.Count) (to, from) = (from, to);
		foreach (var v in from) to.Add(v);
		from.Clear();
	}
}
