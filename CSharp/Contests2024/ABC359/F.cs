class F
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		var pq = new PriorityQueue<long, long>();
		foreach (var v in a)
			pq.Enqueue(v, 3 * v);

		var r = a.Sum();
		for (int i = 0; i < n - 2; i++)
		{
			pq.TryDequeue(out var v, out var x);
			r += x;
			pq.Enqueue(v, x + 2 * v);
		}
		return r;
	}
}
