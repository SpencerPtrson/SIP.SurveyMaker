using SIP.SurveyMaker.BL;
using SIP.SurveyMaker.BL.Models;
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

namespace SIP.SurveyMaker.WPFUI
{
    /// <summary>
    /// Interaction logic for MaintainSurvey.xaml
    /// </summary>
    public partial class MaintainSurvey : Window
    {
        ucMaintainQA[] ucMaintainQAs = new ucMaintainQA[4];
        List<Question> questions;
        List<Answer> answers;
        List<Answer> qaSet;


        public MaintainSurvey()
        {
            InitializeComponent();
            DrawScreen();
        }

        private async void DrawScreen()
        {
            // Load question list as combobox source
            cboQuestions.ItemsSource = null;
            questions = (List<Question>)await QuestionManager.Load();
            cboQuestions.ItemsSource = questions;
            cboQuestions.DisplayMemberPath = "Text";
            cboQuestions.SelectedValuePath = "Id";
            cboQuestions.SelectedIndex = 0;

            ucMaintainQA answer1 = new ucMaintainQA();
            ucMaintainQA answer2 = new ucMaintainQA();
            ucMaintainQA answer3 = new ucMaintainQA();
            ucMaintainQA answer4 = new ucMaintainQA();

            answer1.Margin = new Thickness(100, -50, 0, 0);
            answer2.Margin = new Thickness(100, -10, 0, 0);
            answer3.Margin = new Thickness(100, 30, 0, 0);
            answer4.Margin = new Thickness(100, 70, 0, 0);

            grdSurvey.Children.Add(answer1);
            grdSurvey.Children.Add(answer2);
            grdSurvey.Children.Add(answer3);
            grdSurvey.Children.Add(answer4);

            ucMaintainQAs[0] = answer1;
            ucMaintainQAs[1] = answer2;
            ucMaintainQAs[2] = answer3;
            ucMaintainQAs[3] = answer4;

            LoadAnswers(questions[0].Id);
        }

        private void btnAddAnswer_Click(object sender, RoutedEventArgs e)
        {
            new MaintainText(ScreenMode.Answer).ShowDialog();
        }

        private void btnAddQuestions_Click(object sender, RoutedEventArgs e)
        {
            new MaintainText(ScreenMode.Question).ShowDialog();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void LoadAnswers(Guid QuestionId)
        {
            qaSet = (List<Answer>)await AnswerManager.LoadById(questions[cboQuestions.SelectedIndex].Id);

            for (int i = 0; i < qaSet.Count; i++)
            {
                ucMaintainQAs[i].SetAnswer(qaSet[i].Id);
            }
        }

        private void cboQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadAnswers(questions[cboQuestions.SelectedIndex].Id);
        }
    }
}
