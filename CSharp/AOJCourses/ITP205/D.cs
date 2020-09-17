using System;
using System.Linq;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		new Perm<int>().Find(Enumerable.Range(1, n).ToArray(), n, p => Console.WriteLine(string.Join(" ", p)));
	}
}
