class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w, n) = Read3();
		var t = Console.ReadLine();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var delta = new int[1 << 7];
		delta['L'] = -1;
		delta['R'] = 1;
		delta['U'] = -w;
		delta['D'] = w;

		var ss = s.SelectMany(c => c).ToArray();
		return Enumerable.Range(0, h * w).Count(Check);

		bool Check(int v)
		{
			if (ss[v] == '#') return false;
			foreach (var c in t)
				if (ss[v += delta[c]] == '#') return false;
			return true;
		}
	}
}
