using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VectorPaint
{
    public partial class VectorPaintForm : Form
    {
        public VectorPaintForm()
        {
            InitializeComponent();
        }

        private List<Entities.Point> points = new List<Entities.Point>();
        private Vector3 currentPosition;
        private int DrawIndex = -1;
        private bool active_drawing = false;

        private void drawing_MouseMove(object sender, MouseEventArgs e)
        {
            currentPosition = PointToCartesian(e.Location);
            label1.Text = string.Format("{0}, {1}", e.Location.X, e.Location.Y);
            label2.Text = string.Format("{0}, {1}", currentPosition.X, currentPosition.Y);
        }

        // DPI값
        private float DPI
        {
            get
            {
                using (var g = CreateGraphics())
                    return g.DpiX;
            }
        }

        // 화면 좌표를 카테시안 좌표로 변환
        private Vector3 PointToCartesian(Point point)
        {
            return new Vector3(Pixcel_to_Mn(point.X), Pixcel_to_Mn(drawing.Height - point.Y));
        }

        // 화면 픽셀 값을 밀리미터 단위로 변환
        private float Pixcel_to_Mn(float pixel)
        {
            return pixel * 25.4f / DPI;
        }

        // 마우스 클릭 시 이벤트
        private void drawing_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                if(active_drawing)
                {
                    switch(DrawIndex)
                    {
                        case 0:
                            // 포인트 큐에 좌표 엔티티 추가
                            points.Add(new Entities.Point(currentPosition));
                            break;
                    }
                    drawing.Refresh();
                }
            }
        }

        private void drawing_Paint(object sender, PaintEventArgs e)
        {
            // 화면 높이를 밀리미터 단위로 변환하여 설정
            e.Graphics.SetParameters(Pixcel_to_Mn(drawing.Height));

            // 포인트 큐가 1건 이상인 경우
            if (points.Count > 0)
            {
                // 포인트 큐의 좌표 엔티티 추출 반복
                foreach(Entities.Point p in points)
                {
                    // 점 그리기
                    e.Graphics.DrawPoint(new Pen(Color.Red, 0), p);
                }
            }
        }

        // Point 버튼 클릭 시 이벤트
        // 커서를 변경하며 그리기 모드로 변경
        private void pointBtn_Click(object sender, EventArgs e)
        {
            DrawIndex = 0;
            active_drawing = true;
            drawing.Cursor = Cursors.Cross;
        }
    }
}
