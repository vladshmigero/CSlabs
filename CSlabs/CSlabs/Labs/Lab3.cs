
using System.Text;
using System.Text.RegularExpressions;

namespace CSlabs.Labs
{
    public class Lab3
    {
        public class Token
        {
            public string Word { get; private set; }
            public bool Isword { get; private set; }
            public Token (string word, bool isword)
            {
                this.Word = word;
                this.Isword = isword;
            }
            public override string ToString()
            {
                return this.Word;
            }
        }
        public class Sentence
        {
            public List<Token> Tokens { get; private set; } = new List<Token>();
            public override string ToString() 
            {
                var sb = new System.Text.StringBuilder();
                foreach (var token in Tokens)
                {
                    sb.Append(token.Word + " ");
                }
                return sb.ToString();
            }


        }
        public class Text
        {
            public List<Sentence> Sentences { get; private set; } = new List<Sentence>();
            public override string ToString()
            {
                var sb = new StringBuilder();
                for (int i = 0; i < Sentences.Count; i++)
                {
                    sb.Append(Sentences[i].ToString());
                }
                return sb.ToString();
            }
            public void Sort1()
            {
                List<Sentence> sentences2 = (Sentences);
                int count = 0;
                sentences2.Sort(delegate (Sentence s1, Sentence s2)
                {
                    int count1 = 0;
                    foreach (var token in s1.Tokens)
                    {
                        if (token.Isword) count1++;
                    }

                    int count2 = 0;
                    foreach (var token in s2.Tokens)
                    {
                        if (token.Isword) count2++;
                    }

                    return count1.CompareTo(count2);
                });

                Console.WriteLine("\nПредложения по возрастанию количества слов:");
                foreach (Sentence sentence in sentences2)
                {
                    int wordCount = 0;
                    foreach (var token in sentence.Tokens)
                    {
                        if (token.Isword) wordCount++;
                    }

                    Console.WriteLine("[Слова: " + wordCount + "] " + sentence);
                }
            }
            public void Sort2()
            {
                List<Sentence> sentences = new List<Sentence>(Sentences);
                
            }
        }
        class Parser
        {
            public static Text Parse(string sequences)
            {
                var matches = Regex.Matches(sequences, @"\w+|[.,!?;:]");//регулярка слова или знаки препинания .,!?;:
                var text = new Text();
                var sentence = new Sentence();
                foreach (Match match in matches)
                {
                    string tokenValue = match.Value;
                    bool isWord = Regex.IsMatch(tokenValue, @"\w+");
                    var token = new Token(tokenValue, isWord);
                    sentence.Tokens.Add(token);
                    if (tokenValue == "." || tokenValue == "!" || tokenValue == "?" || tokenValue == ":" || tokenValue == ";")
                    {
                        text.Sentences.Add(sentence);
                        sentence = new Sentence();
                    }
                }
                return text;
            }
        }




        static void Main(string[] args)
        {
            string inputFile = @"C:\Users\user\source\repos\vladshmigero\CSlabs\CSlabs\CSlabs\TextFile.txt";
            string sequences = File.ReadAllText(inputFile);
            Text parsedText = Parser.Parse(sequences);
            Console.WriteLine("Готовый текст:");
            Console.WriteLine(parsedText);
            parsedText.Sort1();
        }
    }
}
