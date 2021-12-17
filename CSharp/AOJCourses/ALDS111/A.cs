using System;
using System.Linq;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var m = Array.ConvertAll(new bool[n], _ => new int[n]);
		for (int i = 0; i < n; i++)
		{
			var a = Read();
			foreach (var to in a.Skip(2))
				m[a[0] - 1][to - 1] = 1;
		}

		foreach (var r in m)
			Console.WriteLine(string.Join(" ", r));
	}
}
