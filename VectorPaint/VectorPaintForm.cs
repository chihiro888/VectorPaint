﻿using System;
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

        // 엔티티 큐
        private List<Entities.Point> points = new List<Entities.Point>();
        private List<Entities.Line> lines = new List<Entities.Line>();
        private List<Entities.Circle> circles = new List<Entities.Circle>();

        // 좌표정보
        private Vector3 currentPosition;
        private Vector3 firstPoint;

        // 0:점, 1:선
        private int DrawIndex = -1;
        private bool active_drawing = false;
        private int ClickNum = 1;

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
                        // point
                        case 0:
                            // 포인트 큐에 좌표 엔티티 추가
                            points.Add(new Entities.Point(currentPosition));
                            break;
                        // line
                        case 1:
                            switch(ClickNum)
                            {
                                case 1:
                                    firstPoint = currentPosition;
                                    points.Add(new Entities.Point(currentPosition));
                                    ClickNum++;
                                    break;
                                case 2:
                                    lines.Add(new Entities.Line(firstPoint, currentPosition));
                                    points.Add(new Entities.Point(currentPosition));
                                    firstPoint = currentPosition;
                                    ClickNum = 1;
                                    break;
                            }
                            break;
                        // circle
                        case 2:
                            switch(ClickNum)
                            {
                                case 1:
                                    firstPoint = currentPosition;
                                    ClickNum++;
                                    break;
                                case 2:
                                    double r = firstPoint.DistanceFrom(currentPosition);
                                    circles.Add(new Entities.Circle(firstPoint, r));
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

            // 점 그리기
            if (points.Count > 0)
            {
                // 포인트 큐의 좌표 엔티티 추출 반복
                foreach(Entities.Point p in points)
                {
                    // 점 그리기
                    e.Graphics.DrawPoint(new Pen(Color.Red, 0), p);
                }
            }

            // 라인 그리기
            if (lines.Count > 0)
            {
                // 포인트 큐의 좌표 엔티티 추출 반복
                foreach (Entities.Line l in lines)
                {
                    // 라인 그리기
                    e.Graphics.DrawLine(pen, l);
                }
            }

            // 라인 그리기
            if (circles.Count > 0)
            {
                // 포인트 큐의 좌표 엔티티 추출 반복
                foreach (Entities.Circle c in circles)
                {
                    // 원 그리기
                    e.Graphics.DrawCircle(pen, c);
                }
            }

            // 라인 그리기 (확장)
            switch (DrawIndex)
            {
                case 1:
                    if (ClickNum == 2)
                    {
                        Entities.Line line = new Entities.Line(firstPoint, currentPosition);
                        e.Graphics.DrawLine(extpen, line);
                    }
                    break;
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
    }
}