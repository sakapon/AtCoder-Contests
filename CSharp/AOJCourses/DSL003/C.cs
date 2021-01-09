using System;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0];
		var a = Read();
		var seq = new Seq(a);
		var q = ReadL();

		foreach (var x in q)
		{
			var c = 0L;
			for (int l = 0, r = 0; l < n; l++)
			{
				while (r < n && seq.Sum(l, r + 1) <= x) r++;
				c += r - l;
			}
			Console.WriteLine(c);
		}
	}
}
