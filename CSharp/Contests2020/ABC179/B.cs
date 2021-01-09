using System;
using System.Linq;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var d = new int[n].Select(_ => Console.ReadLine()).Select(s => s[0] == s[2]).ToArray();
		Console.WriteLine(Enumerable.Range(0, n - 2).Any(i => d[i] && d[i + 1] && d[i + 2]) ? "Yes" : "No");
	}
}
