class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var q = new Stack<(int value, int count)>();
		q.Push((0, 0));

		foreach (var v in a)
		{
			var (value, count) = q.Peek();

			if (v == value)
			{
				q.Pop();
				if (++count < 4) q.Push((value, count));
			}
			else
			{
				q.Push((v, 1));
			}
		}

		return q.Sum(p => p.count);
	}
}
