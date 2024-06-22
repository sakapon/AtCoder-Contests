class E
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		var r = new List<long>();
		var s = 1L;
		var q = new Stack<(long h, int l)>();
		q.Push((1 << 30, 0));

		foreach (var v in a)
		{
			var len = 1;

			while (q.Peek().h < v)
			{
				var (h, l) = q.Pop();
				len += l;
				s -= h * l;
			}

			q.Push((v, len));
			s += v * len;
			r.Add(s);
		}

		return string.Join(" ", r);
	}
}
