using System;
using System.Linq;

class F
{
	static void Main()
	{
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int m = a[0], k = a[1], pm = (int)Math.Pow(2, m);

		if (k >= pm) { Console.WriteLine(-1); return; }
		if (m == 1) { Console.WriteLine(k == 0 ? "0 0 1 1" : "-1"); return; }
		var ek = new[] { k };
		var er = Enumerable.Range(0, pm).Where(x => x != k);
		Console.WriteLine(string.Join(" ", ek.Concat(er).Concat(ek).Concat(er.Reverse())));
	}
}
