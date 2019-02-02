using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// This class holds the terms and frequencies for a corpus
    /// </summary>
    public class N_gram
    {
        /// <summary>
        /// This is the dictionary that holds the term and the frequency
        /// </summary>
        Dictionary<string, int> Terms { get; set; }

        /// <summary>
        /// constructor that sets the dictionary
        /// </summary>
        public N_gram()
        {
            Terms = new Dictionary<string, int>();
        }

        /// <summary>
        /// This will check if the key already exist in the collection. If it does not, then it will add it, else it will increment the frequency by 1
        /// </summary>
        /// <param name="term">The term to add or increment in the dictionary</param>
        public void Add(string term)
        {
            if (!Terms.ContainsKey(term))
            {
                Terms.Add(term, 1);
            }
            else
            {
                Terms[term]++;
            }
        }

        /// <summary>
        /// This will write the dictionary to a 'reports' folder of the root directory (location of the exe)
        /// </summary>
        /// <param name="OutputName">The name of the file being written too</param>
        public void ToCSV(string OutputName)
        {
            string basePath = Directory.GetCurrentDirectory() + "/Reports";
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }
            using (StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory() + "/Reports/" + OutputName))
            {
                foreach (KeyValuePair<string, int> keyValue in Terms)
                {
                    sw.WriteLine(keyValue.Key + "," + keyValue.Value);
                }
                sw.Flush();
                sw.Close();
            }
        }
    }
}
