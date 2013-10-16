using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WpfTools.Controls
{
    /// <summary>
    /// Interaction logic for NumericUpDown.xaml
    /// </summary>
    public partial class NumericUpDown : UserControl
    {
        public enum NumberType
        {
            Integer, Double, Decimal, UnsignedInt
        }
        public struct Number
        {
            public NumberType Type { get; set; }
            public object Value { get; set; }

            public T Cast<T>()
            {
                if (Value is T)
                    return (T)Value;
                return default(T);
            }

            public double GetDouble()
            {
                return Cast<double>();
            }

            public decimal GetDecimal()
            {
                return Cast<decimal>();
            }

            public int GetInt()
            {
                return Cast<int>();
            }

            public uint GetUInt()
            {
                return Cast<uint>();
            }

            public static implicit operator double(Number n)
            {
                return n.GetDouble();
            }

            public static implicit operator decimal(Number n)
            {
                return n.GetDecimal();
            }

            public static implicit operator int(Number n)
            {
                return n.GetInt();
            }

            public static implicit operator uint(Number n)
            {
                return n.GetUInt();
            }

            public static implicit operator Number(decimal d)
            {
                return new Number() { Type = NumberType.Decimal, Value = d };
            }
            public static implicit operator Number(double d)
            {
                return new Number() { Type = NumberType.Double, Value = d };
            }
            public static implicit operator Number(int d)
            {
                return new Number() { Type = NumberType.Integer, Value = d };
            }

            public static implicit operator Number(uint d)
            {
                return new Number() { Type = NumberType.UnsignedInt, Value = d };
            }
        }

        [Browsable(true)]
        public NumberType ValueType { get; set; }

        [Browsable(true)]
        public Number Minimum { get; set; }
        [Browsable(true)]
        public Number Maximum { get; set; }
        [Browsable(true)]
        public Number Value { get; set; }
        [Browsable(true)]
        public string TextHint
        {
            get { return txtBox.TextHint; }
            set { txtBox.TextHint = value; }
        }

        public NumericUpDown()
        {
            InitializeComponent();
            Loaded += NumericUpDown_Loaded;
        }

        void NumericUpDown_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
