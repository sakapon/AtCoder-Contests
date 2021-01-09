using System;

class BL
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		var h = Read();
		var n = h[0];
		var a = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);

		var st = new LST<long, long>(n,
			(x, y) => x + y, 0,
			(x, y) => x + y, 0,
			(x, p, _, l) => p + x * l,
			a);

		for (int k = 0; k < h[1]; k++)
		{
			var q = Read();
			if (q[0] == 0)
				st.Set(q[1], q[2]);
			else
				Console.WriteLine(st.Get(q[1], q[2]));
		}
		Console.Out.Flush();
	}
}
