using System;
using System.Linq;

class A2
{
	static void Main() => Console.WriteLine(new[] { "", "Gold", "Silver", "Alloy" }[Console.ReadLine().Split().Select((s, i) => Math.Sign(int.Parse(s)) << i).Sum()]);
}
