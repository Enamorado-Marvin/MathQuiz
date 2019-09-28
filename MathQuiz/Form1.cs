using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace MathQuiz
{
    public partial class Form1 : Form
    {
        //Sound player
        private SoundPlayer _soundPlayer;

        //Create a Random object to generate random numbers
        Random randomizer = new Random();

        //Variables to store numbers of the addition
        int addend1;
        int addend2;
        
        // Variables to store numbers for the subtraction problem. 
        int minuend;
        int subtrahend;

        // Variables to store numbers for the multiplication problem. 
        int multiplicand;
        int multiplier;

        // Variables to store numbers  for the division problem.
        int dividend;
        int divisor;

        // This integer variable keeps track of the 
        // remaining time.
        int timeLeft;

        //Date
        DateTime thisDate = DateTime.Now;

        /// <summary>
        /// Start the quiz by filling in all of the problems
        /// and starting the timer.
        /// </summary>
        public void StartTheQuiz()
        {
            

            // Fill in the addition problem.
            // Generate two random numbers to add.
            // Store the values in the variables 'addend1' and 'addend2'.
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);

            // Convert the two randomly generated numbers
            // into strings so that they can be displayed
            // in the label controls.
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();

            // 'sum' is the name of the NumericUpDown control.
            // This step makes sure its value is zero before
            // adding any values to it.
            sum.Value = 0;

            // The subtraction problem.
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            // The multiplication problem.
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            // The division problem.
            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            // Start the timer.
            timeLeft = 30;
            timeLabel.Text = "30 seconds";
            timer1.Start();
        }

        public Form1()
        {
            InitializeComponent();
            _soundPlayer = new SoundPlayer("sms-alert-1-daniel_simon.wav");
            todayDate.Text = thisDate.ToString("dd MMMM yyyy");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //Start the Math Quiz
        //Disable the start button during timer
        private void StartButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startButton.Enabled = false;
            timeLabel.BackColor = default(Color);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {   if (CheckTheAnswer())
            {
                // If CheckTheAnswer() returns true, then the user 
                // got the answer right. Stop the timer  
                // and show a MessageBox.
                timer1.Stop();
                MessageBox.Show("You got all the answers right!",
                                "Congratulations!");
                startButton.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " seconds";
                if (timeLeft < 6)
                {
                    timeLabel.BackColor = Color.Red;
                }                
            }            

            else
            {
                timer1.Stop();
                timeLabel.Text = "Time's up";
                MessageBox.Show("You didn't finish in time.", "Sorry!");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                startButton.Enabled = true;
                timeLabel.BackColor = default(Color);
            }
        }

        private void TimeLabel_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Check the answer to see if the user got everything right.
        /// </summary>
        /// <returns>True if the answer's correct, false otherwise.</returns>
        private bool CheckTheAnswer()
        {
            if ((addend1 + addend2 == sum.Value) 
                && (minuend - subtrahend == difference.Value)
                && (multiplicand * multiplier == product.Value)
                && (dividend / divisor == quotient.Value))
                return true;
            else
                return false;
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            // Select the whole answer in the NumericUpDown control.
            NumericUpDown answerBox = sender as NumericUpDown;

            if(answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }

        // Play sound when correct number is entered in the NumericUpDown control
        // Sum
        private void sound_answer(object sender, EventArgs e)
        {
            if (addend1 + addend2 == sum.Value)
            {
                _soundPlayer.Play();
            }            
        }

        // Difference
        private void sound_answer_diff(object sender, EventArgs e)
        {
            if (minuend - subtrahend == difference.Value)
            {
                _soundPlayer.Play();
            }
        }

        // Multiplication
        private void sound_answer_mult(object sender, EventArgs e)
        {
            if (multiplicand * multiplier == product.Value)
            {
                _soundPlayer.Play();
            }
        }

        // Division
        private void sound_answer_divi(object sender, EventArgs e)
        {
            if(dividend/divisor == quotient.Value)
            {
                _soundPlayer.Play();
            }
        }
    }
}
