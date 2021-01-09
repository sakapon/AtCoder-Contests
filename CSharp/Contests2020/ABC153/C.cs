using System;
using System.Linq;

class C
{
	static long[] Read() => Console.ReadLine().Split().Select(long.Parse).ToArray();
	static void Main()
	{
		var z = Read();
		Console.WriteLine(Read().OrderBy(x => -x).Skip((int)z[1]).Sum());
	}
}
