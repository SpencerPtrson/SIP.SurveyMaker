using SIP.SurveyMaker.BL;
using SIP.SurveyMaker.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
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

        public MaintainSurvey()
        {
            InitializeComponent();
            DrawScreen();
        }

        private async void DrawScreen()
        {
            // Load question list as combobox source
            cboQuestions.ItemsSource = null;
            questions = await QuestionManager.Load();
            cboQuestions.ItemsSource = questions;
            cboQuestions.DisplayMemberPath = "Text";
            cboQuestions.SelectedValuePath = "Id";
            cboQuestions.SelectedIndex = 0;

            ucMaintainQA answer1 = new ucMaintainQA();
            ucMaintainQA answer2 = new ucMaintainQA();
            ucMaintainQA answer3 = new ucMaintainQA();
            ucMaintainQA answer4 = new ucMaintainQA();

            answer1.Margin = new Thickness(100, -100, 0, 0);
            answer2.Margin = new Thickness(100, -40, 0, 0);
            answer3.Margin = new Thickness(100, 20, 0, 0);
            answer4.Margin = new Thickness(100, 80, 0, 0);

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
            Reload();
        }

        private void btnAddQuestions_Click(object sender, RoutedEventArgs e)
        {
            new MaintainText(ScreenMode.Question).ShowDialog();
            Reload();
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                // POSSIBLE METHODS
                // Delete all question-answer pairs and then insert new ones for the question
                // Delete the question itself and then reinsert it, with the QuestionManager also inserting into the QuestionAnswer table
                // Update the question answer list using the QuestionManager update, which deletes existing question-answer pairs and inserts the new ones

                Question question = questions[cboQuestions.SelectedIndex];
                System.Diagnostics.Debug.WriteLine("Question loaded");

                // Testing method: Delete from the QuestionAnswer table and then insert the new combinations

                await
                Task.Run(async () =>
                {
                    System.Diagnostics.Debug.WriteLine("Task.Run Started");
                    System.Diagnostics.Debug.WriteLine("Deleting Answers for Question: " + question.Text);
                    await QuestionAnswerManager.DeleteByQuestionId(question.Id);
                    question.Answers.Clear();

                    //System.Diagnostics.Debug.WriteLine("About to enter for-each");
                    //foreach (ucMaintainQA ucAnswer in ucMaintainQAs)
                    //{
                    //    System.Diagnostics.Debug.WriteLine("Inside of For-each");
                    //    question.Answers.Add(answers[ucAnswer.cboText.SelectedIndex]);
                    //}
                });

                Reload();
            }
            catch (Exception ex) { throw ex; }
        }

        private async void cboQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cboQuestions.SelectedIndex > -1)
                LoadAnswers(questions[cboQuestions.SelectedIndex].Id);
        }

        private async void Reload()
        {
            cboQuestions.ItemsSource = null;
            questions = await QuestionManager.Load();
            cboQuestions.ItemsSource = questions;
            cboQuestions.DisplayMemberPath = "Text";
            cboQuestions.SelectedValuePath = "Id";
            cboQuestions.SelectedIndex = questions.Count - 1;

            answers = await AnswerManager.Load();

            foreach (ucMaintainQA ucAnswer in ucMaintainQAs)
            {
                ucAnswer.cboText.ItemsSource = null;
                ucAnswer.cboText.ItemsSource = answers;
                ucAnswer.cboText.DisplayMemberPath = "Text";
                ucAnswer.cboText.SelectedValuePath = "Id";
            }
            LoadAnswers(questions[cboQuestions.SelectedIndex].Id);
        }

        private async void LoadAnswers(Guid QuestionId)
        {
            Question question = await QuestionManager.LoadById(QuestionId);
            List<Answer> answerSet = question.Answers;

            for (int i = 0; i < ucMaintainQAs.Length; i++)
            {
                if (i < answerSet.Count)
                {
                    ucMaintainQAs[i].DisplayAnswer(answerSet[i].Id);
                    ucMaintainQAs[i].rbIsCorrect.IsChecked = answerSet[i].IsCorrect;
                }    
                else
                {
                    ucMaintainQAs[i].cboText.SelectedIndex = -1;
                    ucMaintainQAs[i].rbIsCorrect.IsChecked = false;
                }              
            }
        }
    }
}
