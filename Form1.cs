using System;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // نوع کمیت
            cmbType.Items.Add("طول");
            cmbType.Items.Add("جرم");
            cmbType.Items.Add("زمان");
            cmbType.Items.Add("دما");

            cmbType.SelectedIndexChanged += CmbType_SelectedIndexChanged;
            btnConvert.Click += BtnConvert_Click;
        }

        private void CmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbFrom.Items.Clear();
            cmbTo.Items.Clear();

            string type = cmbType.SelectedItem.ToString();

            if (type == "طول")
            {
                cmbFrom.Items.AddRange(new object[] { "m", "cm", "mm", "km", "ft", "in" });
                cmbTo.Items.AddRange(new object[] { "m", "cm", "mm", "km", "ft", "in" });
            }
            else if (type == "جرم")
            {
                cmbFrom.Items.AddRange(new object[] { "kg", "g", "lb" });
                cmbTo.Items.AddRange(new object[] { "kg", "g", "lb" });
            }
            else if (type == "زمان")
            {
                cmbFrom.Items.AddRange(new object[] { "s", "min", "h" });
                cmbTo.Items.AddRange(new object[] { "s", "min", "h" });
            }
            else if (type == "دما")
            {
                cmbFrom.Items.AddRange(new object[] { "C", "F", "K" });
                cmbTo.Items.AddRange(new object[] { "C", "F", "K" });
            }
        }

        private void BtnConvert_Click(object sender, EventArgs e)
        {
            try
            {
                double value = double.Parse(txtValue.Text);
                string from = cmbFrom.SelectedItem.ToString();
                string to = cmbTo.SelectedItem.ToString();
                string type = cmbType.SelectedItem.ToString();
                double result = 0;

                if (type == "دما")
                {
                    // تبدیل دما
                    if (from == "C")
                        result = (to == "F") ? value * 9 / 5 + 32 : (to == "K") ? value + 273.15 : value;
                    else if (from == "F")
                        result = (to == "C") ? (value - 32) * 5 / 9 : (to == "K") ? (value - 32) * 5 / 9 + 273.15 : value;
                    else // from == "K"
                        result = (to == "C") ? value - 273.15 : (to == "F") ? (value - 273.15) * 9 / 5 + 32 : value;
                }
                else
                {
                    // تبدیل طول، جرم، زمان
                    double valInBase = 0;

                    // تبدیل به واحد پایه
                    switch (type)
                    {
                        case "طول":
                            valInBase = value * (from == "m" ? 1 : from == "cm" ? 0.01 : from == "mm" ? 0.001 : from == "km" ? 1000 : from == "ft" ? 0.3048 : 0.0254);
                            result = valInBase / (to == "m" ? 1 : to == "cm" ? 0.01 : to == "mm" ? 0.001 : to == "km" ? 1000 : to == "ft" ? 0.3048 : 0.0254);
                            break;
                        case "جرم":
                            valInBase = value * (from == "kg" ? 1 : from == "g" ? 0.001 : 0.453592);
                            result = valInBase / (to == "kg" ? 1 : to == "g" ? 0.001 : 0.453592);
                            break;
                        case "زمان":
                            valInBase = value * (from == "s" ? 1 : from == "min" ? 60 : 3600);
                            result = valInBase / (to == "s" ? 1 : to == "min" ? 60 : 3600);
                            break;
                    }
                }

                lblResult.Text = result + " " + to;
            }
            catch
            {
                MessageBox.Show("لطفاً یک عدد صحیح وارد کنید و همه واحدها را انتخاب کنید.");
            }
        }
    }
}
