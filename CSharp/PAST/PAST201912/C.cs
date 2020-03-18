using System;
using System.Linq;

class C
{
	static void Main()
	{
		Console.WriteLine(Console.ReadLine().Split().Select(int.Parse).OrderBy(x => -x).ElementAt(2));
	}
}
