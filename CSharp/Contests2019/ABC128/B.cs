using System;
using System.Linq;

class B
{
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select((_, i) => $"{i + 1} {Console.ReadLine()}".Split()).OrderBy(x => x[1]).ThenBy(x => -int.Parse(x[2])).Select(x => x[0])));
}
