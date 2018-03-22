using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Write_Read_files
{
    class NumberWrite
    {
        public NumberWrite()
        {}

        public bool WriteToFile()
        {
            try
            {
                StreamWriter myFileOut = new StreamWriter("C:\\Users\\145371\\Desktop\\Output\\TestFile.txt", false);

                myFileOut.WriteLine("4");
                myFileOut.WriteLine("8");

                myFileOut.Close();
            } catch (Exception e)
            {
                Console.WriteLine("Error writing file:");
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }
    }
}
