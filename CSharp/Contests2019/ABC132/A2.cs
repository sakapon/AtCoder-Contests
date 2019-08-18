using System;
using System.Linq;

class A2
{
	static void Main()
	{
		var a = Console.ReadLine().OrderBy(c => c).ToArray();
		Console.WriteLine(a[0] == a[1] && a[2] == a[3] && a[0] != a[2] ? "Yes" : "No");
	}
}
