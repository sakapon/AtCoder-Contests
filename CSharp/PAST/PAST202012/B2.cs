using System;
using System.Linq;

class B2
{
	static void Main()
	{
		var h = "".ToHashSet();
		Console.ReadLine();
		Console.WriteLine(string.Join("", Console.ReadLine().Reverse().Where(h.Add).Reverse()));
	}
}
