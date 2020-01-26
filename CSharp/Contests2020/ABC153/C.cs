using System;
using System.Linq;

class C
{
	static void Main()
	{
		Func<long[]> read = () => Console.ReadLine().Split().Select(long.Parse).ToArray();
		var h = read();
		Console.WriteLine(read().OrderBy(x => -x).Skip((int)h[1]).Sum());
	}
}
