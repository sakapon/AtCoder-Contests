using System;
using System.Linq;
using System.Text.RegularExpressions;

class B
{
	static void Main()
	{
		var regex = new Regex("^.*okyo.*ech.*$");
		foreach (var s in new int[int.Parse(Console.ReadLine())].Select(_ => Console.ReadLine())) Console.WriteLine(regex.IsMatch(s) ? "Yes" : "No");
	}
}
