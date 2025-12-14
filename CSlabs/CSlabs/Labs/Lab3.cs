using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace CSlabs.Labs
{
    public class Lab3
    {
        public class Token
        {
            [XmlText]
            public string Word { get;  set; }
            [XmlIgnore]
            [JsonIgnore]
            public bool Isword { get;  set; }
            [XmlIgnore]
            [JsonIgnore]
            public int Line { get; set; }
            public Token() { }
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
            [XmlElement("word")]
            public List<Token> Tokens { get;  set; } = new List<Token>();
            public override string ToString() 
            {
                var sb = new StringBuilder();
                bool firstWord = true;
                foreach (var token in Tokens)
                {
                    if (token.Isword)
                    {
                        if (!firstWord)
                        {
                            sb.Append(" ");
                        }
                        sb.Append(token.Word);
                        firstWord = false;
                    }
                    else
                    {
                        sb.Append(token.Word);
                        if (token.Word == "," || token.Word == ";" || token.Word == ":" || token.Word == "!" || token.Word == "?" || token.Word == ".")
                            sb.Append(" ");
                    }
                }
                return sb.ToString();
            }


        }
        [XmlRoot("text")]
        public class Text
        {
            [XmlElement("sentence")]
            public List<Sentence> Sentences { get;  set; } = new List<Sentence>();
            public override string ToString()
            {
                var sb = new StringBuilder();
                for (int i = 0; i < Sentences.Count; i++)
                {
                    sb.Append(Sentences[i].ToString());
                }
                return sb.ToString();
            }
            public List<Sentence> Sort1()
            {
                List<Sentence> sentences = (Sentences);
                sentences.Sort(delegate (Sentence s1, Sentence s2)
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

                return sentences;
            }
            public List<Sentence> Sort2()
            {
                List<Sentence> sentences = (Sentences);
                sentences.Sort(delegate (Sentence s1, Sentence s2)
                {
                    int len1 = s1.ToString().Length;
                    int len2 = s2.ToString().Length;
                    return len1.CompareTo(len2);
                });

                return sentences;
            }
            public List<string> Poisk(int length)
            {
                List<string> words = new List<string>();
                foreach (var sentence in Sentences)
                {
                    if (sentence.Tokens.Last().Word == "?")
                    {
                        foreach (var token in sentence.Tokens)
                        {
                            if (token.Isword && token.Word.Length == length)
                            {
                                if (!words.Contains(token.Word))
                                {
                                    words.Add(token.Word);
                                }
                            }
                        }
                    }
                }

                return words;
            }
            public Text Delite(int length)
            {
                char[] glasnye = { 'A','E','I','O','U','a', 'e', 'i', 'o', 'u' };
                foreach (var sentence in Sentences)
                {
                    for (int i = sentence.Tokens.Count - 1; i >= 0; i--)
                    {
                        var token = sentence.Tokens[i];
                        if (token.Isword && token.Word.Length == length)
                        {
                            char firstLetter = token.Word[0];
                            if (!glasnye.Contains(firstLetter))
                            {
                                sentence.Tokens.RemoveAt(i);
                            }
                        }
                    }
                }
                return this;
            }
            public Sentence zamena(int index, int length, string zamena)
            {
                if (index < 0 || index >= Sentences.Count)
                {
                    return null;
                }
                var sentence = Sentences[index];
                foreach (var token in sentence.Tokens)
                {
                    if (token.Isword && token.Word.Length == length)
                    {
                        token.Word = zamena;
                    }
                }
                return sentence;
            }
            public Text StopWords(List<string> stopWords)
            {
                foreach (var sentence in Sentences)
                {
                    for (int i = sentence.Tokens.Count - 1; i >= 0; i--)
                    {
                        var token = sentence.Tokens[i];

                        if (token.Isword)
                        {
                            string wordLower = token.Word.ToLower();
                            bool isStopWord = false;
                            foreach (var sw in stopWords)
                            {
                                if (wordLower == sw)
                                {
                                    isStopWord = true;
                                    break;
                                }
                            }
                            if (isStopWord)
                            {
                                sentence.Tokens.RemoveAt(i);
                            }
                        }
                    }
                }
                return this;
            }
            public static void ExportToXml(Text text, string filePath)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Text));
                using (var writer = new StreamWriter(filePath))
                {
                    serializer.Serialize(writer, text);
                }
                Console.WriteLine($"\nТекст успешно экспортирован в XML: {filePath}");
            }
            public static void ExportToJson(Text text, string filePath)
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                string json = JsonSerializer.Serialize(text, options);
                File.WriteAllText(filePath, json);
                Console.WriteLine($"\nТекст успешно экспортирован в JSON: {filePath}");
            }

            public static void ExportToBinary(Text text, string filePath)
            {
                using (var fs = new FileStream(filePath, FileMode.Create))
                using (var writer = new BinaryWriter(fs))
                {
                    foreach (var sentence in text.Sentences)
                    {
                        foreach (var token in sentence.Tokens)
                        {
                            writer.Write(token.Word);
                        }
                    }
                }
                Console.WriteLine($"\nТекст успешно экспортирован в бинарный формат: {filePath}");
            }
            public string Concordance(string text)
            {
                var concordance = new Dictionary<string, (int count, SortedSet<int> stroka)>(StringComparer.OrdinalIgnoreCase);
                var lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                for (int i = 0; i < lines.Length; i++)
                {
                    int index = i + 1;
                    var matches = Regex.Matches(lines[i], @"\w+");
                    foreach (Match match in matches)
                    {
                        string word = match.Value.ToLower();
                        if (!concordance.ContainsKey(word))
                        {
                            concordance[word] = (0, new SortedSet<int>());
                        }
                        var perem = concordance[word];
                        perem.count++;
                        perem.stroka.Add(index);
                        concordance[word] = perem;
                    }
                }
                var sb = new StringBuilder();

                List<string> words = concordance.Keys.ToList();
                words.Sort();
                foreach (string word in words)
                {
                    var perem = concordance[word];
                    int dots = Math.Max(1, 35 - word.Length);

                    sb.Append(word);
                    sb.Append(new string('.', dots));
                    sb.Append(perem.count);
                    sb.Append(": ");
                    sb.Append(string.Join(" ", perem.stroka));
                    sb.AppendLine();
                }

                return sb.ToString();
            }

            
        }
        class Parser
        {
            public static Text Parse(string sequences)
            {
                var matches = Regex.Matches(sequences, @"\w+|[.,!?;:]");
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
            var StopWords = File.ReadAllLines(@"C:\Users\user\source\repos\vladshmigero\CSlabs\CSlabs\CSlabs\StopWords.txt").ToList();
            Text parsedText = Parser.Parse(sequences);
            Console.WriteLine("Готовый текст:");
            Console.WriteLine(parsedText);
            Console.WriteLine("\nКонкорданс:");
            Console.WriteLine(parsedText.Concordance(sequences));
            Console.WriteLine("\nПредложения по возрастанию количества слов:");
            foreach (var sentence in parsedText.Sort1())
            {
                int wordCount = sentence.Tokens.Count(t => t.Isword);
                Console.WriteLine("[Кол-во слов: " + wordCount + "] " + sentence);
            }
            Console.WriteLine("\nПредложения по возрастанию длины:");
            foreach (var sentence in parsedText.Sort2())
            {
                int length = sentence.ToString().Length;
                Console.WriteLine("[Длина: " + (length - 1) + "] " + sentence);
            }
            Console.WriteLine("\nСлова какой длины вы хотите найти?");
            int a = int.Parse(Console.ReadLine());
            var foundWords = parsedText.Poisk(a);
            Console.WriteLine($"\nСлова длиной {a} в вопросительных предложениях:");
            foreach (var word in foundWords)
            {
                Console.WriteLine(word);
            }
            Console.WriteLine("\nСлова какой длины вы хотите удалить?");
            int b = int.Parse(Console.ReadLine());
            parsedText = parsedText.Delite(b);
            Console.WriteLine("Текст после удаления:");
            Console.WriteLine(parsedText);
            Console.WriteLine("\nКакой длины слово в каком предложении вы хотите заменить?\nВведите длину: ");
            int c = int.Parse(Console.ReadLine());
            Console.WriteLine("\nВведите номер предложения: ");
            int d = int.Parse(Console.ReadLine());
            Console.WriteLine("\nВведите текст: ");
            string text = Console.ReadLine();
            var changedSentence = parsedText.zamena(d, c, text);
            Console.WriteLine($"\nПосле замены в предложении {d}:");
            Console.WriteLine(changedSentence);
            parsedText = parsedText.StopWords(StopWords);
            Console.WriteLine("\nТекст после удаления стоп-слов:");
            Console.WriteLine(parsedText);
            string xml = @"C:\Users\user\source\repos\vladshmigero\CSlabs\CSlabs\CSlabs\TextExport.xml";
            Text.ExportToXml(parsedText, xml);
            string json = @"C:\Users\user\source\repos\vladshmigero\CSlabs\CSlabs\CSlabs\TextExport.json";
            Text.ExportToJson(parsedText, json);
            string bin = @"C:\Users\user\source\repos\vladshmigero\CSlabs\CSlabs\CSlabs\TextExport.bin";
            Text.ExportToBinary(parsedText, bin);
        }
    }
}
