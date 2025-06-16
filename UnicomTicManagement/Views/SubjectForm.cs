using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTicManagement.Controllers;
using UnicomTicManagement.Models;

namespace UnicomTicManagement.Views
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Subject subject = new Subject();
            subject.SuName = textSuName.Text;

            SubjectController sub = new SubjectController();
            sub.AddSubject(subject);
            MessageBox.Show("Subject Addded.");
        }


    }


}


            
    

