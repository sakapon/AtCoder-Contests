using System;
using System.Linq;

class A
{
	static void Main() => Console.WriteLine((char)('A' + Console.ReadLine().Split().Select((s, i) => (x: int.Parse(s), i)).OrderBy(v => v.x).ElementAt(1).i));
}
