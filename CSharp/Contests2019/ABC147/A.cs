using System;
using System.Linq;

class A
{
	static void Main() => Console.WriteLine(Console.ReadLine().Split().Sum(int.Parse) > 21 ? "bust" : "win");
}
