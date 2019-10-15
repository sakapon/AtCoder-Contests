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
		Console.WriteLine(b.Sum() + Enumerable.Range(0, n - 1).Where(i => a[i] + 1 == a[i + 1]).Sum(i => c[a[i] - 1]));
	}
}
