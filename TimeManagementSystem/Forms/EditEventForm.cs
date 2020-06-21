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
    public partial class EditEventForm : Form
    {
        private List<eventTypeHelper> _eventTypes = new List<eventTypeHelper>();
        private TaskEvent taskEvent = null;
        private AppointmentEvent appointmentEvent = null;
        private ISaveObject eventValue;
        private bool _isNewRecord = true;


        class eventTypeHelper
        {
            public int TypeId { get; set; }
            public string TypeName { get; set; }
            public EventType EventType { get; set; }

            public override string ToString()
            {
                return TypeName;
            }
        }

        public EditEventForm(BaseEvent _eventValue, EventType type, bool isNewRecord = true)
        {
            InitializeComponent();

            if (isNewRecord)
                this.Text = "New event";

            _isNewRecord = isNewRecord;

            SetEventTypeHelperList();

            cmbEventTypes.Items.AddRange(_eventTypes.ToArray());

            if (_eventValue != null)
            {
                if (type == EventType.Appointment)
                    eventValue = (AppointmentEvent)_eventValue;
                else
                    eventValue = (TaskEvent)_eventValue;

                cmbEventTypes.SelectedItem = _eventTypes.First(it => it.TypeId == (int)_eventValue.ActionType);
            }
            else
            {
                if (type == EventType.Appointment)                
                    eventValue = new AppointmentEvent();                
                else
                    eventValue = new TaskEvent();
                 
            }
        }

        private void SetEventTypeHelperList()
        {
            foreach (EventType tp in Enum.GetValues(typeof(EventType)))
            {
                _eventTypes.Add(new eventTypeHelper() { TypeId = (int)tp, TypeName = ApplicationData.GetEnumText(tp), EventType = tp });
            }
        }               

        private void btnSave_Click(object sender, EventArgs e)
        {
            string result;

            if (IsValidData(out result))
            {
                bool isSuccess = false;
                
                isSuccess = SaveEvent(eventValue);
                
                if (isSuccess)
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

            if (string.IsNullOrWhiteSpace(textEdit1.Text))
                result += "Event's name is empty\r\n";

            if (dateTimePicker1.Value == null || dateTimePicker1.Value == DateTime.MinValue)
                result += "Event's date is null\r\n";

            if (dateTimePicker2.Value == null || dateTimePicker2.Value == DateTime.MinValue)
                result += "Event's time is null\r\n";

            if (cmbEventTypes.SelectedItem == null)
                result += "Event's type is null\r\n";
            else
            {
                if (((eventTypeHelper)cmbEventTypes.SelectedItem).EventType == EventType.Appointment)
                {
                    if (comboBox2.SelectedValue == null)
                        result += "Event's contact is null\r\n";
                }
            }

            return true;
        }

        private bool SaveEvent(ISaveObject _event)
        {
            if (_event is AppointmentEvent)
            {
                appointmentEvent = (AppointmentEvent)_event;
                appointmentEvent.Name = textEdit1.Text;
                appointmentEvent.Description = textBox1.Text;
                appointmentEvent.RegDate = dateTimePicker1.Value + dateTimePicker2.Value.TimeOfDay;
                appointmentEvent.Year = appointmentEvent.RegDate.Year;
                appointmentEvent.Month = appointmentEvent.RegDate.Month;
                appointmentEvent.Date = dateTimePicker1.Value.Date;
                appointmentEvent.Time = dateTimePicker2.Value.TimeOfDay;
                appointmentEvent.RecDate = DateTime.Now;
                appointmentEvent.ActionType = ((eventTypeHelper)cmbEventTypes.SelectedItem).EventType;
                appointmentEvent.Contact = (Contact)comboBox2.SelectedItem;
                appointmentEvent.ContactId = ((Contact)comboBox2.SelectedItem).Id;
                appointmentEvent.Location = textEdit4.Text;

                _event = appointmentEvent;
            }
            else if (_event is TaskEvent)
            {
                taskEvent = (TaskEvent)_event;
                taskEvent.Name = textEdit1.Text;
                taskEvent.Description = textBox1.Text;
                taskEvent.RegDate = dateTimePicker1.Value + dateTimePicker2.Value.TimeOfDay;
                taskEvent.Year = appointmentEvent.RegDate.Year;
                taskEvent.Month = appointmentEvent.RegDate.Month;
                taskEvent.Date = dateTimePicker1.Value.Date;
                taskEvent.Time = dateTimePicker2.Value.TimeOfDay;
                taskEvent.RecDate = DateTime.Now;
                taskEvent.ActionType = ((eventTypeHelper)cmbEventTypes.SelectedItem).EventType;

                _event = taskEvent;
            }

            return _event.Save(CommandAttribute.INSERT);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
