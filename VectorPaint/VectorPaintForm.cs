using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VectorPaint.Entities;
using VectorPaint.Methods;

namespace VectorPaint
{
    public partial class VectorPaintForm : Form
    {
        public VectorPaintForm()
        {
            InitializeComponent();
        }

        // 엔티티 큐
        private List<EntityObject> entity = new List<EntityObject>();

        private LwPolyline tempPolyline = new LwPolyline();

        // 좌표정보
        private Vector3 currentPosition;
        private Vector3 firstPoint;
        private Vector3 secondPoint;

        // 0:점, 1:선, 2:원, 3:타원, 4:사각형, 5: 삼각형
        private int DrawIndex = -1;
        private bool active_drawing = false;
        private int ClickNum = 1;
        private int direction;
        private int sideQty = 3;
        private int inscribed = 1;

        // drawing 위에서 마우스가 움직일 때 이벤트
        private void drawing_MouseMove(object sender, MouseEventArgs e)
        {
            currentPosition = PointToCartesian(e.Location);
            label1.Text = string.Format("{0}, {1}", e.Location.X, e.Location.Y);
            label2.Text = string.Format("{0}, {1}", currentPosition.X, currentPosition.Y);
            drawing.Refresh();
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
        private Vector3 PointToCartesian(System.Drawing.Point point)
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
            if (e.Button == MouseButtons.Left)
            {
                if (active_drawing)
                {
                    switch (DrawIndex)
                    {
                        // point (점)
                        case 0:
                            // 포인트 큐에 좌표 엔티티 추가
                            entity.Add(new Entities.Point(currentPosition));
                            break;
                        // line (선)
                        case 1:
                            switch (ClickNum)
                            {
                                case 1:
                                    firstPoint = currentPosition;
                                    entity.Add(new Entities.Point(currentPosition));
                                    ClickNum++;
                                    break;
                                case 2:
                                    entity.Add(new Entities.Line(firstPoint, currentPosition));
                                    entity.Add(new Entities.Point(currentPosition));
                                    firstPoint = currentPosition;
                                    ClickNum = 1;
                                    break;
                            }
                            break;
                        // circle (원)
                        case 2:
                            switch (ClickNum)
                            {
                                case 1:
                                    firstPoint = currentPosition;
                                    ClickNum++;
                                    break;
                                case 2:
                                    double r = firstPoint.DistanceFrom(currentPosition);
                                    entity.Add(new Entities.Circle(firstPoint, r));
                                    ClickNum = 1;
                                    break;
                            }
                            break;
                        // Ellipse (타원)
                        case 3:
                            switch (ClickNum)
                            {
                                case 1:
                                    firstPoint = currentPosition;
                                    ClickNum++;
                                    break;
                                case 2:
                                    secondPoint = currentPosition;
                                    ClickNum++;
                                    break;
                                case 3:
                                    Entities.Ellipse ellipse = Methods.Method.GetEllipse(firstPoint, secondPoint, currentPosition);
                                    entity.Add(ellipse);
                                    ClickNum = 1;
                                    break;
                            }
                            break;
                        // Rectangle (사각형)
                        case 4:
                            switch (ClickNum)
                            {
                                case 1:
                                    firstPoint = currentPosition;
                                    ClickNum++;
                                    break;
                                case 2:
                                    entity.Add(Methods.Method.PointToRect(firstPoint, currentPosition, out direction));
                                    ClickNum = 1;
                                    break;
                            }
                            break;
                        // Polygon (삼각형)
                        case 5:
                            switch (ClickNum)
                            {
                                case 1:
                                    firstPoint = currentPosition;
                                    ClickNum++;
                                    break;
                                case 2:
                                    entity.Add(Methods.Method.GetPolygon(firstPoint, currentPosition, sideQty, inscribed));
                                    ClickNum = 1;
                                    break;
                            }
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

            // 펜 객체 생성
            Pen pen = new Pen(Color.Blue, 0.1f);
            Pen extpen = new Pen(Color.Gray, 0.1f);
            extpen.DashPattern = new float[] { 1.0f, 2.0f };

            // 엔티티 그리기
            if (entity.Count > 0)
            {
                // 포인트 큐의 좌표 엔티티 추출 반복
                foreach (EntityObject entities in entity)
                {
                    // 엔티티 그리기
                    e.Graphics.DrawEntity(pen, entities);
                }
            }

            // 라인 그리기 (확장)
            switch (DrawIndex)
            {
                // 선
                case 1:
                    if (ClickNum == 2)
                    {
                        Entities.Line line = new Entities.Line(firstPoint, currentPosition);
                        e.Graphics.DrawLine(extpen, line);
                    }
                    break;
                // 원
                case 2:
                    if (ClickNum == 2)
                    {
                        Entities.Line line = new Entities.Line(firstPoint, currentPosition);
                        e.Graphics.DrawLine(extpen, line);
                        double r = firstPoint.DistanceFrom(currentPosition);
                        Entities.Circle circle = new Entities.Circle(firstPoint, r);
                        e.Graphics.DrawCircle(extpen, circle);
                    }
                    break;
                // 타원
                case 3:
                    switch (ClickNum)
                    {
                        case 2:
                            Entities.Line line = new Entities.Line(firstPoint, currentPosition);
                            e.Graphics.DrawLine(extpen, line);
                            break;
                        case 3:
                            Entities.Line line1 = new Entities.Line(firstPoint, currentPosition);
                            e.Graphics.DrawLine(extpen, line1);
                            Entities.Ellipse elp = Methods.Method.GetEllipse(firstPoint, secondPoint, currentPosition);
                            e.Graphics.DrawEllipse(extpen, elp);
                            break;
                    }
                    break;
                // 사각형
                case 4:
                    if (ClickNum == 2)
                    {
                        LwPolyline lw = Methods.Method.PointToRect(firstPoint, currentPosition, out direction);
                        e.Graphics.DrawLwPolyline(extpen, lw);
                    }
                    break;
                // 폴리곤
                case 5:
                    if (ClickNum == 2)
                    {
                        Entities.Line line = new Entities.Line(firstPoint, currentPosition);
                        e.Graphics.DrawLine(extpen, line);
                        LwPolyline lw = Method.GetPolygon(firstPoint, currentPosition, sideQty, inscribed);
                        e.Graphics.DrawLwPolyline(extpen, lw);
                    }
                    break;
            }
        }

        private void LwPolylineCloseStatus(int index)
        {
            List<LwPolylineVertex> vertexes = new List<LwPolylineVertex>();
            foreach (LwPolylineVertex lw in tempPolyline.Vertexes)
                vertexes.Add(lw);
            if (vertexes.Count > 1)
            {
                switch (index)
                {
                    case 1:
                        if (vertexes.Count > 2)
                            entity.Add(new LwPolyline(vertexes, true));
                        else
                            entity.Add(new LwPolyline(vertexes, false));
                        break;
                    case 2:
                        entity.Add(new LwPolyline(vertexes, false));
                        break;
                }
            }
            tempPolyline.Vertexes.Clear();
        }

        private void CancelAll(int index = 1)
        {
            DrawIndex = -1;
            active_drawing = false;
            drawing.Cursor = Cursors.Default;
            ClickNum = 1;
            LwPolylineCloseStatus(index);
        }

        // Point 버튼 클릭 시 이벤트
        // 커서를 변경하며 그리기 모드로 변경
        private void pointBtn_Click(object sender, EventArgs e)
        {
            DrawIndex = 0;
            active_drawing = true;
            drawing.Cursor = Cursors.Cross;
        }

        private void lineBtn_Click(object sender, EventArgs e)
        {
            DrawIndex = 1;
            active_drawing = true;
            drawing.Cursor = Cursors.Cross;
        }

        private void circleBtn_Click(object sender, EventArgs e)
        {
            DrawIndex = 2;
            active_drawing = true;
            drawing.Cursor = Cursors.Cross;
        }

        private void ellipseBtn_Click(object sender, EventArgs e)
        {
            DrawIndex = 3;
            active_drawing = true;
            drawing.Cursor = Cursors.Cross;
        }

        private void RectangleBtn_Click(object sender, EventArgs e)
        {
            DrawIndex = 4;
            active_drawing = true;
            drawing.Cursor = Cursors.Cross;
        }

        private void polygonBtn_Click(object sender, EventArgs e)
        {
            DrawIndex = 5;
            active_drawing = true;
            drawing.Cursor = Cursors.Cross;
        }

        private void allClearBtn_Click(object sender, EventArgs e)
        {
            // Clear all lists containing shapes
            entity.Clear();

            // Refresh the drawing area to reflect the changes
            drawing.Refresh();
        }
    }
}
