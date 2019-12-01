using System;
using System.Linq;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var x = Enumerable.Range(1, n).FirstOrDefault(i => (int)(1.08 * i) == n);
		Console.WriteLine(x > 0 ? $"{x}" : ":(");
	}
}
