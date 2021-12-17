using System;
using System.Linq;

class I2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var r = Array.ConvertAll(a, _ => n);
		var vis = a.Select((v, i) => (v, i)).ToArray();

		for (int k = 1; k < n; k++)
		{
			var next = new (int v, int i)[vis.Length / 2];
			for (int i = 0; i < next.Length; i++)
			{
				var (v0, i0) = vis[2 * i];
				var (v1, i1) = vis[2 * i + 1];
				r[v0 < v1 ? i0 : i1] = k;
				next[i] = v0 < v1 ? (v1, i1) : (v0, i0);
			}
			vis = next;
		}
		Console.WriteLine(string.Join("\n", r));
	}
}
