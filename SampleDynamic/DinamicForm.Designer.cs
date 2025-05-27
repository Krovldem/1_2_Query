namespace SampleDynamic
{
    partial class ListOf
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм

        private void InitializeComponent()
        {
            this.btnShow = new System.Windows.Forms.Button();
            this.txtParam = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnShow
            // 
            this.btnShow.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnShow.Location = new System.Drawing.Point(58, 289);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(547, 45);
            this.btnShow.TabIndex = 0;
            this.btnShow.Text = "Выполнить";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // txtParam
            // 
            this.txtParam.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtParam.Location = new System.Drawing.Point(58, 46);
            this.txtParam.Name = "txtParam";
            this.txtParam.Size = new System.Drawing.Size(547, 34);
            this.txtParam.TabIndex = 1;
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 22;
            this.listBox1.Location = new System.Drawing.Point(58, 117);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(547, 114);
            this.listBox1.TabIndex = 2;
            // 
            // ListOf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 450);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.txtParam);
            this.Controls.Add(this.btnShow);
            this.Name = "ListOf";
            this.Text = "Динамический запрос к Сессии";
            this.Load += new System.EventHandler(this.ListOf_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.TextBox txtParam;
        private System.Windows.Forms.ListBox listBox1;
    }
}
