using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ProgramParser
{
    enum LexemeType
    {
        Plus,
        Minus,
        UnaryMinus,
        Multiply,
        Divide,
        AND,
        OR,
        NOT,
        Assign,
        Comma,
        Colon,
        BracketOpen,
        BracketClose,
        Int,
        Float,
        Keyword,
        Var,
        EOF
    }

    class Lexeme
    {
        public string Value;
        public LexemeType Type;
        public int Line;
        public int Position;
        public int Lenght;

        public Lexeme(string value, LexemeType type, int line = 0, int position = 0, int lenght = 0)
        {
            Value = value;
            Type = type;
            Line = line;
            Position = position;
            Lenght = lenght;
        }
    }

    class StaticLexeme
    {
        public LexemeType Type { get; protected set; }
        public string Representation { get; protected set; }

        public StaticLexeme(string rep, LexemeType type)
        {
            Representation = rep;
            Type = type;
        }
    }

    class DynamicLexeme
    {
        public LexemeType Type { get; protected set; }
        public Regex Representation { get; protected set; }

        public DynamicLexeme(string rep, LexemeType type)
        {
            Representation = new Regex(@"\G" + rep, RegexOptions.Compiled);
            Type = type;
        }
    }

}
