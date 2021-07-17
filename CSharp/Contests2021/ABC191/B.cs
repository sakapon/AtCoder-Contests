using System;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var x = Read()[1];
		Console.WriteLine(string.Join(" ", Read().Where(v => v != x)));
	}
}
