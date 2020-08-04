using System;
using System.Linq;

class F
{
	static void Main()
	{
		Console.ReadLine();
		var s = Console.ReadLine();
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var ps = new int[h[0]].Select(_ => read()).ToArray();

		Console.WriteLine(string.Join(" ", a));
	}
}
