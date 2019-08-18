using System;
using System.Linq;

class A
{
	static void Main()
	{
		var s = Console.ReadLine();

		var r = 1;
		var c = s[0];
		var is2 = false;
		for (var i = 1; i < s.Length; i++)
		{
			if (is2 || c != s[i])
			{
				is2 = false;
				c = s[i];
			}
			else
			{
				is2 = true;
				i++;
			}
			r++;
		}
		if (is2 && s[s.Length - 2] == s[s.Length - 1]) r--;
		Console.WriteLine(r);
	}
}
