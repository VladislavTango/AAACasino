using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AAAcasino.Components
{
    public partial class BinablePasswordBox : UserControl
    {
        public static readonly DependencyProperty PasswordProperty = 
            DependencyProperty.Register(
                "Password", 
                typeof(string), 
                typeof(BinablePasswordBox), 
                new PropertyMetadata(string.Empty));
        public string? Password
        {
            get => (string?)GetValue(PasswordProperty);
            set => SetValue(PasswordProperty, value);
        }
        public BinablePasswordBox()
        {
            InitializeComponent();
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e) => Password = passwordBox.Password;
    }
}
