using System.Data;
using System.Numerics;

namespace CalculatorApp
{
    public partial class Form1 : Form
    {
        private string calculation = "";
        private bool operator_button_available = false;
        private List<string> history = new List<string>(); 
        public Form1()
        {
            InitializeComponent();
           
        }

        private void button_Click(object sender, EventArgs e)
        {
            
            // This adds the number or operator to the string calculation
            calculation += (sender as Button).Text;
           
            // Display the current calculation back to the user
            if (calculation.Equals("0"))
            {
                calculation = "";
            }
            textBox1.Text = calculation;
            
            button_equal.Enabled = true;
            operator_button_available = true;
        }

        private void button_Click_Equals(object sender, EventArgs e)
        {
            
            if (Char.IsNumber(calculation[calculation.Length - 1]))
            {
                //The first step is to remove the ambiguous char to the correct char (ie. '÷' to '/')
                string formatCalc = calculation.ToString().Replace('×', '*').ToString().Replace("÷", "/").ToString().Replace("=", "");
                textBox1.Text = new DataTable().Compute(formatCalc, null).ToString();
                calculation = textBox1.Text;

                //Save to history and display last calculation
                if(formatCalc.Contains("*") | formatCalc.Contains("/") | formatCalc.Contains("+") | formatCalc.Contains("-")) {
                    
                    history.Add(formatCalc + "=" + calculation);
                    textBox_history_small.Text = formatCalc + "=" + calculation;
                }
               

                if (calculation.Equals("")) { textBox1.Text = "0"; }
                operator_button_available = true;

                
            }
            else
            {
                calculation = "";
                textBox1.Text = "Error";
            }

            
        }

        private void operator_Click(object sender, EventArgs e)
        {
            
            if (operator_button_available == true)
            {
                button_Click_Equals(sender, e);
                calculation += (sender as Button).Text;
                textBox1.Text = calculation;
                //Disable other operators other than equal
                operator_button_available = false;
                button_equal.Enabled = false;
                button_decimal.Enabled = true;
                //current_calculation = "";
            }       
        }

        private void sqrt_Root(object sender, EventArgs e)
        {
            if (operator_button_available == true)
            {
                

                string current_calculation = "";
                int i = calculation.Length-1;
                while ((calculation.Equals("") | calculation[i].Equals('×') | calculation[i].Equals('+') | calculation[i].Equals('-') | calculation[i].Equals('÷')) == false)
                {

                    current_calculation += calculation[i];
                    calculation = calculation.Remove(i, 1);
                    if (i == 0)
                    {
                        break;
                    }
                    else
                    {
                        i--;
                    }
                }
                char[] reverse = current_calculation.ToCharArray();
                Array.Reverse(reverse);
                current_calculation = new string(reverse);


                history.Add( calculation + (sender as Button).Text + current_calculation);
                textBox_history_small.Text = calculation + (sender as Button).Text + current_calculation;

                calculation += Math.Sqrt(double.Parse(current_calculation)).ToString();
                //Don't allow to click other operators other than equal
                button_Click_Equals(sender, e);
                
            }
        }

        private void power(object sender, EventArgs e)
        {
            if (operator_button_available == true)
            {
                string current_calculation = "";
                int i = calculation.Length - 1;
                while ((calculation.Equals("") | calculation[i].Equals('×') | calculation[i].Equals('+') | calculation[i].Equals('-') | calculation[i].Equals('÷')) == false)
                {

                    current_calculation += calculation[i];
                    calculation = calculation.Remove(i, 1);
                    if (i == 0)
                    {
                        break;
                    }
                    else
                    {
                        i--;
                    }
                }
                //Due to reading from the back we have reverse our number
                char[] reverse = current_calculation.ToCharArray();
                Array.Reverse(reverse);
                current_calculation = new string(reverse);

                history.Add(calculation + (sender as Button).Text + current_calculation);
                textBox_history_small.Text = calculation + (sender as Button).Text + current_calculation;

                calculation += Math.Pow(double.Parse(current_calculation), 2).ToString();
                //Don't allow to click other operators other than equal
                button_Click_Equals(sender, e);

            }
            
        }

        private void fraction(object sender, EventArgs e)
        {
            if (operator_button_available == true)
            {
                string current_calculation = "";
                int i = calculation.Length - 1;
                while ((calculation.Equals("") | calculation[i].Equals('×') | calculation[i].Equals('+') | calculation[i].Equals('-') | calculation[i].Equals('÷')) == false)
                {

                    current_calculation += calculation[i];
                    calculation = calculation.Remove(i, 1);
                    if (i == 0)
                    {
                        break;
                    }
                    else
                    {
                        i--;
                    }
                }

                calculation += "1/" + current_calculation;


                //Don't allow to click other operators other than equal
                button_Click_Equals(sender, e);

            }
        }

        private void plus_or_minus(object sender, EventArgs e)
        {
            if (operator_button_available == true)
            {
                string current_calculation = "";
                int i = calculation.Length - 1;
                while ((calculation.Equals("") | calculation[i].Equals('×') | calculation[i].Equals('+') | calculation[i].Equals('-') | calculation[i].Equals('÷')) == false)
                {

                    current_calculation += calculation[i];
                    calculation = calculation.Remove(i, 1);
                    if (i == 0)
                    {
                        break;
                    }
                    else
                    {
                        i--;
                    }
                }
                int x = (int)new DataTable().Compute(current_calculation, null);
                if (x > 0)
                {
                    calculation += " -" + current_calculation;
                    button_Click_Equals(sender, e);
                }
                else
                {
                    calculation += Math.Abs(x).ToString();
                    button_Click_Equals(sender, e);
                }
                //Don't allow to click other operators other than equal
                button_Click_Equals(sender, e);

            }
        }

        private void clear_all(object sender, EventArgs e) //CE
        {
            calculation = "";
            textBox1.Text = "0";
            button_equal.Enabled = false;
            history.Clear();
        }

        private void clear(object sender, EventArgs e) //C
        {
            calculation = "";
            textBox1.Text = "0";
        }

        private void delete(object sender, EventArgs e) //Delete feature
        {
            if (calculation.Length > 0)
            {
                calculation = calculation.Remove(calculation.Length - 1);
                textBox1.Text = calculation;
                //If there is no operator, enable button.
                if (operator_button_available == false)
                {
                    operator_button_available = true;
                }

                //If there is no input, don't allow user to use operator
                if (calculation.Length == 0)
                {
                    operator_button_available = false;
                }
            }
        }

        private void button_Decimal_click(object sender, EventArgs e) //decimal button "." 
        {
            calculation += (sender as Button).Text;
            button_decimal.Enabled = false;
        }

        private void history_Button_Click(object sender, EventArgs e) //Display history panel
        {
            if(panel_history.Visible == false)
            {
                for (int i = 0; i < history.Count; i++) {

                    textBox_history.Text += history[i] + Environment.NewLine;
                }
                panel_history.Visible = true;
                textBox_history.Visible = true;
            }
            else
            {
                textBox_history.Text = "";
                panel_history.Visible = false;
                textBox_history.Visible = false;
            }
            
        }
    }
}