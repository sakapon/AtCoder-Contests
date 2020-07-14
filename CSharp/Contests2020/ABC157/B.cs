using System;
using System.Linq;

class B
{
	static void Main()
	{
		var a = new int[3].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();
		var b = new int[int.Parse(Console.ReadLine())].Select(_ => int.Parse(Console.ReadLine())).ToHashSet();

		var r3 = new[] { 0, 1, 2 };
		Console.WriteLine(a.Any(ai => ai.All(b.Contains)) || r3.Any(j => a.All(ai => b.Contains(ai[j]))) || r3.All(i => b.Contains(a[i][i])) || r3.All(i => b.Contains(a[i][2 - i])) ? "Yes" : "No");
	}
}
