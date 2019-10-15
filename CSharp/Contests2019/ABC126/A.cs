using System;

class A
{
	static void Main()
	{
		var k = Array.ConvertAll(Console.ReadLine().Split(), int.Parse)[1] - 1;
		var s = Console.ReadLine().ToCharArray();
		s[k] = char.ToLower(s[k]);
		Console.WriteLine(new string(s));
	}
}
