using Compiler.LexicalAnalyzer.Objects;

namespace Compiler.LexicalAnalyzer.Test
{
    [TestClass]
    public sealed class LexicalAnalyzerTest
    {
        LexicalAnalyzer lexicalAnalyzer = new LexicalAnalyzer(new LexicalAnalyzerSettings()
        {
            Keywords = new List<string>()
            {
                "and",
                "break",
                "do",
                "else",
                "elseif",
                "end",
                "false",
                "for",
                "function",
                "if",
                "in",
                "local",
                "nil",
                "not",
                "or",
                "repeat",
                "return",
                "then",
                "true",
                "until",
                "while"
            }
        });

        [TestMethod]
        public void PrintTest()
        {
            LexTokenList lexTokenList = lexicalAnalyzer.Analyze("print\"Hello, world!\"");

            Assert.AreEqual(lexTokenList.Consume().Kind, LexKind.EOF);
            Assert.AreEqual(lexTokenList.Consume().Kind, LexKind.String);
            Assert.AreEqual(lexTokenList.Consume().Kind, LexKind.Identifier);
        }
    }
}
