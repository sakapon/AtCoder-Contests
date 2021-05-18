using System;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(':'), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, m) = Read2();

		m += 5;
		if (m >= 60)
		{
			h++;
			h %= 24;
			m -= 60;
		}

		return $"{h:D2}:{m:D2}";
	}
}
