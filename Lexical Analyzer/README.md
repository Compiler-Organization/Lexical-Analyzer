# Lexical Analyzer

This project is a lexical analyzer implemented in C#. It reads a source code file, tokenizes its content, and outputs the tokens to a specified output file.

## Usage

This is an example of how to use the dependency in your own project:
```cs
using LexicalAnalyzer;

class Program
{
	static void Main(string[] args)
	{
		LexicalAnalyzerSettings settings = new LexicalAnalyzerSettings
		{
			Keywords = new List<string> { "if", "else", "while", "return" }
		};
		LexicalAnalyzer lexicalAnalyzer = new LexicalAnalyzer(settings);

		LexTokenList lexTokenList = lexicalAnalyzer.Analyze("print\"Hello, world!\"");

		lexTokenList.ForEach(token => Console.WriteLine(token.Kind));

		// Identifier
		// String
		// Identifier
		// EOF
	}
}
```