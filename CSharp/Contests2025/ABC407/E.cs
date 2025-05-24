class E
{
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Array.ConvertAll(new bool[2 * n], _ => long.Parse(Console.ReadLine()));

		var r = a[0];
		var q = new PriorityQueue<long, long>();

		for (int i = 1; i < n; i++)
		{
			var v = a[2 * i - 1];
			q.Enqueue(v, -v);
			v = a[2 * i];
			q.Enqueue(v, -v);
			r += q.Dequeue();
		}
		return r;
	}
}
