using System;

class D
{
	static void Main() => Console.WriteLine(Count(long.Parse(Console.ReadLine())));
	static long Count(long h) => h == 1 ? 1 : 2 * Count(h / 2) + 1;
}
