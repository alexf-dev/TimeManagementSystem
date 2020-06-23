using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeManagementSystem.Data;
using TimeManagementSystem.Objects;

namespace TimeManagementSystem.Forms
{
    public partial class ContactEditForm : Form
    {
        private bool _isNewRecord = true;

        public ContactEditForm(bool isNewRecord = true)
        {
            InitializeComponent();

            if (isNewRecord)
                this.Text = "New contact";

            _isNewRecord = isNewRecord;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string result;

            if (IsValidData(out result))
            {
                bool isSuccess = false;
                
                isSuccess = SaveContact(new Contact());

                if (isSuccess)
                    MessageBox.Show("Contact is save");
                else
                    MessageBox.Show("Save error");
            }
            else
            {
                MessageBox.Show(result, "Attention!");
                return;
            }
        }

        private bool IsValidData(out string result)
        {
            result = "";

            if (string.IsNullOrWhiteSpace(textBox1.Text))
                result += "Contact name is empty\r\n";

            if (!string.IsNullOrWhiteSpace(result))
                return false;

            return true;
        }

        private bool SaveContact(Contact saveValue)
        {
            saveValue.Name = textBox1.Text;
            saveValue.Phone = textBox2.Text;
            saveValue.EMail = textBox3.Text;
            saveValue.RecDate = DateTime.Now;

            return saveValue.Save(CommandAttribute.INSERT);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
