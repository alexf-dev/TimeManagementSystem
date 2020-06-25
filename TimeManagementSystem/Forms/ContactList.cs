﻿using System;
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
    public partial class ContactList : Form
    {
        public ContactList()
        {
            InitializeComponent();

            List<Contact> contacts = (DataTransfer.GetDataObjects<Contact>(new GetDataFilterContact { AllObjects = true })).ConvertAll(it => (Contact)it);

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = contacts.ToList();
        }
    }
}
