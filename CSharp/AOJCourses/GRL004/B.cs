using System;
using System.Collections.Generic;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		var es = Array.ConvertAll(new bool[h[1]], _ => Read());

		var r = DirectedGraphHelper.TopologicalSort(h[0], es);
		Console.WriteLine(string.Join("\n", r));
	}
}
