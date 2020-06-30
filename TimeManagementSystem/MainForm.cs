using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeManagementSystem.Data;
using TimeManagementSystem.Forms;
using TimeManagementSystem.Objects;
using TimeManagementSystem.Objects.Enumerators;

namespace TimeManagementSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            cmbMonth.Items.AddRange(DateTimeFormatInfo.InvariantInfo.MonthNames);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            EditEventForm edit = new EditEventForm(null, EventType.Task);
            edit.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditEventForm edit = new EditEventForm(null, EventType.Appointment);
            edit.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

        private void btnDayEventList_Click(object sender, EventArgs e)
        {
            EventList listForm = new EventList(EventListType.DayPlan, dtpDay.Value.Date, null, null, -1);
            listForm.Show();
        }

        private void btnWeekEventList_Click(object sender, EventArgs e)
        {
            EventList listForm = new EventList(EventListType.WeekPlan, null, dtpBeginDate.Value.Date, dtpEndDate.Value.Date, -1);
            listForm.Show();
        }

        private void btnMonthEventList_Click(object sender, EventArgs e)
        {
            EventList listForm = new EventList(EventListType.MonthPlan, null, null, null, cmbMonth.SelectedIndex + 1);
            listForm.Show();
        }

        private void contactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ContactEditForm ce = new ContactEditForm();
            ce.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void contactListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ContactList cl = new ContactList();
            cl.Show();
        }
    }
}
