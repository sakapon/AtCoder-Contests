using System;

class C2
{
	static void Main()
	{
		Console.ReadLine();
		var s = Console.ReadLine();

		for (int i = s.Length - 1; i > 0; i--)
			if (i < s.Length && s[i - 1] == s[i])
			{
				var c = s[i].ToString();
				s = s.Replace(c + c, c);
			}
		Console.WriteLine(s.Length);
	}
}
