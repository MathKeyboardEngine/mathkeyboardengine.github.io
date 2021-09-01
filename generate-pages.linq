<Query Kind="Program" />

readonly UTF8Encoding _encoding = new UTF8Encoding(true);
readonly DirectoryInfo _websiteFolder = new System.IO.DirectoryInfo(@"C:\_\Repos\mathkeyboardengine.github.io");

void Main()
{	
	var examples = new System.IO.DirectoryInfo(@"C:\_\Repos\MathKeyboardEngine\examples").GetFiles();
	foreach (var example in examples)
	{
		CreateWebPage(example, text => text.Replace(
			@"import * as mke from '../../dist/MathKeyboardEngine.es2020-esm.js';",
			@"import * as mke from 'https://cdn.jsdelivr.net/npm/mathkeyboardengine@v0.1.0-beta.6/dist/MathKeyboardEngine.es2017-esm.min.js';"));
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
