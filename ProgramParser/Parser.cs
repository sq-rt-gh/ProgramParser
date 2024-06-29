using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace ProgramParser
{
    class Parser
    {
        public string? Result;
        public int ErrorPosition { get; private set; } = 0;
        public int ErrorLenght { get; private set; } = 1;
        public bool HasError { get; private set; } = true;

        Lexer lexer;
        List<Lexeme> Lexemes;
        Lexeme CurrentLexeme { get => Lexemes[LexemeId]; }
        int LexemeId;
        string VarName;
        int RightValueStartIndex;

        public Parser(string code)
        {
            lexer = new Lexer(code);
        }

        void NextLexeme()
        {
            LexemeId++;
            ErrorPosition = CurrentLexeme.Position;
            ErrorLenght = CurrentLexeme.Lenght;
        }

        string OkResult()
        {
            HasError = false;
            return $"Ошибок не найдено! \r\n {VarName} = {Calculate()}";
        }

        public void Parse()
        {
            try
            {
                Lexemes = lexer.Parse();
            }
            catch (Exception e)
            {
                ErrorPosition = lexer.ErrorPosition;
                ErrorLenght = lexer.ErrorLenght;
                Result = e.Message;
                return;
            }

            LexemeId = 0;
            try
            {
                Result = ParseBegin() ??
                    ParseMnozh() ??
                    ParseZveno() ??
                    ParseOper() ??
                    OkResult();
            }
            catch (Exception e)
            {
                Result = "Ошибка: " + e.Message;
            }
        }

        string? ParseBegin()
        {
            if (CurrentLexeme.Type == LexemeType.Keyword && CurrentLexeme.Value == "Начало")
            {
                NextLexeme();
                return null;
            }
            return "Программа должна начинаться со слова 'Начало'.";
        }

        string? ParseMnozh()
        {
            string? s = ParseFirstMnozh() ?? ParseSecondMnozh() ?? ParseThirdMnozh();
            if (s == null)
                return "Ожидается ключевое слово 'Первое', 'Второе' или 'Третье'.";

            while (s == "ok")
            {
                s = ParseFirstMnozh() ?? ParseSecondMnozh() ?? ParseThirdMnozh();
            }

            return s;
        }

        string? ParseFirstMnozh()
        {
            if (CurrentLexeme.Type == LexemeType.Keyword && CurrentLexeme.Value == "Первое")
            {
                NextLexeme();

                bool flag = false;
                while (CurrentLexeme.Type == LexemeType.Int) // int ... int
                {
                    flag = true;
                    NextLexeme();
                }
                if (flag)
                    return "ok";

                return "После слова 'Первое' должны идти целые числа россыпью.";
            }
            return null;
        }

        string? ParseSecondMnozh()
        {
            if (CurrentLexeme.Type == LexemeType.Keyword && CurrentLexeme.Value == "Второе")
            {
                NextLexeme();

                while (CurrentLexeme.Type == LexemeType.Float) // float, ... float
                {
                    NextLexeme();

                    if (CurrentLexeme.Type == LexemeType.Comma)
                    {
                        NextLexeme();
                    }
                    else if (CurrentLexeme.Type == LexemeType.Float)
                    {
                        return "Пропущена запятая на строке " + (CurrentLexeme.Line);
                    }
                    else
                    {
                        if (CurrentLexeme.Type == LexemeType.Keyword && CurrentLexeme.Value == "Конец второго")
                        {
                            NextLexeme();
                            return "ok";
                        }
                        return $"Ожидается ключевое слово 'Конец второго'.";
                    }
                }

                return "После слова 'Второе' должны идти вещественные числа через запятую.";
            }
            return null;
        }

        string? ParseThirdMnozh()
        {
            if (CurrentLexeme.Type == LexemeType.Keyword && CurrentLexeme.Value == "Третье")
            {
                NextLexeme();

                while (CurrentLexeme.Type == LexemeType.Var) // var, ... var
                {
                    NextLexeme();

                    if (CurrentLexeme.Type == LexemeType.Comma)
                    {
                        NextLexeme();
                    }
                    else if (CurrentLexeme.Type == LexemeType.Var)
                    {
                        return "Пропущена запятая на строке " + (CurrentLexeme.Line);
                    }
                    else
                    {
                        return "ok";
                    }
                }

                return "После слова 'Третье' должны идти переменные через запятую.";
            }
            return null;
        }

        string? ParseZveno()
        {
            if (CurrentLexeme.Type == LexemeType.Keyword && CurrentLexeme.Value == "Сочетаемое")
            {
                NextLexeme();

                bool flag = false;
                while (CurrentLexeme.Type == LexemeType.Int) // int ... int
                {
                    NextLexeme();

                    if (CurrentLexeme.Type == LexemeType.Colon) // парсим метку (int + ':')
                    {
                        if (flag)
                        {
                            NextLexeme();
                            break;
                        }
                        return $"Пропущено число перед меткой (строка {CurrentLexeme.Line}).";
                    }

                    flag = true;
                }
                if (flag)
                    return null;

                return "После слова 'Сочетаемое' должны идти целые числа россыпью.";
            }
            return $"Ожидается ключевое слово 'Сочетаемое'.";
        }

        string? ParseOper()
        {
            //метка парсится на предыдущем шаге

            if (CurrentLexeme.Type != LexemeType.Var)
                return $"Недопустимый синтаксис на строке {CurrentLexeme.Line}. \r\nОжидается переменная.";
            VarName = CurrentLexeme.Value;
            NextLexeme();
            if (CurrentLexeme.Type != LexemeType.Assign)
                return $"После переменной должен идти знак '='.";
            NextLexeme();

            RightValueStartIndex = LexemeId;
            string? result = ParseRightValue();
            if (result == "end") 
                return null; // no errors

            if (result == null && CurrentLexeme.Type != LexemeType.EOF) // not the end of file
            {
                if (CurrentLexeme.Type == LexemeType.Int || CurrentLexeme.Type == LexemeType.Var)
                {
                    return $"Два выражения не могут стоять подряд (строка {CurrentLexeme.Line})";
                }
                else
                {
                    return $"Недопустимый синтаксис на строке {CurrentLexeme.Line}. \r\nОжидается операция ('+', '-', '*', '/', '&', '|').";
                }
            }

            return result;
        }

        string? ParseRightValue(int nest = 0)
        {
            if (nest == 4)
                return "Глубина вложенности скобок не должна превышать 3.";

            if (CurrentLexeme.Type == LexemeType.Minus)
            {
                CurrentLexeme.Type = LexemeType.UnaryMinus;
                NextLexeme();
            }

            string? error = ParseBlock1(nest);

            while (error == null && (CurrentLexeme.Type == LexemeType.Plus || CurrentLexeme.Type == LexemeType.Minus))
            {
                NextLexeme();
                error = ParseBlock1(nest);
            }

            return error;
        }

        string? ParseBlock1(int nest)
        {
            string? error = ParseBlock2(nest);

            while (error == null && (CurrentLexeme.Type == LexemeType.Multiply || CurrentLexeme.Type == LexemeType.Divide))
            {
                NextLexeme();
                error = ParseBlock2(nest);
            }

            return error;
        }

        string? ParseBlock2(int nest)
        {
            string? error = ParseBlock3(nest);

            while (error == null && (CurrentLexeme.Type == LexemeType.AND || CurrentLexeme.Type == LexemeType.OR))
            {
                NextLexeme();
                error = ParseBlock3(nest);
            }

            return error;
        }

        string? ParseBlock3(int nest)
        {
            if (CurrentLexeme.Type == LexemeType.NOT)
            {
                NextLexeme();
            }

            return ParseBlock4(nest);
        }

        string? ParseBlock4(int nest)
        {
            if (CurrentLexeme.Type == LexemeType.Int || CurrentLexeme.Type == LexemeType.Var)
            {
                NextLexeme();
            }
            else if (CurrentLexeme.Type == LexemeType.BracketOpen)
            {
                NextLexeme();

                string? error = ParseRightValue(nest + 1);
                if (error == "end")
                    return $"Ожидается закрывающая скобка (строка {CurrentLexeme.Line}).";
                if (error != null)
                    return error;

                if (CurrentLexeme.Type != LexemeType.BracketClose)
                    return $"Ожидается закрывающая скобка (строка {CurrentLexeme.Line}).";

                NextLexeme();
            }
            else
            {
                return $"Не допустимый синтаксис на строке {CurrentLexeme.Line}. \r\nОжидается число, переменная или выражение в скобках.";
            }

            if (CurrentLexeme.Type == LexemeType.EOF) // конец файла
                return "end";

            return null;
        }

        string Calculate()
        {
            // Creating postfix form

            Dictionary<LexemeType, int> priority = new Dictionary<LexemeType, int>
            {
                [LexemeType.BracketOpen] = 0,
                [LexemeType.Plus] = 1,
                [LexemeType.Minus] = 1,
                [LexemeType.Multiply] = 2,
                [LexemeType.Divide] = 2,
                [LexemeType.OR] = 3,
                [LexemeType.AND] = 4,
                [LexemeType.NOT] = 5,
                [LexemeType.UnaryMinus] = 5
            };
            List<Lexeme> postfix = new List<Lexeme>();
            Stack<Lexeme> stack = new Stack<Lexeme>();

            for (int i = RightValueStartIndex; Lexemes[i].Type != LexemeType.EOF; i++)
            {
                if (Lexemes[i].Type == LexemeType.Int || Lexemes[i].Type == LexemeType.Var)
                {
                    postfix.Add(Lexemes[i]);
                }
                else if (Lexemes[i].Type == LexemeType.BracketOpen)
                {
                    stack.Push(Lexemes[i]);
                }
                else if (Lexemes[i].Type == LexemeType.BracketClose)
                {
                    while (stack.Peek().Type != LexemeType.BracketOpen)
                    {
                        postfix.Add(stack.Pop());
                    }
                    stack.Pop();
                }
                else
                {
                    while (stack.Count > 0 && priority[stack.Peek().Type] >= priority[Lexemes[i].Type])
                    {
                        postfix.Add(stack.Pop());
                    }
                    stack.Push(Lexemes[i]);
                }

            }
            while (stack.Count > 0)
            {
                postfix.Add(stack.Pop());
            }

            // Calculating postfix expression
            stack.Clear();

            for (int i = 0; i < postfix.Count; i++)
            {
                if (postfix[i].Type == LexemeType.Int || postfix[i].Type == LexemeType.Var)
                {
                    stack.Push(postfix[i]);
                }
                else
                {
                    if (postfix[i].Type == LexemeType.NOT || postfix[i].Type == LexemeType.UnaryMinus)
                    {
                        stack.Push(Operation(postfix[i], stack.Pop()));
                    }
                    else
                    {
                        var a = stack.Pop();
                        var b = stack.Pop();
                        stack.Push(Operation(postfix[i], b, a));
                    }
                }
            }

            return stack.Peek().Value; /* + "\r\n" + string.Join(' ', postfix.Select((l)=> l.Value));*/ 
        }

        Lexeme Operation(Lexeme op, Lexeme a)
        {
            if (a.Type == LexemeType.Var)
            {
                return new Lexeme($"{op.Value}{a.Value}", LexemeType.Var, 0);
            }
            switch (op.Type)
            {
                case LexemeType.NOT:
                    return new Lexeme((~int.Parse(a.Value)).ToString(), LexemeType.Int, 0);

                case LexemeType.UnaryMinus:
                    return new Lexeme((-int.Parse(a.Value)).ToString(), LexemeType.Int, 0);
            }

            throw new ArgumentException("Неизвесная операция");
        }

        Lexeme Operation(Lexeme op, Lexeme a, Lexeme b)
        {
            if (a.Type == LexemeType.Var || b?.Type == LexemeType.Var)
            {
                return new Lexeme($"{a.Value}{op.Value}{b.Value}", LexemeType.Var);
            }

            switch (op.Type)
            {
                case LexemeType.Plus:
                    return new Lexeme((int.Parse(a.Value) + int.Parse(b.Value)).ToString(), LexemeType.Int);

                case LexemeType.Minus:
                    return new Lexeme((int.Parse(a.Value) - int.Parse(b.Value)).ToString(), LexemeType.Int);

                case LexemeType.Multiply:
                    return new Lexeme((int.Parse(a.Value) * int.Parse(b.Value)).ToString(), LexemeType.Int);

                case LexemeType.Divide:
                    var bVal = int.Parse(b.Value);
                    if (bVal == 0)
                        throw new DivideByZeroException("Деление на ноль.");
                    return new Lexeme((int.Parse(a.Value) / bVal).ToString(), LexemeType.Int);

                case LexemeType.AND:
                    return new Lexeme((int.Parse(a.Value) & int.Parse(b.Value)).ToString(), LexemeType.Int);

                case LexemeType.OR:
                    return new Lexeme((int.Parse(a.Value) | int.Parse(b.Value)).ToString(), LexemeType.Int);
            }

            throw new ArgumentException("Неизвесная операция");
        }
    }
}
