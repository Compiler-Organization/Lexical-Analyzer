using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexicalAnalyzer.Objects
{
    public class LexTokenList : List<LexToken>
    {
        public LexToken Consume()
        {
            LexToken lexToken = this.Last();
            this.Remove(lexToken);

            return lexToken;
        }
    }
}
