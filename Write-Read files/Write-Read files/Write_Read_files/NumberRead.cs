using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Write_Read_files
{
    class NumberRead
    {
        List<int> ints = new List<int>();
        public NumberRead()
        {}

        public bool ReadFromFile()
        {
            try
            {
                StreamReader myFile = new StreamReader("C:\\Users\\145371\\Desktop\\Output\\TestFile.txt");

                int fChar;
                fChar = myFile.Read();
                while (fChar != -1)
                {
                    Console.Write(Convert.ToChar(fChar));
                    ints.Add(Convert.ToChar(fChar));
                    fChar = myFile.Read();
                }

                myFile.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error reading file:");
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public List<int> Ints
        {
            get
            {
                return ints;
            }
        }
    }
}
