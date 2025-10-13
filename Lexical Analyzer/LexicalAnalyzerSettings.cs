using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.LexicalAnalyzer
{
    public class LexicalAnalyzerSettings
    {
        /// <summary>
        /// Strings that the lexical analyzer will recognize as keywords
        /// </summary>
        public List<string> Keywords = new List<string>();
    }
}
