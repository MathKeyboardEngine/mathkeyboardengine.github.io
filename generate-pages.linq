<Query Kind="Program" />

readonly UTF8Encoding _encoding = new UTF8Encoding(true);
readonly DirectoryInfo _websiteFolder = new System.IO.DirectoryInfo(@"C:\_\Repos\mathkeyboardengine.github.io");
readonly string version = "v1.1.0";

void Main()
{	
	var examples = new System.IO.DirectoryInfo(@"C:\_\Repos\MathKeyboardEngine\examples").GetFiles().Where(f => !f.Name.Contains("-easy"));
	foreach (var example in examples)
	{
		CreateWebPage(example, text => text.Replace(
			@"import * as mke from '../../dist/MathKeyboardEngine.es2020-esm.js';",
			$@"import * as mke from 'https://cdn.jsdelivr.net/npm/mathkeyboardengine@{version}/dist/MathKeyboardEngine.es2017-esm.min.js';"));
	}
}

public void CreateWebPage(FileInfo exampleFile, params Func<string, string>[] replacementFuncs)
{
	string text = File.ReadAllText(exampleFile.FullName, _encoding);
	foreach (var replacementFunc in replacementFuncs)
	{
		text = replacementFunc.Invoke(text);
	}
	string targetFilePath = Path.Combine(@"C:\_\Repos\mathkeyboardengine.github.io\live-examples", exampleFile.Name);
	File.WriteAllText(targetFilePath, text.NormalizeNewLines(), _encoding);
}

public static class StringHelper 
{
	public static string NormalizeNewLines(this string text) => Regex.Replace(text, @"\r\n?|\n", "\r\n");
}
