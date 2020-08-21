using System;
using System.Linq;

class B
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int n = h[0], m = h[1];
		var a = Read();

		var d = a.Sum() / (4D * m);
		Console.WriteLine(a.Count(x => x >= d) >= m ? "Yes" : "No");
	}
}
