using System;

class A
{
	static void Main() => Console.WriteLine("0334114422253"[(int)(long.Parse(Console.ReadLine()) % 26 / 2)]);
}
