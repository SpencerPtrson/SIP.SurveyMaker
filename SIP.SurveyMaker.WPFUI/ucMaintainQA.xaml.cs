﻿using System;
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

        public ucMaintainQA()
        {
            InitializeComponent();
            Reload();
        }

        // Get control mode (whatever attribute the control is meant to get data for) and load appropriate data
        private async void Reload()
        {
            cboText.ItemsSource = null;
            Answers = (List<Answer>)await AnswerManager.Load();
            cboText.ItemsSource = Answers;
            cboText.DisplayMemberPath = "Text";
            cboText.SelectedValuePath = "Id";
        }

        public async void SetAnswer(Guid AnswerId)
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

        private void imgDelete_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AnswerManager.Delete(Answers[cboText.SelectedIndex].Id);
            Reload();
        }
    }
}