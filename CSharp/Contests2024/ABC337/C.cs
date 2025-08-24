class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var r = new List<int>();
		var i = Enumerable.Range(1, n).Except(a).Single();
		for (; i != -1; i = a[i - 1])
			r.Add(i);
		r.Reverse();
		return string.Join(" ", r);
	}
}
