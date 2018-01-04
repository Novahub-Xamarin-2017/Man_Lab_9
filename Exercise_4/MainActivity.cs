using Android.App;
using Android.Widget;
using Android.OS;
using System;

namespace Exercise_4
{
    [Activity(Label = "Exercise_4", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private double inputValue;
        private double recentValue = 0;
        private string currentOperator = "";
        private bool isInputting = true;

        [InjectView(Resource.Id.txtExpression)] private TextView txtExpression;
        [InjectView(Resource.Id.txtResult)] private TextView txtResult;

        [InjectOnClick(Resource.Id.btnCE)]
        private void ClearEntry(object sender, EventArgs e)
        {
            txtResult.Text = "0";
            inputValue = 0;
        }

        [InjectOnClick(Resource.Id.btnC)]
        private void Clear(object sender, EventArgs e)
        {
            txtExpression.Text = "";
            txtResult.Text = "0";
            inputValue = 0;
            recentValue = 0;
            currentOperator = "";
        }

        [InjectOnClick(Resource.Id.btnZero)]
        private void InputZero(object sender, EventArgs e)
        {
            AddToInput(((Button)sender).Text);
        }

        [InjectOnClick(Resource.Id.btnOne)]
        private void InputOne(object sender, EventArgs e)
        {
            AddToInput(((Button)sender).Text);
        }

        [InjectOnClick(Resource.Id.btnTwo)]
        private void InputTwo(object sender, EventArgs e)
        {
            AddToInput(((Button)sender).Text);
        }

        [InjectOnClick(Resource.Id.btnThree)]
        private void InputThree(object sender, EventArgs e)
        {
            AddToInput(((Button)sender).Text);
        }

        [InjectOnClick(Resource.Id.btnFour)]
        private void InputFour(object sender, EventArgs e)
        {
            AddToInput(((Button)sender).Text);
        }

        [InjectOnClick(Resource.Id.btnFive)]
        private void InputFive(object sender, EventArgs e)
        {
            AddToInput(((Button)sender).Text);
        }

        [InjectOnClick(Resource.Id.btnSix)]
        private void InputSix(object sender, EventArgs e)
        {
            AddToInput(((Button)sender).Text);
        }

        [InjectOnClick(Resource.Id.btnSeven)]
        private void InputSeven(object sender, EventArgs e)
        {
            AddToInput(((Button)sender).Text);
        }

        [InjectOnClick(Resource.Id.btnEight)]
        private void InputEight(object sender, EventArgs e)
        {
            AddToInput(((Button)sender).Text);
        }

        [InjectOnClick(Resource.Id.btnNine)]
        private void InputNine(object sender, EventArgs e)
        {
            AddToInput(((Button)sender).Text);
        }

        [InjectOnClick(Resource.Id.btnDot)]
        private void InputDot(object sender, EventArgs e)
        {
            if (!txtResult.Text.Contains("."))
            {
                if (String.IsNullOrEmpty(txtResult.Text))
                {
                    txtResult.Text = "0.";
                }
                else
                {
                    txtResult.Text += ".";
                }
            }
            inputValue = Convert.ToDouble(txtResult.Text);
        }

        [InjectOnClick(Resource.Id.btnDel)]
        private void Delete(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtResult.Text))
            {
                txtResult.Text = txtResult.Text.Remove(txtResult.Text.Length - 1);
                inputValue = String.IsNullOrEmpty(txtResult.Text) ? 0 : Convert.ToDouble(txtResult.Text);
            }
        }

        [InjectOnClick(Resource.Id.btnChangeStatus)]
        private void ChangeStatus(object sender, EventArgs e)
        {
            if (txtResult.Text.Contains("-"))
            {
                txtResult.Text = txtResult.Text.Substring(1);
            }
            else
            {
                txtResult.Text = "-" + txtResult.Text;
            }
            inputValue = -inputValue;
        }

        [InjectOnClick(Resource.Id.btnAdd)]
        private void Add(object sender, EventArgs e)
        {
            ClickOperatorButton(((Button)sender).Text);
        }

        [InjectOnClick(Resource.Id.btnSub)]
        private void Sub(object sender, EventArgs e)
        {
            ClickOperatorButton(((Button)sender).Text);
        }

        [InjectOnClick(Resource.Id.btnMul)]
        private void Mul(object sender, EventArgs e)
        {
            ClickOperatorButton(((Button)sender).Text);
        }

        [InjectOnClick(Resource.Id.btnDiv)]
        private void Div(object sender, EventArgs e)
        {
            ClickOperatorButton(((Button)sender).Text);
        }

        [InjectOnClick(Resource.Id.btnSolve)]
        private void Solve(object sender, EventArgs e)
        {
            isInputting = false;
            ChooseOperation();
            txtExpression.Text = "";
            txtResult.Text = recentValue + "";
            currentOperator = "";
            inputValue = Convert.ToDouble(txtResult.Text);
        }

        private void ClickOperatorButton(string operation)
        {
            if (!String.IsNullOrEmpty(txtResult.Text))
            {
                ChooseOperation();
                txtExpression.Text = $"{recentValue} {operation}";
                txtResult.Text = "";
                currentOperator = operation;
                inputValue = 0;
            }
        }
        private void AddToInput(string number)
        {
            if (!isInputting)
            {
                txtResult.Text = "";
            }
            if (txtResult.Text.Equals("0"))
            {
                txtResult.Text = number;
            }
            else
            {
                txtResult.Text += number;
            }
            inputValue = Convert.ToDouble(txtResult.Text);
        }

        private void ChooseOperation()
        {
            switch (currentOperator)
            {
                case "":
                    recentValue = inputValue;
                    break;
                case "+":
                    recentValue += inputValue;
                    break;
                case "-":
                    recentValue -= inputValue;
                    break;
                case "*":
                    recentValue *= inputValue;
                    break;
                case "/":
                    recentValue /= inputValue;
                    break;
            }
        }


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            Cheeseknife.Inject(this);
            txtResult.Text = "";
            txtExpression.Text = "";
        }
    }
}

