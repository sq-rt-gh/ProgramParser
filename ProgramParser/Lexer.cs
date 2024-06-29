using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ProgramParser
{
    class Lexer
    {
        static readonly StaticLexeme[] StaticLexems =
            [
                new StaticLexeme("=", LexemeType.Assign),
                new StaticLexeme("+", LexemeType.Plus),
                new StaticLexeme("-", LexemeType.Minus),
                new StaticLexeme("*", LexemeType.Multiply),
                new StaticLexeme("/", LexemeType.Divide),
                new StaticLexeme("&", LexemeType.AND),
                new StaticLexeme("|", LexemeType.OR),
                new StaticLexeme("!", LexemeType.NOT),
                new StaticLexeme(",", LexemeType.Comma),
                new StaticLexeme(":", LexemeType.Colon),
                new StaticLexeme("[", LexemeType.BracketOpen),
                new StaticLexeme("]", LexemeType.BracketClose),
                new StaticLexeme("Начало", LexemeType.Keyword),
                new StaticLexeme("Первое", LexemeType.Keyword),
                new StaticLexeme("Второе", LexemeType.Keyword),
                new StaticLexeme("Конец второго", LexemeType.Keyword),
                new StaticLexeme("Третье", LexemeType.Keyword),
                new StaticLexeme("Сочетаемое", LexemeType.Keyword),
            ];
        static readonly DynamicLexeme[] DynamicLexems = 
            [
                new DynamicLexeme(@"([0-9]+)(?:\.[0-9]+)", LexemeType.Float),
                new DynamicLexeme(@"([0-9]+)", LexemeType.Int),
                new DynamicLexeme("[a-zA-Zа-яА-Я][a-zA-Zа-яА-Я0-9]*", LexemeType.Var)
            ];
        static readonly char[] SpaceChars = [' ', '\n', '\r', '\t'];

        public int ErrorPosition { get; private set; } = 0;
        public int ErrorLenght { get; private set; } = 0;

        readonly string Code;
        int Position;
        int Line = 1;

        public Lexer(string code)
        {
            Code = code;
        }

        public List<Lexeme> Parse()
        {
            var lexems = new List<Lexeme>();

            while (Position < Code.Length)
            {
                SkipSpaces();
                if (Position >= Code.Length) 
                    break;

                var lex = (ProcessStatic() ?? ProcessDynamic()) ?? throw new Exception($"Неизвестное слово: '{ParseUnknown()}'");
                lexems.Add(lex);
            }
            lexems.Add(new Lexeme("End of file", LexemeType.EOF, Line, Position-1, 1));
            return lexems;
        }

        private void SkipSpaces()
        {
            while (Position < Code.Length && SpaceChars.Contains(Code[Position]))
            {
                if (Code[Position] == '\n') Line++;
                Position++;
            }
        }

        private Lexeme? ProcessStatic()
        {
            foreach (var sl in StaticLexems)
            {
                var len = sl.Representation.Length;

                if (Position + len > Code.Length || Code.Substring(Position, len) != sl.Representation)
                    continue;

                if (sl.Type == LexemeType.Keyword)
                {
                    char prev = ' ', next = ' ';

                    if (Position != 0) prev = Code[Position - 1];
                    if (Position + len < Code.Length) next = Code[Position + len];

                    if (!(SpaceChars.Contains(prev) && SpaceChars.Contains(next)))
                        continue;
                }

                Position += len;
                return new Lexeme(sl.Representation, sl.Type, Line, Position-len, len);
            }

            return null;
        }

        private Lexeme? ProcessDynamic()
        {
            foreach (var dl in DynamicLexems)
            {
                var match = dl.Representation.Match(Code, Position);

                if (match.Success)
                {
                    if (dl.Type == LexemeType.Var && Position != 0 && char.IsNumber(Code[Position-1])) continue;

                    Position += match.Length;
                    return new Lexeme(match.Value, dl.Type, Line, Position-match.Length, match.Length);
                }
            }

            return null;
        }

        string ParseUnknown()
        {
            int pos = Position;
            while (Position < Code.Length && !SpaceChars.Contains(Code[Position]))
            {
                Position++;
            }
            ErrorPosition = pos;
            ErrorLenght = Position - pos + 1;
            return Code[pos..Position];
        }

    }
}
