class C2
{
	record Tuple(int Count, long Value)
	{
		public int Count { get; set; } = Count;
	}

	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var r = new List<long>();
		var q = new Queue<Tuple>();

		foreach (var z in qs)
		{
			if (z[0] == 1)
			{
				q.Enqueue(new(z[1], z[2]));
			}
			else
			{
				var k = z[1];
				var s = 0L;

				while (k > 0)
				{
					var (c0, x0) = q.Peek();

					var d = Math.Min(k, c0);
					s += x0 * d;
					k -= d;

					if (d < c0)
						q.Peek().Count -= d;
					else
						q.Dequeue();
				}
				r.Add(s);
			}
		}
		return string.Join("\n", r);
	}
}
