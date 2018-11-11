using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLab;
using static DataLab.Block_info;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static DataLab.Fabricator;
using static DataLab.MainWindow;
using System.Reflection;

namespace DataLab
{
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public class Link
    {

        public dynamic output_ref;
        public dynamic input_ref;

        Point input = new Point(0, 0);
        Point output = new Point(0, 0);

        Line_container line_con = new Line_container();

        public struct Line_container
        {
            public Line lineA;
            public Line lineB;
            public Line lineC;

            public Point first;
            public Point second;
            public Point third;
        }

        public Link(IO_sockiet.output_sockiet out_ref, IO_sockiet.input_sockiet in_ref)
        {
            output_ref = out_ref;
            input_ref = in_ref;

            output_ref.Disconnect();
            input_ref.Disconnect();

            Connect_link(input_ref, output_ref);
            CreateLines();

            Update_Line();
        }


        private void CreateLines()
        {
            line_con.lineA = new Line();
            line_con.lineA.Visibility = Visibility.Visible;
            SolidColorBrush redBrush = new SolidColorBrush();
            redBrush.Color = Colors.Red;
            line_con.lineA.StrokeThickness = 4;
            line_con.lineA.Stroke = redBrush;
            MainGrid_ref.Children.Add(line_con.lineA);

            line_con.lineB = new Line();
            line_con.lineB.Visibility = Visibility.Visible;
            //SolidColorBrush redBrush = new SolidColorBrush();
            redBrush.Color = Colors.Red;
            line_con.lineB.StrokeThickness = 4;
            line_con.lineB.Stroke = redBrush;
            MainGrid_ref.Children.Add(line_con.lineB);

            line_con.lineC = new Line();
            line_con.lineC.Visibility = Visibility.Visible;
            //SolidColorBrush redBrush = new SolidColorBrush();
            redBrush.Color = Colors.Red;
            line_con.lineC.StrokeThickness = 4;
            line_con.lineC.Stroke = redBrush;
            MainGrid_ref.Children.Add(line_con.lineC);
        }

        public void Update_Line()
        {
            Calculate_Positions();
        }

        public void Calculate_Positions()
        {
           double X1 = output_ref.X;
           double X2 = input_ref.X;
           double Y1 = output_ref.Y;
           double Y2 = input_ref.Y;

            line_con.first = new Point((X1 + X2) / 2, Y1);
            line_con.second = new Point((X1 + X2) / 2, Y2);
            line_con.third = new Point(X2, Y2);

            line_con.lineA.X1 = X1;
            line_con.lineA.X2 = line_con.first.X;
            line_con.lineA.Y1 = Y1;
            line_con.lineA.Y2 = line_con.first.Y;

            line_con.lineB.X1 = line_con.first.X;
            line_con.lineB.X2 = line_con.second.X;
            line_con.lineB.Y1 = line_con.first.Y;
            line_con.lineB.Y2 = line_con.second.Y;

            line_con.lineC.X1 = line_con.second.X;
            line_con.lineC.X2 = line_con.third.X;
            line_con.lineC.Y1 = line_con.second.Y;
            line_con.lineC.Y2 = line_con.third.Y;
        }

        public void Connect_link(IO_sockiet.input_sockiet input, IO_sockiet.output_sockiet output)
        {
            input.link_ref = this;
            output.link_ref = this;

            output.function_ref = input.function_ref;
        }

        public void Disconnect_link(dynamic parent)
        {
            output_ref.function_ref = null;

            output_ref.link_ref = null;
            input_ref.link_ref = null;



            Console.WriteLine(parent);

            MainGrid_ref.Children.Remove(line_con.lineA);
            MainGrid_ref.Children.Remove(line_con.lineB);
            MainGrid_ref.Children.Remove(line_con.lineC);

            output_ref = null;
            input_ref = null;

            MainWindow.link_list.Remove(this);
        }


    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
