using System;

class B
{
	static void Main()
	{
		Func<string, bool> month = x => x.CompareTo("00") > 0 && x.CompareTo("12") <= 0;
		var s = Console.ReadLine();
		var m1 = month(s.Substring(0, 2));
		var m2 = month(s.Substring(2));
		Console.WriteLine(m1 ? (m2 ? "AMBIGUOUS" : "MMYY") : (m2 ? "YYMM" : "NA"));
	}
}
