using Bang.Graphs.Typed.SPPs.Unweighted.v1_0_2;

class D
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine() + "..";
		var t = Console.ReadLine() + "..";

		var g = new DGraph();
		var r = g.ShortestByBFS(s, t);
		if (!r.ContainsKey(t)) return -1;
		return r[t];
	}
}

public class DGraph : UnweightedGraph<string>
{
	public override int VertexesCount => -1;
	public override IEnumerable<string> GetVertexes() => null;

	public override List<string> GetEdges(string v)
	{
		var r = new List<string>();

		var cs = v.ToCharArray();
		var ei = v.IndexOf('.');

		for (int i = 0; i < ei - 1; i++)
		{
			var c0 = cs[i];
			var c1 = cs[i + 1];
			cs[i] = '.';
			cs[i + 1] = '.';
			cs[ei] = c0;
			cs[ei + 1] = c1;
			r.Add(new string(cs));
			cs[i] = c0;
			cs[i + 1] = c1;
		}
		for (int i = ei + 2; i < v.Length - 1; i++)
		{
			var c0 = cs[i];
			var c1 = cs[i + 1];
			cs[i] = '.';
			cs[i + 1] = '.';
			cs[ei] = c0;
			cs[ei + 1] = c1;
			r.Add(new string(cs));
			cs[i] = c0;
			cs[i + 1] = c1;
		}

		return r;
	}
}
