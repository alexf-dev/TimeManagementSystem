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
using TimeManagementSystem.Objects.Enumerators;

namespace TimeManagementSystem.Forms
{
    public partial class EventList : Form
    {
        public DateTime DayDate { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Month { get; set; }

        class helper
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime RegDate { get; set; }
            public string EventName { get { return Name; } }
            public DayOfWeek DayOfWeek { get; set; }
            public string Location { get; set; }
            public string ContactName { get; set; }

            public string TimeofDay { get { return RegDate.TimeOfDay.ToString(); } }
            public string DayWithDate { get { return DayOfWeek.ToString() + Environment.NewLine + RegDate.Date.ToShortDateString().ToString(); } }
            public string DayName { get { return DayOfWeek.ToString(); } }
        }

        public EventList(EventListType planType, DateTime? dayDate, DateTime? beginDate, DateTime? endDate, int? month)
        {
            InitializeComponent();

            tabControl1.TabPages.Remove(tabPage3);

            switch (planType)
            {
                case EventListType.DayPlan:
                    DayDate = (DateTime)dayDate;
                    tabControl1.TabPages.Remove(tabPage2);
                    SetDayEvents(DayDate);
                    break;
                case EventListType.WeekPlan:
                    BeginDate = (DateTime)beginDate;
                    EndDate = (DateTime)endDate;
                    tabControl1.TabPages.Remove(tabPage1);
                    //tabPage2.v
                    SetWeekEvents(BeginDate, EndDate);
                    break;
                case EventListType.MonthPlan:
                    Month = (int)month;
                    tabControl1.TabPages.Remove(tabPage1);
                    tabPage2.Text = "Month plan";
                    SetMonthEvents(Month);
                    break;
            }              
        }

        private void SetDayEvents(DateTime date)
        {
            //dataGridView1

            List<TaskEvent> tasks = (DataTransfer.GetDataObjects<TaskEvent>(new GetDataFilterTaskEvent { IsDayPlan = true, DayDate = this.DayDate })).ConvertAll(it => (TaskEvent)it);
            List<AppointmentEvent> appointments = (DataTransfer.GetDataObjects<AppointmentEvent>(new GetDataFilterAppointmentEvent { IsDayPlan = true, DayDate = this.DayDate })).ConvertAll(it => (AppointmentEvent)it);
            List<Contact>  contacts = (DataTransfer.GetDataObjects<Contact>(new GetDataFilterContact { AllObjects = true })).ConvertAll(it => (Contact)it);

            List<helper> helpers = new List<helper>();

            foreach (var item in appointments)
            {
                item.Contact = contacts.FirstOrDefault(it => it.Id == item.ContactId);
                helpers.Add(new helper() { Name = item.Name, Description = item.Description, RegDate = item.RegDate, DayOfWeek = item.DayOfWeek, ContactName = item.Contact.Name, Location = item.Location });
            }

            foreach (var item in tasks)
                helpers.Add(new helper() { Name = item.Name, Description = item.Description, RegDate = item.RegDate, DayOfWeek = item.DayOfWeek, ContactName = "  -  ", Location = "  -  " });

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = helpers.OrderBy(it => it.RegDate).ToList();
        }

        private void SetWeekEvents(DateTime beginDate, DateTime endDate)
        {
            List<TaskEvent> tasks = (DataTransfer.GetDataObjects<TaskEvent>(new GetDataFilterTaskEvent { IsWeekPlan = true, BeginDate = this.BeginDate, EndDate = this.EndDate })).ConvertAll(it => (TaskEvent)it);
            List<AppointmentEvent> appointments = (DataTransfer.GetDataObjects<AppointmentEvent>(new GetDataFilterAppointmentEvent { IsWeekPlan = true, BeginDate = this.BeginDate, EndDate = this.EndDate })).ConvertAll(it => (AppointmentEvent)it);
            List<Contact> contacts = (DataTransfer.GetDataObjects<Contact>(new GetDataFilterContact { AllObjects = true })).ConvertAll(it => (Contact)it);

            List<helper> helpers = new List<helper>();

            foreach (var item in appointments)
            {
                item.Contact = contacts.FirstOrDefault(it => it.Id == item.ContactId);
                helpers.Add(new helper() { Name = item.Name, Description = item.Description, RegDate = item.RegDate, DayOfWeek = item.DayOfWeek, ContactName = item.Contact.Name, Location = item.Location });
            }

            foreach (var item in tasks)
                helpers.Add(new helper() { Name = item.Name, Description = item.Description, RegDate = item.RegDate, DayOfWeek = item.DayOfWeek, ContactName = "  -  ", Location = "  -  " });

            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.DataSource = helpers.OrderBy(it => it.RegDate).ToList();
        }

