using System;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], k = h[1];
		var a = Read();

		var u = new int[k + 1];
		var c = 0;
		var m = 1 << 30;

		for (int l = -1, r = -1; l < n;)
		{
			while (r < n - 1 && c < k)
				if (a[++r] <= k && u[a[r]]++ == 0) c++;
			if (c < k) break;
			Chmin(ref m, r - l);

			if (a[++l] <= k && --u[a[l]] == 0) c--;
		}
		Console.WriteLine(m == 1 << 30 ? 0 : m);
	}

	static int Chmin(ref int x, int v) => x > v ? x = v : x;
}
