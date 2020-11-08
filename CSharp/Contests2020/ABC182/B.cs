using System;
using System.Linq;

class B
{
	static void Main()
	{
		Console.ReadLine();
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		Console.WriteLine(Enumerable.Range(2, 999).OrderBy(k => -a.Count(x => x % k == 0)).First());
	}
}
