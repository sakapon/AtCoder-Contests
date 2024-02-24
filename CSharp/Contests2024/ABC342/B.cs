class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var p = Read();
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var r = new List<int>();

		foreach (var (a, b) in qs)
		{
			var ai = Array.IndexOf(p, a);
			var bi = Array.IndexOf(p, b);
			r.Add(ai < bi ? a : b);
		}
		return string.Join("\n", r);
	}
}
