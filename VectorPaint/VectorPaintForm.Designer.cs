namespace VectorPaint
{
    partial class VectorPaintForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.drawing = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pointBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lineBtn = new System.Windows.Forms.Button();
            this.circleBtn = new System.Windows.Forms.Button();
            this.ellipseBtn = new System.Windows.Forms.Button();
            this.RectangleBtn = new System.Windows.Forms.Button();
            this.allClearBtn = new System.Windows.Forms.Button();
            this.polygonBtn = new System.Windows.Forms.Button();
            this.zoomInBtn = new System.Windows.Forms.Button();
            this.zoomOutBtn = new System.Windows.Forms.Button();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.arcBtn = new System.Windows.Forms.Button();
            this.EditBtn = new System.Windows.Forms.Button();
            this.copyBtn = new System.Windows.Forms.Button();
            this.moveBtn = new System.Windows.Forms.Button();
            this.rotateBtn = new System.Windows.Forms.Button();
            this.scaleBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.drawing)).BeginInit();
            this.SuspendLayout();
            // 
            // drawing
            // 
            this.drawing.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.drawing.BackColor = System.Drawing.SystemColors.Window;
            this.drawing.Location = new System.Drawing.Point(12, 12);
            this.drawing.Name = "drawing";
            this.drawing.Size = new System.Drawing.Size(606, 337);
            this.drawing.TabIndex = 0;
            this.drawing.TabStop = false;
            this.drawing.Paint += new System.Windows.Forms.PaintEventHandler(this.drawing_Paint);
            this.drawing.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawing_MouseDown);
            this.drawing.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawing_MouseMove);
            this.drawing.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.drawing_MouseWheel);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 408);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // pointBtn
            // 
            this.pointBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pointBtn.Location = new System.Drawing.Point(663, 12);
            this.pointBtn.Name = "pointBtn";
            this.pointBtn.Size = new System.Drawing.Size(101, 23);
            this.pointBtn.TabIndex = 2;
            this.pointBtn.Text = "Point";
            this.pointBtn.UseVisualStyleBackColor = true;
            this.pointBtn.Click += new System.EventHandler(this.pointBtn_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 438);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "label2";
            // 
            // lineBtn
            // 
            this.lineBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lineBtn.Location = new System.Drawing.Point(664, 42);
            this.lineBtn.Name = "lineBtn";
            this.lineBtn.Size = new System.Drawing.Size(101, 23);
            this.lineBtn.TabIndex = 4;
            this.lineBtn.Text = "Line";
            this.lineBtn.UseVisualStyleBackColor = true;
            this.lineBtn.Click += new System.EventHandler(this.lineBtn_Click);
            // 
            // circleBtn
            // 
            this.circleBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.circleBtn.Location = new System.Drawing.Point(664, 71);
            this.circleBtn.Name = "circleBtn";
            this.circleBtn.Size = new System.Drawing.Size(101, 23);
            this.circleBtn.TabIndex = 5;
            this.circleBtn.Text = "Circle";
            this.circleBtn.UseVisualStyleBackColor = true;
            this.circleBtn.Click += new System.EventHandler(this.circleBtn_Click);
            // 
            // ellipseBtn
            // 
            this.ellipseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ellipseBtn.Location = new System.Drawing.Point(664, 100);
            this.ellipseBtn.Name = "ellipseBtn";
            this.ellipseBtn.Size = new System.Drawing.Size(101, 23);
            this.ellipseBtn.TabIndex = 6;
            this.ellipseBtn.Text = "Ellipse";
            this.ellipseBtn.UseVisualStyleBackColor = true;
            this.ellipseBtn.Click += new System.EventHandler(this.ellipseBtn_Click);
            // 
            // RectangleBtn
            // 
            this.RectangleBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RectangleBtn.Location = new System.Drawing.Point(664, 130);
            this.RectangleBtn.Name = "RectangleBtn";
            this.RectangleBtn.Size = new System.Drawing.Size(101, 23);
            this.RectangleBtn.TabIndex = 7;
            this.RectangleBtn.Text = "Rectangle";
            this.RectangleBtn.UseVisualStyleBackColor = true;
            this.RectangleBtn.Click += new System.EventHandler(this.RectangleBtn_Click);
            // 
            // allClearBtn
            // 
            this.allClearBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.allClearBtn.Location = new System.Drawing.Point(663, 326);
            this.allClearBtn.Name = "allClearBtn";
            this.allClearBtn.Size = new System.Drawing.Size(101, 23);
            this.allClearBtn.TabIndex = 8;
            this.allClearBtn.Text = "All Clear";
            this.allClearBtn.UseVisualStyleBackColor = true;
            this.allClearBtn.Click += new System.EventHandler(this.allClearBtn_Click);
            // 
            // polygonBtn
            // 
            this.polygonBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.polygonBtn.Location = new System.Drawing.Point(664, 159);
            this.polygonBtn.Name = "polygonBtn";
            this.polygonBtn.Size = new System.Drawing.Size(101, 23);
            this.polygonBtn.TabIndex = 9;
            this.polygonBtn.Text = "Triangle";
            this.polygonBtn.UseVisualStyleBackColor = true;
            this.polygonBtn.Click += new System.EventHandler(this.polygonBtn_Click);
            // 
            // zoomInBtn
            // 
            this.zoomInBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomInBtn.Location = new System.Drawing.Point(663, 256);
            this.zoomInBtn.Name = "zoomInBtn";
            this.zoomInBtn.Size = new System.Drawing.Size(101, 23);
            this.zoomInBtn.TabIndex = 10;
            this.zoomInBtn.Text = "Zoom In";
            this.zoomInBtn.UseVisualStyleBackColor = true;
            this.zoomInBtn.Click += new System.EventHandler(this.zoomInBtn_Click);
            // 
            // zoomOutBtn
            // 
            this.zoomOutBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomOutBtn.Location = new System.Drawing.Point(663, 285);
            this.zoomOutBtn.Name = "zoomOutBtn";
            this.zoomOutBtn.Size = new System.Drawing.Size(101, 23);
            this.zoomOutBtn.TabIndex = 11;
            this.zoomOutBtn.Text = "Zoom Out";
            this.zoomOutBtn.UseVisualStyleBackColor = true;
            this.zoomOutBtn.Click += new System.EventHandler(this.zoomOutBtn_Click);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(12, 352);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(606, 17);
            this.hScrollBar1.TabIndex = 13;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(621, 14);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(20, 335);
            this.vScrollBar1.TabIndex = 14;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // arcBtn
            // 
            this.arcBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.arcBtn.Location = new System.Drawing.Point(664, 188);
            this.arcBtn.Name = "arcBtn";
            this.arcBtn.Size = new System.Drawing.Size(101, 23);
            this.arcBtn.TabIndex = 15;
            this.arcBtn.Text = "Arc";
            this.arcBtn.UseVisualStyleBackColor = true;
            this.arcBtn.Click += new System.EventHandler(this.arcBtn_Click);
            // 
            // EditBtn
            // 
            this.EditBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EditBtn.Location = new System.Drawing.Point(375, 372);
            this.EditBtn.Name = "EditBtn";
            this.EditBtn.Size = new System.Drawing.Size(101, 23);
            this.EditBtn.TabIndex = 16;
            this.EditBtn.Text = "Edit";
            this.EditBtn.UseVisualStyleBackColor = true;
            this.EditBtn.Click += new System.EventHandler(this.EditBtn_Click);
            // 
            // copyBtn
            // 
            this.copyBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.copyBtn.Location = new System.Drawing.Point(375, 401);
            this.copyBtn.Name = "copyBtn";
            this.copyBtn.Size = new System.Drawing.Size(101, 23);
            this.copyBtn.TabIndex = 17;
            this.copyBtn.Text = "Copy";
            this.copyBtn.UseVisualStyleBackColor = true;
            this.copyBtn.Click += new System.EventHandler(this.copyBtn_Click);
            // 
            // moveBtn
            // 
            this.moveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.moveBtn.Location = new System.Drawing.Point(482, 401);
            this.moveBtn.Name = "moveBtn";
            this.moveBtn.Size = new System.Drawing.Size(101, 23);
            this.moveBtn.TabIndex = 18;
            this.moveBtn.Text = "Move";
            this.moveBtn.UseVisualStyleBackColor = true;
            this.moveBtn.Click += new System.EventHandler(this.moveBtn_Click);
            // 
            // rotateBtn
            // 
            this.rotateBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rotateBtn.Location = new System.Drawing.Point(375, 430);
            this.rotateBtn.Name = "rotateBtn";
            this.rotateBtn.Size = new System.Drawing.Size(101, 23);
            this.rotateBtn.TabIndex = 19;
            this.rotateBtn.Text = "Rotate";
            this.rotateBtn.UseVisualStyleBackColor = true;
            this.rotateBtn.Click += new System.EventHandler(this.rotateBtn_Click);
            // 
            // scaleBtn
            // 
            this.scaleBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scaleBtn.Location = new System.Drawing.Point(482, 430);
            this.scaleBtn.Name = "scaleBtn";
            this.scaleBtn.Size = new System.Drawing.Size(101, 23);
            this.scaleBtn.TabIndex = 20;
            this.scaleBtn.Text = "Scale";
            this.scaleBtn.UseVisualStyleBackColor = true;
            this.scaleBtn.Click += new System.EventHandler(this.scaleBtn_Click);
            // 
            // VectorPaintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 466);
            this.Controls.Add(this.scaleBtn);
            this.Controls.Add(this.rotateBtn);
            this.Controls.Add(this.moveBtn);
            this.Controls.Add(this.copyBtn);
            this.Controls.Add(this.EditBtn);
            this.Controls.Add(this.arcBtn);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.zoomOutBtn);
            this.Controls.Add(this.zoomInBtn);
            this.Controls.Add(this.polygonBtn);
            this.Controls.Add(this.allClearBtn);
            this.Controls.Add(this.RectangleBtn);
            this.Controls.Add(this.ellipseBtn);
            this.Controls.Add(this.circleBtn);
            this.Controls.Add(this.lineBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pointBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.drawing);
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "VectorPaintForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.drawing)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox drawing;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button pointBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button lineBtn;
        private System.Windows.Forms.Button circleBtn;
        private System.Windows.Forms.Button ellipseBtn;
        private System.Windows.Forms.Button RectangleBtn;
        private System.Windows.Forms.Button allClearBtn;
        private System.Windows.Forms.Button polygonBtn;
        private System.Windows.Forms.Button zoomInBtn;
        private System.Windows.Forms.Button zoomOutBtn;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.Button arcBtn;
        private System.Windows.Forms.Button EditBtn;
        private System.Windows.Forms.Button copyBtn;
        private System.Windows.Forms.Button moveBtn;
        private System.Windows.Forms.Button rotateBtn;
        private System.Windows.Forms.Button scaleBtn;
    }
}

