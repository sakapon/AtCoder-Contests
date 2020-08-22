using System;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

		var r = 0L;
		var M = 0;
		foreach (var x in a)
			if (x < M) r += M - x;
			else M = x;
		Console.WriteLine(r);
	}
}
