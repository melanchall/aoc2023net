using System;

namespace Aoc2023Net.Utilities
{
    [AttributeUsage(AttributeTargets.Field)]
    internal sealed class TokenAttribute : Attribute
    {
        public TokenAttribute(string token) => Token = token;

        public string Token { get; }
    }
}
