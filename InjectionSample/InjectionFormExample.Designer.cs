namespace SampleSQLInjectionPostgres
{
    partial class InjectionFormExample
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox TextStudent;
        private System.Windows.Forms.TextBox TextSubject;
        private System.Windows.Forms.TextBox TextTeacher;
        private System.Windows.Forms.TextBox TextDateOfExam;
        private System.Windows.Forms.TextBox TextMark;
        private System.Windows.Forms.Button buttonDangerous;
        private System.Windows.Forms.Button buttonSafe;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.TextStudent = new System.Windows.Forms.TextBox();
            this.TextSubject = new System.Windows.Forms.TextBox();
            this.TextTeacher = new System.Windows.Forms.TextBox();
            this.TextDateOfExam = new System.Windows.Forms.TextBox();
            this.TextMark = new System.Windows.Forms.TextBox();
            this.buttonDangerous = new System.Windows.Forms.Button();
            this.buttonSafe = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TextStudent
            // 
            this.TextStudent.Location = new System.Drawing.Point(20, 20);
            this.TextStudent.Name = "TextStudent";
            this.TextStudent.Size = new System.Drawing.Size(200, 22);
            this.TextStudent.TabIndex = 0;
            this.TextStudent.Text = "Студент (int)";
            // 
            // TextSubject
            // 
            this.TextSubject.Location = new System.Drawing.Point(20, 60);
            this.TextSubject.Name = "TextSubject";
            this.TextSubject.Size = new System.Drawing.Size(200, 22);
            this.TextSubject.TabIndex = 1;
            this.TextSubject.Text = "Предмет (int)";
            // 
            // TextTeacher
            // 
            this.TextTeacher.Location = new System.Drawing.Point(20, 100);
            this.TextTeacher.Name = "TextTeacher";
            this.TextTeacher.Size = new System.Drawing.Size(200, 22);
            this.TextTeacher.TabIndex = 2;
            this.TextTeacher.Text = "Преподаватель (int)";
            // 
            // TextDateOfExam
            // 
            this.TextDateOfExam.Location = new System.Drawing.Point(20, 140);
            this.TextDateOfExam.Name = "TextDateOfExam";
            this.TextDateOfExam.Size = new System.Drawing.Size(200, 22);
            this.TextDateOfExam.TabIndex = 3;
            this.TextDateOfExam.Text = "Дата (YYYY-MM-DD)";
            // 
            // TextMark
            // 
            this.TextMark.Location = new System.Drawing.Point(20, 180);
            this.TextMark.Name = "TextMark";
            this.TextMark.Size = new System.Drawing.Size(200, 22);
            this.TextMark.TabIndex = 4;
            this.TextMark.Text = "Оценка (2–5)";
            // 
            // buttonDangerous
            // 
            this.buttonDangerous.Location = new System.Drawing.Point(250, 30);
            this.buttonDangerous.Name = "buttonDangerous";
            this.buttonDangerous.Size = new System.Drawing.Size(150, 40);
            this.buttonDangerous.TabIndex = 5;
            this.buttonDangerous.Text = "Опасно";
            this.buttonDangerous.UseVisualStyleBackColor = true;
            this.buttonDangerous.Click += new System.EventHandler(this.buttonDangerous_Click);
            // 
            // buttonSafe
            // 
            this.buttonSafe.Location = new System.Drawing.Point(250, 90);
            this.buttonSafe.Name = "buttonSafe";
            this.buttonSafe.Size = new System.Drawing.Size(150, 40);
            this.buttonSafe.TabIndex = 6;
            this.buttonSafe.Text = "Безопасно";
            this.buttonSafe.UseVisualStyleBackColor = true;
            this.buttonSafe.Click += new System.EventHandler(this.buttonSafe_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(430, 230);
            this.Controls.Add(this.TextStudent);
            this.Controls.Add(this.TextSubject);
            this.Controls.Add(this.TextTeacher);
            this.Controls.Add(this.TextDateOfExam);
            this.Controls.Add(this.TextMark);
            this.Controls.Add(this.buttonDangerous);
            this.Controls.Add(this.buttonSafe);
            this.Name = "Form1";
            this.Text = "Пример SQL-инъекций (PostgreSQL)";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
