$source = @"
using System;
using System.IO;
using System.Linq;

public static class Program
{
	public static int Main(string[] args)
	{
		var dirs = Directory.EnumerateDirectories(".").Select(Path.GetFileName).ToArray();

		var dir_template = dirs.FirstOrDefault(x => x.EndsWith("000"));
		if (dir_template == null) { Console.WriteLine("The template does not exist."); return 101; }

		var dir_new = "";
		if (args.Length >= 1)
		{
			dir_new = args[0];
			if (dirs.Contains(dir_new)) { Console.WriteLine("The specified directory already exists."); return 102; }
		}
		else
		{
			var prefix = dir_template.Replace("000", "");
			dirs = dirs.Where(x => x.TakeWhile(char.IsLetter).SequenceEqual(prefix)).ToArray();
			var last = dirs.Max(x => int.Parse(x.Replace(prefix, "")));
			dir_new = prefix + (last + 1).ToString("D3");
		}

		Directory.CreateDirectory(dir_new);
		foreach (var path in Directory.EnumerateFiles(dir_template))
		{
			var fileName = Path.GetFileName(path);
			if (fileName.EndsWith(".csproj")) fileName = dir_new + ".csproj";
			File.Copy(path, Path.Combine(dir_new, fileName));
		}
		Console.WriteLine("{0} has been created.", dir_new);

		return 0;
	}
}
"@

Add-Type -TypeDefinition $source -Language CSharp
[Program]::Main($Args)
