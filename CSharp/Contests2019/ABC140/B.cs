using System;
using System.Linq;

class B
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var n = int.Parse(Console.ReadLine());
		var a = read();
		var b = read();
		var c = read();

		var s = b.Sum();
		for (int i = 0; i < n - 1; i++) if (a[i] + 1 == a[i + 1]) s += c[a[i] - 1];
		Console.WriteLine(s);
	}
}
