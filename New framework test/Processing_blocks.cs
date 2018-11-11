using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataLab.IO_sockiet;
using DataLab;
using static DataLab.Blocks;
using System.Windows.Input;
using System.Windows.Data;
using System.IO;
using System.Drawing;
using System.Reflection;
using static DataLab.MainWindow;
using New_framework_test;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace DataLab
{
    public partial class Blocks
    {

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public class Split_string_by_commas : Basic_block
        {
            protected input_sockiet.parent_function funct_ref;

            /// <summary>
            /// Block constructor, the most important place in the whole code: decodes what features are needed in a block.
            /// </summary>
            public Split_string_by_commas(string input) : base(input, block_type.Processing)
            {
                output_io = new output_sockiet(this, info.Output_info);
                funct_ref = new input_sockiet.parent_function(Input_function);
                input_io = new input_sockiet(this, info.Input_info, ref funct_ref);


                canvas.Children.Add(output_io.button);
                canvas.Children.Add(input_io.button);
            }

            public void Input_function(dynamic input)
            {
                string[] pieces = input.Split(',');
                foreach (string piece in pieces)
                {
                    Output_function(piece);
                }
            }

            public void Output_function(string input)
            {
                if (output_io != null)
                {
                    output_io.Call_next(input);
                }
            }

            public override void Render(double X, double Y)
            {
                output_io.Render(X, Y);
                input_io.Render(X, Y);
            }

            public override void Disconnect()
            {
                input_io.Disconnect();
                output_io.Disconnect();
                input_io = null;
                output_io = null;
            }

        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public class Split_data_flow : Basic_block
        {
            protected input_sockiet.parent_function funct_ref;
            public output_sockiet output_io2;

            /// <summary>
            /// Block constructor, the most important place in the whole code: decodes what features are needed in a block.
            /// </summary>
            public Split_data_flow(string input) : base(input, block_type.Flow)
            {
                output_io = new output_sockiet(this, info.Output_info);
                output_io2 = new output_sockiet(this, info.Output_info2);

                funct_ref = new input_sockiet.parent_function(Input_function);
                input_io = new input_sockiet(this, info.Input_info, ref funct_ref);

                canvas.Children.Add(output_io2.button);
                canvas.Children.Add(output_io.button);
                canvas.Children.Add(input_io.button);
            }

            public void Input_function(dynamic input)
            {
                Output_function(input);
            }

            public void Output_function(string input)
            {
                if (output_io != null)
                {
                    output_io.Call_next(input);
                }
                if (output_io2 != null)
                {
                    output_io2.Call_next(input);
                }
            }

            public override void Render(double X, double Y)
            {
                output_io.Render(X, Y);
                output_io2.Render(X, Y);
                input_io.Render(X, Y);
            }

            public override void Disconnect()
            {
                input_io.Disconnect();
                output_io.Disconnect();
                output_io2.Disconnect();
                input_io = null;
                output_io = null;
                output_io2 = null;
            }

        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public class While_Loop : Basic_block
        {
            private DataProcessing.Flow.Loops loops;
            public bool condition = true;

            /// <summary>
            /// Block constructor, the most important place in the whole code: decodes what features are needed in a block.
            /// </summary>
            public While_Loop(string input) : base(input, block_type.Flow)
            {
                loops = new DataProcessing.Flow.Loops();
            }

            public void Input_function(string input)
            {
                Loop(input);
            }

            public void Loop(string input)
            {
                while (condition)
                {
                    Output_function(input);
                }
            }

            public void Output_function(string input)
            {

            }

        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public class Add_string : Basic_block
        {
            private DataProcessing.Processing.Math math;
            protected input_sockiet.parent_function funct_ref;

            /// <summary>
            /// Block constructor, the most important place in the whole code: decodes what features are needed in a block.
            /// </summary>
            public Add_string(string input) : base(input, block_type.Processing)
            {

                math = new DataProcessing.Processing.Math();

                output_io = new output_sockiet(this, info.Output_info);
                funct_ref = new input_sockiet.parent_function(Input_function);
                input_io = new input_sockiet(this, info.Input_info, ref funct_ref);


                canvas.Children.Add(output_io.button);
                canvas.Children.Add(input_io.button);
            }

            public void Input_function(dynamic input)
            {
                Output_function(math.CustomOperation(input));
            }

            public void Output_function(string input)
            {
                if (output_io != null)
                {
                    output_io.Call_next(input);
                }
            }

            public override void Render(double X, double Y)
            {
                output_io.Render(X, Y);
                input_io.Render(X, Y);
            }

            public override void Disconnect()
            {
                input_io.Disconnect();
                output_io.Disconnect();
                input_io = null;
                output_io = null;
            }

        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    }
}
