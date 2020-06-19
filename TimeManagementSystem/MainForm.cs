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

namespace TimeManagementSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string result;

            if (IsValidData(out result))
            {
                if (SaveEvent())
                    MessageBox.Show("Event is save");
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

            return true;
        }

        private bool SaveEvent()
        {
            TaskEvent taskEvent = new TaskEvent();

            DateTime date = DateTime.Now;
            taskEvent.Name = "Learn C#";
            taskEvent.Description = "I learn C#";
            taskEvent.Year = date.Year;
            taskEvent.Month = date.Month;
            taskEvent.Date = date.Date;
            taskEvent.Time = date.TimeOfDay;
            taskEvent.RegDate = date;
            taskEvent.RecDate = DateTime.Now;
            taskEvent.ActionType = EventType.Task;

            return taskEvent.Save(CommandAttribute.INSERT);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<TaskEvent> tasks = (DataTransfer.GetDataObjects<TaskEvent>(new GetDataFilterTaskEvent { AllObjects = true })).ConvertAll(it => (TaskEvent)it);
        }
    }
}
