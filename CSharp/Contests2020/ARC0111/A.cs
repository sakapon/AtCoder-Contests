using System;
using System.Linq;

class A
{
	static void Main()
	{
		var ms = new int[int.Parse(Console.ReadLine())].Select(_ => Console.ReadLine().Split()).ToArray();
		var x = Console.ReadLine();

		Console.WriteLine(ms.SkipWhile(m => m[0] != x).Skip(1).Sum(m => int.Parse(m[1])));
	}
}
