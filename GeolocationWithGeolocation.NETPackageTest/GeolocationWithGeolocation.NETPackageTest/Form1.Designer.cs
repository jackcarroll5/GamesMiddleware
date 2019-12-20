namespace GeolocationWithGeolocation.NETPackageTest
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.lbl1 = new System.Windows.Forms.Label();
            this.lblLocationPosition = new System.Windows.Forms.Label();
            this.btnLocationRetrieval = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1.Location = new System.Drawing.Point(259, 30);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(229, 17);
            this.lbl1.TabIndex = 0;
            this.lbl1.Text = "Retrieving the current location";
            // 
            // lblLocationPosition
            // 
            this.lblLocationPosition.AutoSize = true;
            this.lblLocationPosition.Location = new System.Drawing.Point(259, 79);
            this.lblLocationPosition.Name = "lblLocationPosition";
            this.lblLocationPosition.Size = new System.Drawing.Size(0, 17);
            this.lblLocationPosition.TabIndex = 1;
            // 
            // btnLocationRetrieval
            // 
            this.btnLocationRetrieval.Location = new System.Drawing.Point(320, 343);
            this.btnLocationRetrieval.Name = "btnLocationRetrieval";
            this.btnLocationRetrieval.Size = new System.Drawing.Size(124, 60);
            this.btnLocationRetrieval.TabIndex = 2;
            this.btnLocationRetrieval.Text = "Get Location";
            this.btnLocationRetrieval.UseVisualStyleBackColor = true;
            this.btnLocationRetrieval.Click += new System.EventHandler(this.btnLocationRetrieval_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLocationRetrieval);
            this.Controls.Add(this.lblLocationPosition);
            this.Controls.Add(this.lbl1);
            this.Name = "Form1";
            this.Text = "Geolocation Position Test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lblLocationPosition;
        private System.Windows.Forms.Button btnLocationRetrieval;
    }
}

