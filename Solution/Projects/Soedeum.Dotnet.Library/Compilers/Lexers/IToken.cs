using System;
using System.Collections.Generic;
using Soedeum.Dotnet.Library.Text;

namespace Soedeum.Dotnet.Library.Compilers.Lexers
{
    public interface IToken<TType>
    {
        string Source { get; }

        TextLocation Location { get; }

        string Value { get; }

        TType Type { get; }

        bool IsOf(TType type);

        void VerifyIsOf(TType type);
    }
}