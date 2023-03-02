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
    /// <summary>
    /// Interaction logic for ucMaintainQA.xaml
    /// </summary>
    public partial class ucMaintainQA : UserControl
    {
        public List<Question> Questions { get; set; }
        public List<Answer> Answers { get; set; }

        // This control should only have a single question Id sent to it as a parameter

        public ucMaintainQA()
        {
            InitializeComponent();
            Reload();
        }

        private async void Reload()
        {
            cboText.ItemsSource = null;
            rbIsCorrect.IsChecked = false;
            Answers = await AnswerManager.Load();
            cboText.ItemsSource = Answers;
            cboText.DisplayMemberPath = "Text";
            cboText.SelectedValuePath = "Id";
        }

        private void cboText_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cboText.SelectedIndex > -1)
            {
                ReloadAnswerList();

                Answer answer = Answers[cboText.SelectedIndex];
                if (answer != null)
                {
                    DisplayAnswer(answer.Id);
                }
            }
        }

        public async void DisplayAnswer(Guid AnswerId)
        {

            int index = 0;
            for (int i = 0; i < Answers.Count; i++)
            {
                if (Answers[i].Id == AnswerId)
                {
                    index = i;
                    rbIsCorrect.IsChecked = Answers[i].IsCorrect;
                }
            }
            cboText.SelectedIndex = index;
        }

        public async void ReloadAnswerList()
        {
            Answers = await AnswerManager.Load();
        }


        //private void imgDelete_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (cboText.SelectedIndex != -1)
        //    {
        //        Task.Run(async () =>
        //        {
        //            int results = await QuestionAnswerManager.Delete( , Answers[cboText.SelectedIndex].Id);
        //        });
        //    }
        //    Reload();
        //}
    }
}
