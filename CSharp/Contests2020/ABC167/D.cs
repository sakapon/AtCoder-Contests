using System;

class D
{
	static int[] Read() => Array.ConvertAll(("0 " + Console.ReadLine()).Split(), int.Parse);
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, k) = Read2L();
		var a = Read();

		var townOrderMap = Array.ConvertAll(new int[n + 1], _ => -1);
		var orderTownMap = new int[n + 1];
		townOrderMap[1] = 0;
		orderTownMap[0] = 1;

		for (int i = 1; ; i++)
		{
			var pt = orderTownMap[i - 1];
			var nt = a[pt];
			var nto = townOrderMap[nt];

			if (nto == -1)
			{
				townOrderMap[nt] = i;
				orderTownMap[i] = nt;
			}
			else
			{
				if (k <= nto)
				{
					Console.WriteLine(orderTownMap[k]);
				}
				else
				{
					var period = i - nto;
					Console.WriteLine(orderTownMap[nto + (k - nto) % period]);
				}
				return;
			}
		}
	}
}
