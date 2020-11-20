using System;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		Console.ReadLine();
		var s = Console.ReadLine();
		//var h = Read();
		//int n = h[0], m = h[1];
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var ps = Array.ConvertAll(new bool[n], _ => Read());

		Console.WriteLine(string.Join(" ", a));
	}
}
