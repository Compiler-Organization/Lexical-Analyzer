using System.Text;
using LexicalAnalyzer;
using LexicalAnalyzer.Objects;

namespace LexicalAnalyzer
{
    public class LexicalAnalyzer
    {
        LexicalAnalyzerSettings lexicalAnalyzerSettings { get; set; }

        public LexicalAnalyzer(LexicalAnalyzerSettings settings)
        {
            this.lexicalAnalyzerSettings = settings;
        }

        LexKind Identify(string Value)
        {
            if (ulong.TryParse(Value, out _))
                return LexKind.Number;

            if (Value == "false"
                || Value == "true")
                return LexKind.Boolean;

            if (lexicalAnalyzerSettings.Keywords.Contains(Value))
                return LexKind.Keyword;

            return LexKind.Identifier;
        }

        public LexTokenList Analyze(string input)
        {
            LexTokenList LexTokens = new LexTokenList();
            StringBuilder sb = new StringBuilder();
            int Line = 1;

            for (int i = 0; i < input.Length; i++)
            {
                LexKind kind = LexKind.Terminal;
                string value = "";
                switch (input[i])
                {
                    case '(': kind = LexKind.ParentheseOpen; break;
                    case ')': kind = LexKind.ParentheseClose; break;

                    case '[': kind = LexKind.BracketOpen; break;
                    case ']': kind = LexKind.BracketClose; break;

                    case '{': kind = LexKind.BraceOpen; break;
                    case '}': kind = LexKind.BraceClose; break;

                    case '<':
                        {
                            if (input[i + 1] == '=')
                            {
                                kind = LexKind.SmallerOrEqual;
                                i++;
                            }
                            else
                            {
                                kind = LexKind.ChevronOpen;
                            }
                            break;
                        }
                    case '>':
                        {
                            if (input[i + 1] == '=')
                            {
                                kind = LexKind.BiggerOrEqual;
                                i++;
                            }
                            else
                            {
                                kind = LexKind.ChevronClose;
                            }
                            break;
                        }


                    case ';': kind = LexKind.Semicolon; break;

                    case ',': kind = LexKind.Comma; break;

                    case '.':
                        {
                            if (input[i + 1] == '.')
                            {
                                kind = LexKind.Concat;
                            }
                            break;
                        }

                    case '"':
                        {
                            i++;
                            while (input[i] != '"')
                            {
                                if (input[i] == '\\' && input[i + 1] == '"')
                                {
                                    i++;
                                }
                                sb.Append(input[i]);
                                i++;
                            }
                            value = sb.ToString();
                            kind = LexKind.String;
                            sb.Clear();

                            break;
                        }

                    case '\'':
                        {
                            i++;
                            while (input[i] != '\'')
                            {
                                if (input[i] == '\\' && input[i + 1] == '\'')
                                {
                                    i++;
                                }
                                sb.Append(input[i]);
                                i++;
                            }
                            value = sb.ToString();
                            kind = LexKind.String;
                            sb.Clear();

                            break;
                        }

                    case '=':
                        {
                            if (input[i + 1] == '=')
                            {
                                kind = LexKind.EqualTo;
                                i++;
                            }
                            else
                            {
                                kind = LexKind.Equals;
                            }
                            break;
                        }

                    case '~':
                        {
                            if (input[i + 1] == '=')
                            {
                                kind = LexKind.NotEqualTo;
                                i++;
                            }
                            break;
                        }


                    case '\n':
                        Line++;
                        break;

                    case '?':
                        {
                            kind = LexKind.Question;
                            break;
                        }

                    case '!':
                        {
                            kind = LexKind.Exclamation;
                            break;
                        }

                    case '+':
                        {
                            kind = LexKind.Add;
                            break;
                        }
                    case '-':
                        {
                            if (input.Length > i + 1 && input[i + 1] == '-')
                            {
                                i += 2;
                                for (; input[i] != '\n' && i < input.Length; i++)
                                {
                                    sb.Append(input[i]);
                                }
                                value = sb.ToString();
                                kind = LexKind.Comment;
                                sb.Clear();
                                Line++;
                            }
                            else
                            {
                                kind = LexKind.Sub;
                            }
                            break;
                        }
                    case '*':
                        {
                            kind = LexKind.Mul;
                            break;
                        }
                    case '/':
                        {

                            kind = LexKind.Div;
                            break;
                        }
                    case '%':
                        {
                            kind = LexKind.Mod;
                            break;
                        }
                    case '^':
                        {

                            kind = LexKind.Exp;
                            break;
                        }

                    // Discard
                    case ' ':
                    case '\r':
                    case '\t':
                        break;

                    default:
                        {
                            if (Char.IsLetterOrDigit(input[i])
                                || input[i] == '.'
                                || input[i] == '_'
                                || input[i] == ':')
                            {
                                while (input.Length > i && (Char.IsLetterOrDigit(input[i]) || input[i] == '.' || input[i] == ':' || input[i] == '_'))
                                {
                                    sb.Append(input[i++]);
                                }
                                i--;
                            }
                            value = sb.ToString();
                            kind = Identify(value);

                            sb.Clear();
                            break;
                        }
                }


                if (kind != LexKind.Terminal)
                {
                    LexTokens.Add(new LexToken()
                    {
                        Kind = kind,
                        Value = value,
                        Line = Line
                    });
                }
            }

            LexTokens.Add(new LexToken()
            {
                Kind = LexKind.EOF
            });

            return LexTokens;
        }
    }
}
