using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace questions
{
    public class questionClass
    {
        public string Text;
        public string Answer;
        public List<string> Options;
        public int AskType;

        public questionClass(string text, string answer, int askfunc, params string[] options)
        {
            Text = text;
            Answer = answer;
            Options = options.ToList();
            AskType = askfunc;
        }
    }
}
