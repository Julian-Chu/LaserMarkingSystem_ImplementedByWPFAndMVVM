using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LaserMarking
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        int xDim;
        int yDim;
        Button[,] buttonMatrix;
        WrapPanel[] WrapPanelArray;

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            int count;
            yDim = 8;
            xDim = 5;
            buttonMatrix = new Button[yDim, xDim];
            WrapPanelArray = new WrapPanel[yDim];

            if(SP1.Children.Count>0)
            {
                count = SP1.Children.Count;
                for (int i = 0; i < count; i++)
                    SP1.Children.RemoveAt(0);
            }


            count = 0;
            for(int y=0;y<yDim;y++)
            {
                WrapPanelArray[y] = new WrapPanel()
                {
                    Height = double.NaN,
                    Width  = double.NaN,
                    HorizontalAlignment=HorizontalAlignment.Stretch,
                };

                SP1.Children.Add(WrapPanelArray[y]);
                

                for(int x=0;x<xDim;x++)
                {
                    count+=1;
                    buttonMatrix[y, x] = new Button()
                    {
                       //Height=50,
                        Width=40,
                        
                        Content=count.ToString(),
                        Margin=new Thickness(1,1,1,1),
                     };
                    //WP1.Children.Add(buttonMatrix[y, x]);
                    WrapPanelArray[y].Children.Add(buttonMatrix[y, x]);
                }
               
            }
            MSG1.Text = "Built tray";



        }


    }
}
