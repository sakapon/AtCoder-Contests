using System;

class JG
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		var h = Read();
		var n = h[0];
		var a = Read();

		var st = new ST1<int>(n, Math.Max, 0, a);

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
					if (p < n || st[node] < q[2]) return p;
					while (node.i < st.n2 >> 1)
						node = q[2] <= st[node.Child0] ? node.Child0 : node.Child1;
					return st.Original(node);
				}));
		}
		Console.Out.Flush();
	}
}
