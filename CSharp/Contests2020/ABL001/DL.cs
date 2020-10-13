using System;

class DL
{
	const int kM = 300000;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], k = h[1];
		var a = Array.ConvertAll(new int[n], _ => int.Parse(Console.ReadLine()));

		var st = new LST<int, int>(kM + 1,
			Math.Max, 0,
			Math.Max, 0,
			(x, p, _, l) => Math.Max(x, p));

		for (int i = 0; i < n; i++)
			st.Set(Math.Max(0, a[i] - k), Math.Min(kM + 1, a[i] + k + 1), st.Get(a[i]) + 1);

		Console.WriteLine(st.Get(0, kM + 1));
	}
}
