class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var l = new LinkedList<int>();
		var d = new Dictionary<int, LinkedListNode<int>>();

		foreach (var x in a)
		{
			d[x] = l.AddLast(x);
		}

		foreach (var q in qs)
		{
			var x = q[1];

			if (q[0] == 1)
			{
				var y = q[2];
				d[y] = l.AddAfter(d[x], y);
			}
			else
			{
				l.Remove(d[x]);
			}
		}

		return string.Join(" ", l);
	}
}
