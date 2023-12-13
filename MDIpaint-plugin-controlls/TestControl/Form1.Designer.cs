namespace TestControl
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.numberBox1 = new ControlsLab.NumberBox(this.components);
            this.filePathSelect1 = new ControlsLab.FilePathSelect();
            this.SuspendLayout();
            // 
            // numberBox1
            // 
            this.numberBox1.ForeColor = System.Drawing.Color.Red;
            this.numberBox1.Location = new System.Drawing.Point(25, 32);
            this.numberBox1.Name = "numberBox1";
            this.numberBox1.Size = new System.Drawing.Size(100, 20);
            this.numberBox1.TabIndex = 0;
            // 
            // filePathSelect1
            // 
            this.filePathSelect1.FileName = "";
            this.filePathSelect1.Location = new System.Drawing.Point(35, 100);
            this.filePathSelect1.Name = "filePathSelect1";
            this.filePathSelect1.Size = new System.Drawing.Size(394, 29);
            this.filePathSelect1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.filePathSelect1);
            this.Controls.Add(this.numberBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ControlsLab.NumberBox numberBox1;
        private ControlsLab.FilePathSelect filePathSelect1;
    }
}

