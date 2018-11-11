using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataLab;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using static DataLab.Blocks;
using System.Reflection;
using static DataLab.Settings;

namespace DataLab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static dynamic previous_sockiet;
        public static dynamic next_sockiet;

        static string author = "CODE made by Grzegorz Machura (Grzegorz2121, Poland, Dolnyśląsk, I LO w Jaworze)";

        public enum block_type { Input, Output, Processing, Flow };

        public static List<dynamic> block_list = new List<dynamic>();
        public static List<dynamic> link_list = new List<dynamic>();

        public static Window MainWindow_ref;
        public static Grid MainGrid_ref;

        public string application_path = Assembly.GetExecutingAssembly().CodeBase;

        public int name = 0;
        
        public static string selected_block;

        public MainWindow()
        {
            InitializeComponent();

            MainWindow_ref = MainWindow1;
            MainGrid_ref = MainGrid;

            Create_timer(10, dispatcherTimer_Tick);
            Create_timer(100, GC_timer2_Tick);

            Assembly myAssembly = Assembly.GetExecutingAssembly();
            foreach (Type type in myAssembly.GetTypes())
            {
                if (type.BaseType == typeof(Blocks.Basic_block))
                {
                    Console.WriteLine(type.Name);
                    block_listbox1.Items.Add(type.Name.ToString());
                }

            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            dynamic ob = Activator.CreateInstance(Type.GetType(selected_block), name.ToString());
            name++;
            block_list.Add(ob);
        }

        private void GC_timer2_Tick(object sender, EventArgs e)
        {
             foreach (dynamic graf in block_list)
             {
                 if (graf.is_ded == true)
                 {
                     graf.Dispose();
                     block_list.Remove(graf);
                     Console.WriteLine("RIP");
                     break;
                 }
             }

            for (int i = 0; i < block_list.Count; i++)
            {
                if (block_list[i].is_ded == true)
                {
                    block_list[i].Disconnect();
                    block_list[i].Dispose();
                    block_list[i] = null;
                    block_list.Remove(block_list[i]);
                    Console.WriteLine("RIP");
                    break;
                }
            }

        }

        public static void Link_objects()
        {
            Console.WriteLine("Link!");
            if (next_sockiet != null && previous_sockiet != null)
            {
                Link link = new Link(previous_sockiet, next_sockiet);

                link_list.Add(link);

                System.Threading.Thread.Sleep(100);

                next_sockiet = null;
                previous_sockiet = null;
            }
        }

        public static void Create_timer(int interval, EventHandler function)
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += function;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(interval);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            foreach(Blocks.Basic_block t in block_list)
            {
                t.Move();
            }
        }

        private void block_listbox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selected_block = "DataLab.Blocks+" + block_listbox1.SelectedItem.ToString();
        }

        private void DEBUG_Click(object sender, RoutedEventArgs e)
        {
            foreach(dynamic dy in block_list)
            {
                Console.WriteLine(dy);
            }
            foreach (dynamic dy in MainGrid_ref.Children)
            {
                Console.WriteLine(dy);
            }
            foreach (dynamic dy in link_list)
            {
                Console.WriteLine(dy);
            }
        }

        private void MainWindow1_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("Clear!");
            next_sockiet = null;
            previous_sockiet = null;
        }
    }
}