
using RabbIT.MarketGrid.Core;
using RabbIT.MarketGrid.Core.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RabbIT.MarketGrid.UI.Controls
{

    public partial class StockPriceViewItemControl : UserControl
    {
        public StockPriceViewItemControl()
        {
            InitializeComponent();
        }

        public string Code
        {
            get
            {
                return (string)GetValue(CodeProperty);
            }
            set
            {
                SetValue(CodeProperty, value);
            }
        }

        public static readonly DependencyProperty CodeProperty =
            DependencyProperty.Register("Code", typeof(string), typeof(StockPriceViewItemControl));

        public float Price
        {
            get
            {
                return (float)GetValue(PriceProperty);
            }
            set
            {
                SetValue(PriceProperty, value);
            }
        }

        public static readonly DependencyProperty PriceProperty =
            DependencyProperty.Register("Price", typeof(float), typeof(StockPriceViewItemControl), new PropertyMetadata(0f));


        public StatusStock Status
        {
            get
            {
                return (StatusStock)GetValue(StatusProperty);
            }
            set
            {
                SetValue(StatusProperty, value);
            }
        }

        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(StatusStock), typeof(StockPriceViewItemControl), new PropertyMetadata(StatusStock.Steel, OnStatusStockChanged));

        private static void OnStatusStockChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is StockPriceViewItemControl control)
            {
                StatusStock newStatus = (StatusStock)e.NewValue;            

                if (newStatus == StatusStock.Up)
                    control.Background = Brushes.LightGreen;

                if (newStatus == StatusStock.Down)
                    control.Background = Brushes.LightSalmon;
            }
        }
    }
}
