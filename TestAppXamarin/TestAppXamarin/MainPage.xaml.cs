using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestAppXamarin
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public string first = "";
        public string second = "";
        public char znak = ' ';

        public string firstLast = "";
        public string secondLast = "";
        public char znakLast = ' ';

        protected override void OnAppearing()
        {
            //c от лево
            // r cреху
            Button btn;
            int r = 3;
            int c = 2;
            for (int i = 9; i > 0; i--)
            {
                
                btn = new Button();

                btn.Style = buttonStyle2;
                btn.Text = i.ToString();
                btn.Clicked += Btn_Clicked;

                if (c < 0)
                {
                    c = 2;
                    r++;
                }

                mainGrid.Children.Add(btn, c, r);
                c--;
            }

            btn = new Button();
            btn.Text = "0";
            btn.Style = buttonStyle2;
            btn.Clicked += Btn_Clicked;

            mainGrid.Children.Add(btn, 1, 6);

            base.OnAppearing();
        }

        private void Btn_Clicked(object sender, EventArgs e)
        {
            textBox.Text += ((Button)sender).Text;
            if (znak == ' ')
                first += ((Button)sender).Text.Contains('.') ? ((Button)sender).Text.Replace('.', ',') : ((Button)sender).Text;
            else
                second += ((Button)sender).Text.Contains('.') ? ((Button)sender).Text.Replace('.', ',') : ((Button)sender).Text;
            
        }
        private void ClearButton_Clicked(object sender, EventArgs e)
        {
            first = "";
            second = "";
            znak = ' ';
            textBox.Text = "";

        }
        private void btn_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            var err = new char[]{ '+', '-', '^', '%', '×', '÷', '=' };
            err.ToList().Remove(button.Text[0]);
            if (!textBox.Text.All(x => x != button.Text[0]) && !(textBox.Text.Length > 0)) return;
            for (int i = 0; i < err.Length; i++)
            {
                if (textBox.Text.EndsWith(err[i].ToString())) return;
            }

            textBox.Text += button.Text;
            znak = button.Text[0];
        }

        private void successButton_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(second) || String.IsNullOrEmpty(first)) return;
            string result = "";
            switch (znak)
            {
                case '+':
                    result = (float.Parse(first) + float.Parse(second)).ToString();
                    break;
                case '-':
                    result = (float.Parse(first) - float.Parse(second)).ToString();
                    break;
                case '^':
                    result = (Math.Pow(float.Parse(first), float.Parse(second))).ToString();
                    break;
                case '%':
                    result = (float.Parse(first) % float.Parse(second)).ToString();
                    break;
                case '×':
                    result = (float.Parse(first) * float.Parse(second)).ToString();
                    break;
                case '÷':
                    result = (float.Parse(first) / float.Parse(second)).ToString();
                    break;
            }
            textBoxHistory.Text = $"{first}{znak}{second}";

            firstLast = first;
            secondLast = second;
            znakLast = znak;

            ClearButton_Clicked(null, null);
            first = result;
            textBox.Text = result.Contains(',') ? result.Replace(',', '.') : result;
        }

        private void removeLastButton_Clicked(object sender, EventArgs e)
        {
           if (textBox.Text.Length > 0)
                if (znak == ' ')
                {
                    textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
                    first = textBox.Text;
                } else 
                {
                    if (textBox.Text.IndexOf(znak) >= first.Length) { 
                        znak = ' ';
                        textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
                        return;
                    }
                    textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
                    second = second.Remove(second.Length - 1);
                }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            first = firstLast;
            second = secondLast;
            znak = znakLast;
            textBox.Text = $"{first}{znak}{second}";
            textBoxHistory.Text = "";
        }
    }
}
