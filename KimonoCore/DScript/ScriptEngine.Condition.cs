﻿/*
Copyright (c) 2014 Darren Horrocks

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace DScript
{
	public partial class ScriptEngine
	{
		private ScriptVarLink Condition(ref bool execute)
		{
			ScriptVarLink a = Shift(ref execute);

			while (_currentLexer.TokenType == ScriptLex.LexTypes.Equal ||
				_currentLexer.TokenType == ScriptLex.LexTypes.NEqual ||
				_currentLexer.TokenType == ScriptLex.LexTypes.TypeEqual ||
				_currentLexer.TokenType == ScriptLex.LexTypes.NTypeEqual ||
				_currentLexer.TokenType == ScriptLex.LexTypes.LEqual ||
				_currentLexer.TokenType == ScriptLex.LexTypes.GEqual ||
				_currentLexer.TokenType == (ScriptLex.LexTypes)'>' ||
				_currentLexer.TokenType == (ScriptLex.LexTypes)'<'
				)
			{
				ScriptLex.LexTypes op = _currentLexer.TokenType;
				_currentLexer.Match(op);
				ScriptVarLink b = Shift(ref execute);

				if (execute)
				{
					ScriptVar res = a.Var.MathsOp(b.Var, op);

					if (a.Owned)
					{
						a = new ScriptVarLink(res, null);
					}
					else
					{
						a.ReplaceWith(res);
					}
				}
			}

			return a;
		}
	}
}
