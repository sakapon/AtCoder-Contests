using System;
using System.Linq;

class B
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var z = Read();
		Console.WriteLine(Read().Sum() >= z[0] ? "Yes" : "No");
	}
}
