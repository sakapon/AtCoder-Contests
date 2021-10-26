using System;
using System.Linq;

class B2
{
	static void Main() => Console.WriteLine($"A{"BRGH".Except(new int[3].Select(_ => Console.ReadLine()[1])).First()}C");
}
