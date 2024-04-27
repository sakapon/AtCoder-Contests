using AlgorithmLib10.SegTrees.SegTrees111;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var r = 0L;
		var sts = new RSQTree(n);
		var stc = new RSQTree(n);

		foreach (var i in Enumerable.Range(0, n).OrderBy(i => -a[i]))
		{
			r += sts[i, n] - stc[i, n] * a[i];

			sts[i] += a[i];
			stc[i]++;
		}
		return r;
	}
}
