using System;
using System.Collections.Generic;
using System.Text;

namespace Blocknot
{
    public class Analyzer
    {
        private static readonly HashSet<string> keywords = new HashSet<string> { "enum", "case" };
        private static readonly HashSet<char> separators = new HashSet<char> { ' ', '\t' };
        private static readonly HashSet<char> validSymbols = new HashSet<char>(
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_".ToCharArray()
        );

        private enum State
        {
            None,
            InIdentifier,
            InStringLiteral
        }

        public static string Analyze(string input)
        {
            StringBuilder output = new StringBuilder();
            int i = 0;
            int length = input.Length;
            State state = State.None;
            StringBuilder currentToken = new StringBuilder();
            int tokenStart = 0;

            void AddToken(int code, string type, string token, int start, int end)
            {
                output.AppendLine($"{code}\t{type}\t\"{token}\"\t({start} - {end})");
            }

            while (i < length)
            {
                char ch = input[i];

                // Строка
                if (ch == '"')
                {
                    int stringStart = i;
                    i++;
                    StringBuilder str = new StringBuilder("\"");
                    bool closed = false;

                    while (i < length)
                    {
                        str.Append(input[i]);
                        if (input[i] == '"')
                        {
                            closed = true;
                            break;
                        }
                        i++;
                    }

                    if (closed)
                    {
                        i++;
                        AddToken(3, "строка", str.ToString(), stringStart, i - 1);
                        continue;
                    }
                    else
                    {
                        AddToken(8, "ERROR", str.ToString(), stringStart, i - 1);
                        break;
                    }
                }

                // Пробел или табуляция
                if (separators.Contains(ch))
                {
                    AddToken(4, "разделитель", ch.ToString(), i, i);
                    i++;
                    continue;
                }

                // Открывающаяся скобка
                if (ch == '{')
                {
                    AddToken(5, "открывающаяся скобка", ch.ToString(), i, i);
                    i++;
                    continue;
                }

                // Закрывающаяся скобка
                if (ch == '}')
                {
                    AddToken(6, "закрывающаяся скобка", ch.ToString(), i, i);
                    i++;
                    continue;
                }

                // Конец оператора
                if (ch == ';')
                {
                    AddToken(7, "конец оператора", ch.ToString(), i, i);
                    i++;
                    continue;
                }

                // Идентификаторы и ключевые слова
                if (char.IsLetter(ch))
                {
                    // Проверка на русские буквы
                    if ((ch >= 'а' && ch <= 'я') || (ch >= 'А' && ch <= 'Я'))
                    {
                        AddToken(8, "ERROR", ch.ToString(), i, i);
                        i++;
                        continue;
                    }

                    tokenStart = i;
                    currentToken.Clear();

                    while (i < length && (char.IsLetterOrDigit(input[i]) || input[i] == '_'))
                    {
                        // Проверка на русские буквы внутри идентификатора
                        if ((input[i] >= 'а' && input[i] <= 'я') || (input[i] >= 'А' && input[i] <= 'Я'))
                        {
                            AddToken(8, "ERROR", input[i].ToString(), i, i);
                            i++;
                            break;
                        }

                        currentToken.Append(input[i]);
                        i++;
                    }

                    if (currentToken.Length == 0)
                        continue;

                    string token = currentToken.ToString();

                    if (keywords.Contains(token))
                    {
                        int code = token == "enum" ? 1 : 2;
                        AddToken(code, "ключевое слово", token, tokenStart, i - 1);
                    }
                    else
                    {
                        AddToken(3, "идентификатор", token, tokenStart, i - 1);
                    }

                    continue;
                }

                // Присваивание
                if (ch == '=')
                {
                    AddToken(6, "оператор присваивания", "=", i, i);
                    i++;
                    continue;
                }

                // Недопустимые символы (не пробел и не латиница)
                if (!char.IsWhiteSpace(ch))
                {
                    AddToken(8, "ERROR", ch.ToString(), i, i);
                    i++;
                    continue;
                }

                i++;
            }

            return output.ToString();
        }
    }
}
