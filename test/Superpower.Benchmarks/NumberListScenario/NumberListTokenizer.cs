﻿using System;
using System.Collections.Generic;
using Superpower.Model;
using Superpower.Parsers;

namespace Superpower.Benchmarks.NumberListScenario
{
    class NumberListTokenizer : Tokenizer<NumberListToken>
    {
        protected override IEnumerable<Result<NumberListToken>> Tokenize(TextSpan span)
        {
            var next = SkipWhiteSpace(span);
            if (!next.HasValue)
                yield break;

            do
            {
                var ch = next.Value;
                if (ch >= '0' && ch <= '9')
                {
                    var integer = Numerics.Integer(next.Location);
                    next = integer.Remainder.ConsumeChar();
                    yield return Result.Value(NumberListToken.Number, integer.Location, integer.Remainder);
                }
                else
                {
                    yield return Result.Empty<NumberListToken>(next.Location, new[] { "digit" });
                }

                next = SkipWhiteSpace(next.Location);
            } while (next.HasValue);
        }
    }
}
