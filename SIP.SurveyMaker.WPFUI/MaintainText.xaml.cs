﻿using SIP.SurveyMaker.BL;
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
using System.Windows.Shapes;

namespace SIP.SurveyMaker.WPFUI
{
    public enum ScreenMode
    {
        Question = 0,
        Answer = 1
    }

    /// <summary>
    /// Interaction logic for MaintainText.xaml
    /// </summary>
    public partial class MaintainText : Window
    {
        List<Question> questions;
        List<Answer> answers;
        ScreenMode screenMode; 

        public MaintainText()
        {
            InitializeComponent();
        }

        public MaintainText(ScreenMode screenMode)
        {
            InitializeComponent();
            this.screenMode = screenMode;
            Reload();
            cboText.DisplayMemberPath = "Text";
            cboText.SelectedValuePath = "Id";
            this.Title = "Maintain " + screenMode.ToString() + "s";
            lblQA.Content = screenMode.ToString() + "s";

            btnInsert.IsEnabled = false;
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            switch (screenMode)
            {
                case ScreenMode.Question:
                    Question question = new Question() { Text = txtText.Text };
                    Task.Run(async () =>
                    {
                        int results = await QuestionManager.Insert(question);
                    });
                    questions.Add(question);
                    Rebind(questions.Count- 1);
                    break;
                case ScreenMode.Answer:
                    Answer answer = new Answer() { Text = txtText.Text };
                    Task.Run(async () =>
                    {
                        int results = await AnswerManager.Insert(answer);
                    });
                    answers.Add(answer);
                    Rebind(answers.Count - 1);
                    break;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Attempted update on index: " + cboText.SelectedIndex);
            switch (screenMode)
            {
                case ScreenMode.Question:
                    Question question = questions[cboText.SelectedIndex];
                    question.Text = txtText.Text;
                    Task.Run(async () =>
                    {
                        int results = await QuestionManager.Update(question);
                    });
                    Rebind(cboText.SelectedIndex);
                    break;
                case ScreenMode.Answer:
                    Answer answer = answers[cboText.SelectedIndex];
                    answer.Text = txtText.Text;
                    Task.Run(async () =>
                    {
                        int results = await AnswerManager.Update(answer);
                    });
                    Rebind(cboText.SelectedIndex);
                    break;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            switch (screenMode)
            {
                case ScreenMode.Question:
                    Question question = questions[cboText.SelectedIndex];
                    Task.Run(async () =>
                    {
                        int results = await QuestionManager.Delete(question.Id);
                    });
                    questions.Remove(question);
                    Rebind(-1);
                    break;
                case ScreenMode.Answer:
                    Answer answer = answers[cboText.SelectedIndex];
                    Task.Run(async () =>
                    {
                        int results = await AnswerManager.Delete(answer.Id);
                    });
                    answers.Remove(answer);
                    Rebind(-1);
                    break;
            }
        }

        private void cboText_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboText.SelectedIndex > -1)
            {
                btnUpdate.IsEnabled = true;
                btnDelete.IsEnabled = true;
            }
            else
            {
                btnUpdate.IsEnabled = false;
                btnDelete.IsEnabled = false;
            }

            txtText.Text = string.Empty;
            if (cboText.SelectedIndex > -1)
            {
                if (screenMode == ScreenMode.Question)
                    txtText.Text = questions[cboText.SelectedIndex].Text;
                else
                    txtText.Text = answers[cboText.SelectedIndex].Text;
            }
        }

        private void txtText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtText.Text))
                btnInsert.IsEnabled = true;
            else
                btnInsert.IsEnabled = false;
        }

        private async void Reload()
        {
            cboText.ItemsSource = null;

            switch (screenMode)
            {
                case ScreenMode.Question:
                    questions = await QuestionManager.Load();
                    cboText.ItemsSource = questions;
                    break;
                case ScreenMode.Answer:
                    answers = await AnswerManager.Load();
                    cboText.ItemsSource = answers;
                    break;
            }
        }

        private void Rebind(int index)
        {
            cboText.ItemsSource = null;
            switch (screenMode)
            {
                case ScreenMode.Question:
                    cboText.ItemsSource = questions;
                    break;
                case ScreenMode.Answer:
                    cboText.ItemsSource = answers;
                    break;
            }

            cboText.DisplayMemberPath = "Text";
            cboText.SelectedValuePath = "Id";
            cboText.SelectedIndex = index;

            System.Diagnostics.Debug.WriteLine("Current index: " +  cboText.SelectedIndex);
        }


    }
}
