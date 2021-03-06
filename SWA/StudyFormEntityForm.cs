﻿using MetroFramework.Forms;
using SWA.Entities;
using System;
using System.Data.Entity;
using System.Windows.Forms;

namespace SWA
{
    public partial class StudyFormEntityForm : MetroForm
    {
        public StudentForm sf;
        public int StudyFormId = 0;
        StudyForm studyform = new StudyForm();
        string MesAccept = String.Empty;

        public StudyFormEntityForm()
        {
            InitializeComponent();
            
        }

        private void StudyFormEntityForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (StudyFormId == 0)
                {
                    this.Text = "Добавление формы обучения";
                    MesAccept = "Запись успешно добавлена";
                }
                else
                {
                    this.Text = "Редактирование формы обучения";
                    using (ApplicationDbContext context = new ApplicationDbContext())
                    {
                        studyform = context.StudyForms.Find(StudyFormId);
                        metroTextBoxStudyFormName.Text = studyform.StudyFormName;
                    }
                    MesAccept = "Запись успешно изменена";
                }
            }
            catch(Exception gg)
            {
                MessageBox.Show(gg.Message, "SWA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private async void metroTileAccept_Click(object sender, EventArgs e)
        {
            try
            {
                if(String.IsNullOrEmpty(metroTextBoxStudyFormName.Text))
                {
                    throw new Exception("Введите название формы обучения");
                }

                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    if (StudyFormId == 0)
                    {
                        studyform.StudyFormName = metroTextBoxStudyFormName.Text;
                        context.StudyForms.Add(studyform);
                    }
                    else
                    {
                        studyform.StudyFormName = metroTextBoxStudyFormName.Text;
                        context.Entry(studyform).State = EntityState.Modified;
                    }

                    await context.SaveChangesAsync();
                    sf.StudyFormsTable(await context.StudyForms.ToListAsync());
                }
                MessageBox.Show(MesAccept, "SWA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();                
            }
            catch (Exception gg)
            {
                MessageBox.Show(gg.Message, "SWA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

       
    }
}
