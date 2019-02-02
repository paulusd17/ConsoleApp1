using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1
{
    /// <summary>
    /// This class will conduct the indexing of a text file
    /// </summary>
    public class Index
    {
        /// <summary>
        /// A collection of single charactes and their frequency
        /// </summary>
        public N_gram Char_gram { get; set; }

        /// <summary>
        /// A collection of single terms and their frequency
        /// </summary>
        public N_gram Uni_gram { get; set; }

        /// <summary>
        /// A collection of bi-terms and their frequency
        /// </summary>
        public N_gram Bi_gram { get; set; }

        /// <summary>
        /// A collection of tri-terms and their frequency
        /// </summary>
        public N_gram Tri_gram { get; set; }

        /// <summary>
        /// An instance of the Porters Algorithum class <see cref="PortersStemmer"/>
        /// </summary>
        private PortersStemmer Stemmer { get; set; }

        /// <summary>
        /// Constructor that creates instances of the objects
        /// </summary>
        public Index()
        {
            Char_gram = new N_gram();
            Uni_gram = new N_gram();
            Bi_gram = new N_gram();
            Tri_gram = new N_gram();
            Stemmer = new PortersStemmer();
        }

        /// <summary>
        /// This will take in a text file, read the file by line, parse the terms, stem the terms, and set the terms into the correct dictionary
        /// </summary>
        /// <param name="path">The path to the file that is being currently worked</param>
        public void IndexFile(string path)
        {
            //Read in the file
            FileInfo info = new FileInfo(path);
            StreamReader sr = new StreamReader(path);

            //Loop through the file line by line
            while (!sr.EndOfStream)
            {
                string orgline = sr.ReadLine();

                string[] terms = orgline.Split(' ');

                //Break the line down into its individual characters
                foreach (string s in terms)
                {
                    char[] scrubList = Token_Char(s);
                    foreach (char c in scrubList)
                    {
                        Char_gram.Add(c.ToString());
                    }
                }

                //break the line down into its individual tokens
                string line = Token_String(orgline);
                terms = line.Split(' ');

                List<string> temp = new List<string>();

                //remove any white spaces that ended up in the set
                foreach (string s in terms)
                {
                    string hold = s.Trim();
                    if (hold.Length > 0)
                    {
                        temp.Add(hold);
                    }
                }

                terms = temp.ToArray();

                //if the set has terms then process the single terms
                if (terms.Length >= 1)
                {
                    foreach (string term in terms)
                    {
                        //if (term.Contains("affection"))
                        //{
                        //    string s = term;
                        //}
                        Uni_gram.Add(Stemmer.Stem(term));
                    }
                }

                //if the set has terms then process the bi-terms
                if (terms.Length >= 2)
                {
                    string biGram = "";
                    for (int i = 0; i < terms.Length - 1; i++)
                    {
                        biGram = Stemmer.Stem(terms[i]) + " " + Stemmer.Stem(terms[i + 1]);
                        Bi_gram.Add(biGram);
                    }
                }

                //if the set has terms then process the tri-terms
                if (terms.Length >= 3)
                {
                    string triGram = "";
                    for (int i = 0; i < terms.Length - 2; i++)
                    {
                        triGram = Stemmer.Stem(terms[i]) + " " + Stemmer.Stem(terms[i + 1]) + " " + Stemmer.Stem(terms[i + 2]);
                        Tri_gram.Add(triGram);
                    }
                }
            }
        }

        /// <summary>
        /// Take a string of terms and convert to lower, remove punctuation, and return the char[]
        /// </summary>
        /// <param name="term">the string that contains the terms</param>
        /// <returns>a tokenized char[]</returns>
        private char[] Token_Char(string term)
        {
            List<char> returnList = new List<char>();
            term = term.Trim().ToLowerInvariant();
            foreach (char c in term)
            {
                if ((int)c >= 97 && (int)c <= 122)
                {
                    returnList.Add(c);
                }
            }
            return returnList.ToArray();
        }

        /// <summary>
        /// Takes a string of terms and converts to lower, removes punctuation, and returns the reconstructed string of terms
        /// </summary>
        /// <param name="term">the string that contains the terms</param>
        /// <returns>a new string of tokenized terms</returns>
        private string Token_String(string terms)
        {
            terms = terms.Replace("--", " ");
            string[] sentence = terms.Split(' ');

            string returnItem = "";

            foreach (string term in sentence)
            {
                if (term.Length > 0)
                {
                   char[] tokenizedTerm = Token_Char(term);
                    if(tokenizedTerm.Length > 0)
                    {
                        string temp = "";
                        foreach(char c in tokenizedTerm)
                        {
                            temp += c;
                        }
                        returnItem += temp + " ";
                    }
                }
            }

            return returnItem;
        }

        /// <summary>
        /// Writes each of the dictionaries to file
        /// </summary>
        public void WriteToFile()
        {
            Char_gram.ToCSV("Char.csv");
            Uni_gram.ToCSV("Uni.csv");
            Bi_gram.ToCSV("Bi.csv");
            Tri_gram.ToCSV("Tri.csv");
        }
    }
}
