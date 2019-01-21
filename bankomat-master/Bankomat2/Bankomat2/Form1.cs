using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Bankomat2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            getAttempts();
            label3.Hide();
            label4.Hide();
            button12.Enabled = false;
        }
        String textboxText;
        #region
        private void button1_Click(object sender, EventArgs e)
        {
            textboxText = textBox1.Text;            
            textBox1.Text = textboxText + button1.Text;           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textboxText = textBox1.Text;
            textBox1.Text = textboxText + button2.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textboxText = textBox1.Text;
            textBox1.Text = textboxText + button3.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textboxText = textBox1.Text;
            textBox1.Text = textboxText+ button4.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textboxText = textBox1.Text;
            textBox1.Text = textboxText + button5.Text;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textboxText = textBox1.Text;
            textBox1.Text = textboxText + button6.Text;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textboxText = textBox1.Text;
            textBox1.Text = textboxText + button7.Text;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textboxText = textBox1.Text;
            textBox1.Text = textboxText + button8.Text;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textboxText = textBox1.Text;
            textBox1.Text = textboxText+button9.Text;
        }       

        private void button10_Click(object sender, EventArgs e)
        {
            textboxText = textBox1.Text;
            textBox1.Text = textboxText + button10.Text;            
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            selectInformation();          
        }
#endregion
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           if(textBox1.TextLength == 4)
            {
                button12.Enabled = true;
            }

           if(textBox1.TextLength>=4)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                button8.Enabled = false;
                button9.Enabled = false;
                button10.Enabled = false;
            }
           else
            {
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;
                button7.Enabled = true;
                button8.Enabled = true;
                button9.Enabled = true;
                button10.Enabled = true;
            }
        }

        public void selectInformation()
        {
            string ConnectionString = "server = localhost; port = 3306; database = bankomat;user=root; password=1234";
            string query = "SELECT code,attempCount,isBlocked from codeinformation";
            
            MySqlConnection databaseConnection = new MySqlConnection(ConnectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            try
            {                
                databaseConnection.Open();      
                reader = commandDatabase.ExecuteReader();              

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {                       
                        Boolean isBlocked = reader.GetBoolean(2);
                        String code = reader.GetString(0);
                        int attempts = reader.GetInt32(1);
                       
                        if(code == textBox1.Text)
                        {
                            MessageBox.Show("Correct code!");
                            label4.Show();                        
                        }
                        else
                        {
                            MessageBox.Show("Incorrect code!");
                            updateAttempts();
                            getAttempts();
                            button1.Enabled = true;
                            button2.Enabled = true;
                            button3.Enabled = true;
                            button4.Enabled = true;
                            button5.Enabled = true;
                            button6.Enabled = true;
                            button7.Enabled = true;
                            button8.Enabled = true;
                            button9.Enabled = true;
                            button10.Enabled = true;
                            textBox1.Text = "";
                        }

                        if(attempts==1)
                        {
                            blockCard();
                            label3.Show();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }              

                // Finally close the connection
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                // Show any error message.
                MessageBox.Show(ex.Message);
            }
        }

        public void updateAttempts()
        {
            string ConnectionString = "server = localhost;port = 3306; database = bankomat;user=root; password= 1234";
            string query = "UPDATE codeinformation set attempCount= attempCount - 1 ";

            // Prepare the connection
            MySqlConnection databaseConnection = new MySqlConnection(ConnectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;
            databaseConnection.Open();

            // Execute the query
            reader = commandDatabase.ExecuteReader();
        }

        public void blockCard()
        {
            string ConnectionString = "server = localhost; port = 3306; database = bankomat;user=root; password=1234";
            string query = "UPDATE codeinformation set isBlocked= true";

            // Prepare the connection
            MySqlConnection databaseConnection = new MySqlConnection(ConnectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            databaseConnection.Open();

            // Execute the query
            reader = commandDatabase.ExecuteReader();

            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
            button10.Enabled = false;
            button11.Enabled = false;
            button12.Enabled = false;         
        }

        public void getAttempts()
        {
            string ConnectionString = "server = localhost; port = 3306; database = bankomat;user=root; password=1234";
            string query = "SELECT attempCount from codeinformation";

            // Prepare the connection
            MySqlConnection databaseConnection = new MySqlConnection(ConnectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            try
            {
                // Open the database
                databaseConnection.Open();
                // Execute the query
                reader = commandDatabase.ExecuteReader();                

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int attempts = reader.GetInt32(0);
                        label2.Text = attempts.ToString();                                    
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                // Finally close the connection
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                // Show any error message.
                MessageBox.Show(ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