        private void SetMonthEvents(int month)
        {
            List<TaskEvent> tasks = (DataTransfer.GetDataObjects<TaskEvent>(new GetDataFilterTaskEvent { IsMonthPlan = true, Month = this.Month })).ConvertAll(it => (TaskEvent)it);
            List<AppointmentEvent> appointments = (DataTransfer.GetDataObjects<AppointmentEvent>(new GetDataFilterAppointmentEvent { IsMonthPlan = true, Month = this.Month })).ConvertAll(it => (AppointmentEvent)it);
            List<Contact> contacts = (DataTransfer.GetDataObjects<Contact>(new GetDataFilterContact { AllObjects = true })).ConvertAll(it => (Contact)it);

            List<helper> helpers = new List<helper>();

            foreach (var item in appointments)
            {
                item.Contact = contacts.FirstOrDefault(it => it.Id == item.ContactId);
                helpers.Add(new helper() { Name = item.Name, Description = item.Description, RegDate = item.RegDate, DayOfWeek = item.DayOfWeek, ContactName = item.Contact.Name, Location = item.Location });
            }

            foreach (var item in tasks)
                helpers.Add(new helper() { Name = item.Name, Description = item.Description, RegDate = item.RegDate, DayOfWeek = item.DayOfWeek, ContactName = "  -  ", Location = "  -  " });

            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.DataSource = helpers.OrderBy(it => it.RegDate).ToList();
        }

        bool IsTheSameCellValueGrid1(int column, int row)
        {
            DataGridViewCell cell1 = dataGridView1[column, row];
            DataGridViewCell cell2 = dataGridView1[column, row - 1];
            if (cell1.Value == null || cell2.Value == null)
            {
                return false;
            }
            return cell1.Value.ToString() == cell2.Value.ToString();
        }

        bool IsTheSameCellValueGrid2(int column, int row)
        {
            DataGridViewCell cell1 = dataGridView2[column, row];
            DataGridViewCell cell2 = dataGridView2[column, row - 1];
            if (cell1.Value == null || cell2.Value == null)
            {
                return false;
            }
            return cell1.Value.ToString() == cell2.Value.ToString();
        }

        private void EventList_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
            if (e.RowIndex < 1 || e.ColumnIndex != 0)
                return;
            if (IsTheSameCellValueGrid1(e.ColumnIndex, e.RowIndex))
            {
                e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            }
            else
            {
                e.AdvancedBorderStyle.Top = dataGridView1.AdvancedCellBorderStyle.Top;
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == 0 || e.ColumnIndex != 0)
                return;
            if (IsTheSameCellValueGrid1(e.ColumnIndex, e.RowIndex))
            {
                e.Value = "";
                e.FormattingApplied = true;
            }
        }

        private void dataGridView2_CellPainting_1(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
            if (e.RowIndex < 1 || e.ColumnIndex != 0)
                return;
            if (IsTheSameCellValueGrid2(e.ColumnIndex, e.RowIndex))
            {
                e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            }
            else
            {
                e.AdvancedBorderStyle.Top = dataGridView2.AdvancedCellBorderStyle.Top;
            }
        }

        private void dataGridView2_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == 0 || e.ColumnIndex != 0)
                return;
            if (IsTheSameCellValueGrid2(e.ColumnIndex, e.RowIndex))
            {
                e.Value = "";
                e.FormattingApplied = true;
            }
        }
    }
}
