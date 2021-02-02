using System;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var k = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();
		if (s.Length <= k)
		{
			Console.WriteLine(s);
		}
		else
		{
			Console.WriteLine(s[..k] + "...");
		}
	}
}
