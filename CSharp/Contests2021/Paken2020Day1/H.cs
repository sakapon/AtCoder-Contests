using System;

class H
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void WriteYesNo(bool b) => Console.WriteLine(b ? "Yes" : "No");
	static void Main()
	{
		var t = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[t], _ => Read3L());

		foreach (var (a, b, c) in qs)
		{
			WriteYesNo(Check(a, b, c));
		}

		bool Check(long a, long b, long c)
		{
			bool? isOdd = null;

			for (int i = 0; i < 60; i++)
			{
				var f = 1 << i;
				var af = (a & f) != 0;
				var bf = (b & f) != 0;
				var cf = (c & f) != 0;

				if (af)
				{
					if (bf)
					{
						if (isOdd == null || isOdd == cf)
						{
							isOdd = cf;
						}
						else
						{
							return false;
						}
					}
					else
					{

					}
				}
				else
				{
					if (bf)
					{
						return false;
					}
					else
					{
						if (cf) return false;
					}
				}
			}
			return true;
		}
	}
}
