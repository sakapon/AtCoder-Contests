class E2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ss = Console.ReadLine().Split();

		var r = 0L;
		var root = new Node();

		foreach (var s in ss)
		{
			var node = root;

			foreach (var c in s)
			{
				node = (node.Children[c - 'a'] ??= new Node());
				r += node.Count++;
			}
		}
		return r;
	}

	class Node
	{
		public int Count;
		public Node[] Children = new Node[26];
	}
}
