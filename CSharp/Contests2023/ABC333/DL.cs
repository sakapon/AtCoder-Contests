using CoderLib8.Graphs.Trees.Trees2401;

class DL
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());

		var map = Tree.ToMap(n + 1, es, true);
		var tree = new Tree(map, 1);
		return n - tree.Root.Children.Max(node => node.Count);
	}
}
