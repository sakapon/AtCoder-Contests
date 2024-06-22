class F
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		var pq = new PriorityQueue<(long, long), (long, long)>();
		foreach (var v in a)
			pq.Enqueue((3 * v, v), (3 * v, v));

		var r = a.Sum();
		for (int i = 0; i < n - 2; i++)
		{
			var (x, v) = pq.Dequeue();
			r += x;
			pq.Enqueue((x + 2 * v, v), (x + 2 * v, v));
		}
		return r;
	}
}
