using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class IndexTest
    {
        [TestMethod]
        public void TestIndexNoError()
        {
            string testPath = Directory.GetCurrentDirectory() + "/TestData/corpus";
            ConsoleApp1.Program.Main(new string[] { testPath });
        }

        [TestMethod]
        public void TestIndexCharReport()
        {
            string testPath = Directory.GetCurrentDirectory() + "/Reports/Char.csv";
            if (File.Exists(testPath))
            {
                StreamReader sr = new StreamReader(testPath);
                int rows = 0;
                bool value = false;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    rows++;
                    if (line.Contains("d"))
                    {
                        if (line.Substring(line.IndexOf(',')).Contains("1"))
                        {
                            value = true;
                        }
                    }
                }
                Assert.IsTrue(rows == 15, "Invalid amount of characters");
                Assert.IsTrue(value, "the char d was not found or it had a value greater than 1");
            }
            else
            {
                Assert.Fail("Char.csv does not exist");
            }
        }

        [TestMethod]
        public void TestIndexUniReport()
        {
            string testPath = Directory.GetCurrentDirectory() + "/Reports/Uni.csv";
            if (File.Exists(testPath))
            {
                StreamReader sr = new StreamReader(testPath);
                int rows = 0;
                bool value = false;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    rows++;
                    if (line.Contains("line"))
                    {
                        if (line.Substring(line.IndexOf(',')).Contains("2"))
                        {
                            value = true;
                        }
                    }
                }
                Assert.IsTrue(rows == 9, "Invalid amount of uni-grams");
                Assert.IsTrue(value, "the uni-gram 'line' was not found or it had a value != 2");
            }
            else
            {
                Assert.Fail("Char.csv does not exist");
            }
        }

        [TestMethod]
        public void TestIndexBiReport()
        {
            string testPath = Directory.GetCurrentDirectory() + "/Reports/Bi.csv";
            if (File.Exists(testPath))
            {
                StreamReader sr = new StreamReader(testPath);
                int rows = 0;
                bool value = false;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    rows++;
                    if (line.Contains("is a"))
                    {
                        if (line.Substring(line.IndexOf(',')).Contains("2"))
                        {
                            value = true;
                        }
                    }
                }
                Assert.IsTrue(rows == 8, "Invalid amount of bi-grams");
                Assert.IsTrue(value, "the bi gram 'is a' was not found or it had a value != 2");
            }
            else
            {
                Assert.Fail("Char.csv does not exist");
            }
        }

        [TestMethod]
        public void TestIndexTriReport()
        {
            string testPath = Directory.GetCurrentDirectory() + "/Reports/Tri.csv";
            if (File.Exists(testPath))
            {
                StreamReader sr = new StreamReader(testPath);
                int rows = 0;
                bool value = false;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    rows++;
                    if (line.Contains("line with punctuat"))
                    {
                        if (line.Substring(line.IndexOf(',')).Contains("1"))
                        {
                            value = true;
                        }
                    }
                }
                Assert.IsTrue(rows == 7, "Invalid amount of tri-grams");
                Assert.IsTrue(value, "the string 'line with punctuat' was not found or it had a value != 1");
            }
            else
            {
                Assert.Fail("Char.csv does not exist");
            }
        }
    }
}
