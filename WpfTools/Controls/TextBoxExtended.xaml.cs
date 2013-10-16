using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for TextBoxExtended.xaml
    /// </summary>
    public partial class TextBoxExtended : UserControl
    {
        [Browsable(true), Bindable(true)]
        public string TextHint { get; set; }
        [Browsable(true), Bindable(true)]
        public string RegexValidator { get; set; }
        [Browsable(true), Bindable(true)]
        public Brush HintBrush { get; set; }

        private Regex _validator;

        private Regex TextValidator
        {
            get { return _validator = _validator ?? new Regex(RegexValidator); }
        }
        private bool _usesHint
        {
            get { return !string.IsNullOrWhiteSpace(TextHint); }
        }

        private bool _usesRegex
        {
            get { return !string.IsNullOrWhiteSpace(RegexValidator); }
        }
        private bool _hintShown { get; set; }

        private string _text
        {
            get { return txtBox.Text; } 
            set { txtBox.Text = value; }
        }

        public TextBoxExtended()
        {
            InitializeComponent();
            Loaded += TextBoxExtended_Loaded;
        }

        void TextBoxExtended_Loaded(object sender, RoutedEventArgs e)
        {
            if (_usesHint)
                ShowHint();

            if (_usesHint || _usesRegex)
            {
                txtBox.KeyDown += txtBox_KeyDown;
                txtBox.KeyUp += txtBox_KeyUp;
                txtBox.GotFocus += txtBox_GotFocus;
                txtBox.LostFocus += txtBox_LostFocus;
            }
        }

        void txtBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ShowHint();
        }

        void txtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ShowHint();
        }

        void txtBox_KeyUp(object sender, KeyEventArgs e)
        {
            ShowHint();
        }

        private readonly Key[] _ignoredKeys = new[]
        {
            Key.Escape,
            Key.LeftShift,
            Key.RightShift,
            Key.LeftCtrl,
            Key.RightCtrl,
            Key.LeftAlt,
            Key.RightAlt,
            Key.Enter,
            Key.End,
            Key.Home,
            Key.PageDown,
            Key.PageUp,
            Key.Back
        };
        void txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!_ignoredKeys.Contains(e.Key))
                HideHint();
            else
                ShowHint();
        }
        
        private void ShowHint()
        {
            if (_hintShown) return;
            if (!string.IsNullOrWhiteSpace(_text) && _text != TextHint)
                return;
            _text = TextHint;
            HintBrush = HintBrush ?? Brushes.DarkSlateGray;
            txtBox.Foreground = HintBrush;
            _hintShown = true;
        }

        private void HideHint()
        {
            if (!_hintShown) return;
            if (_text != TextHint) return;
            _text = "";
            txtBox.Foreground = Foreground;
            _hintShown = false;
        }
    }
}
