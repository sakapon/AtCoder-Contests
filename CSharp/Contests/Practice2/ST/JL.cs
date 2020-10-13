using System;

class JL
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		var h = Read();
		var n = h[0];
		var a = Read();

		var st = new LST<int, int>(n,
			(x, y) => x == int.MinValue ? y : x, int.MinValue,
			Math.Max, int.MinValue,
			(x, p, _, l) => x == int.MinValue ? p : x,
			a);

		for (int k = 0; k < h[1]; k++)
		{
			var q = Read();
			if (q[0] == 1)
				st.Set(q[1] - 1, q[2]);
			else if (q[0] == 2)
				Console.WriteLine(st.Get(q[1] - 1, q[2]));
			else
				Console.WriteLine(1 + st.Aggregate(q[1] - 1, n, n, (p, node, l) =>
				{
					if (p < n || st.a2[node.i] < q[2]) return p;
					while (node.i < st.n2 >> 1)
						node = q[2] <= st.a2[node.Child0.i] ? node.Child0 : node.Child1;
					return st.Original(node);
				}));
		}
		Console.Out.Flush();
	}
}
