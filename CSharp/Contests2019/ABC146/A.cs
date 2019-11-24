using System;

class A
{
	static void Main() => Console.WriteLine(7 - Array.IndexOf(new[] { "SUN", "MON", "TUE", "WED", "THU", "FRI", "SAT" }, Console.ReadLine()));
}
