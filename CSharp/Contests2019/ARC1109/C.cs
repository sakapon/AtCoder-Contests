using System;
using System.Linq;

class C
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var n = int.Parse(Console.ReadLine());
		var a = read();
		var b = read();
		var ab = a.Zip(b, (x, y) => new { x, y }).OrderBy(_ => _.y).ThenBy(_ => _.x).ToArray();
		var ao = a.OrderBy(x => x).ToArray();

		if (Enumerable.Range(0, n).Any(i => ao[i] > ab[i].y)) { Console.WriteLine("No"); return; }
		if (Enumerable.Range(0, n).Any(i => ab[i].x <= ab[i].y && ao[i] <= ab[i].x)) { Console.WriteLine("Yes"); return; }

		int k = 0, c = 0;
		while (true)
		{
			c++;
			if ((k = Array.BinarySearch(ao, ab[k].x)) == 0) break;
		}
		Console.WriteLine(c != n ? "Yes" : "No");
	}
}
