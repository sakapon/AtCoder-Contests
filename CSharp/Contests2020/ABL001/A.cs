using System;
using System.Linq;

class A
{
	static void Main() => Console.WriteLine(string.Join("", Enumerable.Repeat("ACL", int.Parse(Console.ReadLine()))));
}
