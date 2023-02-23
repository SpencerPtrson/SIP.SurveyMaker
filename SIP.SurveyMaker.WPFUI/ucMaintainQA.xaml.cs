using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SIP.SurveyMaker.BL;
using SIP.SurveyMaker.BL.Models;

namespace SIP.SurveyMaker.WPFUI
{
    public enum ControlMode : int
    {
        Question = 0,
        Answer = 1
    }

    /// <summary>
    /// Interaction logic for ucMaintainQA.xaml
    /// </summary>
    public partial class ucMaintainQA : UserControl
    {
        ControlMode controlMode;
        public List<Question> Questions { get; set; }
        public List<Answer> Answers { get; set; }

        public ucMaintainQA()
        {
            InitializeComponent();
        }

        public ucMaintainQA(ControlMode controlMode, Guid selectedId)
        {
            InitializeComponent();
            this.controlMode = controlMode;
            Reload();
            cboText.SelectedValue = selectedId;
        }

        public ucMaintainQA(ControlMode controlMode, int selectedId)
        {
            InitializeComponent();
            this.controlMode = controlMode;
            Reload();
            cboText.SelectedValue = selectedId;
        }

        // Get control mode (whatever attribute the control is meant to get data for) and load appropriate data
        private async void Reload()
        {
            cboText.ItemsSource = null;

            switch (controlMode) // Set ItemsSource depending on ControlMode
            {
                case ControlMode.Question:
                    Questions = (List<Question>)await QuestionManager.Load();
                    cboText.ItemsSource = Questions;
                    break;
                case ControlMode.Answer:
                    Answers = (List<Answer>)await AnswerManager.Load();
                    cboText.ItemsSource = Answers;
                    break;
            }
            cboText.DisplayMemberPath = "Text";
            cboText.SelectedValuePath = "Id";
        }
    }
}
