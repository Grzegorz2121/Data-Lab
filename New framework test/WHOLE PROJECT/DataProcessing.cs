using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DataLab
{
    public class DataProcessing
    {

        /*This is class with fancy functions that are wrapped in blocks in blocks class*/
        //This is class for managing those functions and maybe chainging how they execiute

        public class Input
        {
            public class File
            {
                public string ReadLine(string input_path)
                {
                    StreamReader sr = new StreamReader(input_path);
                    return sr.ReadLine();
                }

                public List<string> ReadLines(string input_path)
                { 
                    StreamReader sr = new StreamReader(input_path);
                    List<string> string_list = new List<string>();
                    string line;
                    line = sr.ReadLine();
                    while(line!=null)
                    {
                        string_list.Add(line);
                        line = sr.ReadLine();
                    }

                    return string_list;
                }
            }
        }



        public class Processing
        {
            public class Math
            {
                public string CustomOperation(string input)
                {
                    return input + " PROCESSED ";
                }
            }
        }

        public class Flow
        {

            public class Loops
            {
                
            }

        }

        public class Output
        {

        }




    }

}
