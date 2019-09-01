using System;
using System.Linq;

class E
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var ps = new int[h[0]].Select(_ => read()).ToArray();

		Console.WriteLine(string.Join(" ", h));
	}
}
