using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class A
{
	enum Location
	{
		Up, Down, Left, Right
	}

	struct Drawing
	{
		public Location Location;
		public int Begin, End;
	}

	static void Main()
	{
		var dirPath = @"signboard_t1\pieces";
		if (!Directory.Exists(dirPath)) return;

		var pieces = Enumerable.Range(1, 64)
			.Select(i => Path.Combine(dirPath, $"{i}.txt"))
			.Select(p => File.ReadAllLines(p))
			.ToArray();
		var drawings = pieces.Select(GetDrawings).ToArray();

		// 左上角
		var lus = drawings.Where(ds => ds.All(d => d.Location != Location.Up && d.Location != Location.Left)).ToArray();
	}

	static Drawing[] GetDrawings(string[] lines)
	{
		var r = new List<Drawing>();
		r.AddRange(GetDrawings(lines[0].ToCharArray(), Location.Up));
		r.AddRange(GetDrawings(lines[31].ToCharArray(), Location.Down));
		r.AddRange(GetDrawings(Enumerable.Range(0, 32).Select(i => lines[i][0]).ToArray(), Location.Left));
		r.AddRange(GetDrawings(Enumerable.Range(0, 32).Select(i => lines[i][31]).ToArray(), Location.Right));
		return r.ToArray();
	}

	static IEnumerable<Drawing> GetDrawings(char[] cs, Location l)
	{
		var begins = Enumerable.Range(0, 32).Where(i => cs[i] == '#' && (i == 0 || cs[i - 1] == '.')).ToArray();
		var ends = Enumerable.Range(0, 32).Where(i => cs[i] == '#' && (i == 31 || cs[i + 1] == '.')).ToArray();
		return Enumerable.Range(0, begins.Length).Select(i => new Drawing
		{
			Location = l,
			Begin = begins[i],
			End = ends[i],
		});
	}
}
