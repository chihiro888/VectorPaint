using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        private List<EntityObject> entities = new List<EntityObject>();

        private LwPolyline tempPolyline = new LwPolyline();

        // 좌표정보
        private Vector3 currentPosition;
        private Vector3 firstPoint;
        private Vector3 secondPoint;

        // system point
        private System.Drawing.Point firstCorner;

        // int
        // 0:점, 1:선, 2:원, 3:타원, 4:사각형, 5: 삼각형
        private int DrawIndex = -1;
        private int ModifyIndex = -1;
        private int ClickNum = 1;
        private int zoomClick = 1;
        private int direction;
        private int sideQty = 3;
        private int inscribed = 1;
        private int Modify1Index = -1;
        private int segmentIndex = -1;

        // float
        private float XScroll;
        private float YScroll;
        private float ScaleFactor = 1.0f;
        private float x1, x2, y1, y2;
        private float edit_cursorSize = 2.5f;
        private float draw_cursorSize = 5.0f;

        // bool
        private bool active_drawing = false;
        private bool active_zoom = false;
        private bool active_modify = false;
        private bool active_selection = true;

        // base
        private SizeF drawingSize = new SizeF(297, 210);

        // #035 - Index of entities
        private PointF[] CursorRect(Vector3 mousePosition)
        {
            float l = edit_cursorSize * 0.5f;
            float x = mousePosition.ToPointF.X;
            float y = mousePosition.ToPointF.Y;

            return new PointF[]
            {
                new PointF(x-1, y-1),
                new PointF(x+1, y-1),
                new PointF(x+1, y+1),
                new PointF(x-1, y+1)
            };
        }

        // #001- Coordinate system
        // DPI값
        private float DPI
        {
            get
            {
                using (var g = CreateGraphics())
                    return g.DpiX;
            }
        }

        // #001- Coordinate system
        // 화면 좌표를 카테시안 좌표로 변환
        private Vector3 PointToCartesian(System.Drawing.Point point)
        {
            return new Vector3(Pixel_to_Mn(point.X + XScroll) / ScaleFactor, Pixel_to_Mn(drawing.Height - point.Y - YScroll) / ScaleFactor);
        }

        // #001- Coordinate system
        // 화면 픽셀 값을 밀리미터 단위로 변환
        private float Pixel_to_Mn(float pixel)
        {
            return pixel * 25.4f / DPI;
        }

        // #001- Coordinate system
        // drawing 위에서 마우스가 움직일 때 이벤트
        private void drawing_MouseMove(object sender, MouseEventArgs e)
        {
            currentPosition = PointToCartesian(e.Location);
            label1.Text = string.Format("{0}, {1}", e.Location.X, e.Location.Y);
            label2.Text = string.Format("{0}, {1}", currentPosition.X, currentPosition.Y);

            // #025 - mouse wheel event
            x1 = e.Location.X;
            x2 = drawing.ClientSize.Width - x1;
            y1 = e.Location.Y;
            y2 = drawing.ClientSize.Height - y1;

            drawing.Refresh();
        }

        // #002- vector and draw a point
        // 마우스 클릭 시 이벤트
        private void drawing_MouseDown(object sender, MouseEventArgs e)
        {
            // 마우스 왼쪽 버튼 클릭 시
            if (e.Button == MouseButtons.Left)
            {
                if (active_zoom)
                {
                    switch (zoomClick)
                    {
                        case 1:
                            firstCorner = e.Location;
                            zoomClick++;
                            break;
                        case 2:
                            SetZoomWin(firstCorner, e.Location);
                            active_zoom = false;
                            active_drawing = false;
                            zoomClick = 1;
                            break;
                    }
                }

                if (active_drawing && !active_zoom)
                {
                    switch (DrawIndex)
                    {
                        // // #002- vector and draw a point
                        // point (점)
                        case 0:
                            entities.Add(new Entities.Point(currentPosition));
                            break;
                        // #003 - draw a line
                        // line (선)                        
                        case 1:
                            switch (ClickNum)
                            {
                                case 1:
                                    firstPoint = currentPosition;
                                    // entities.Add(new Entities.Point(currentPosition));
                                    ClickNum++;
                                    break;
                                case 2:
                                    entities.Add(new Entities.Line(firstPoint, currentPosition));
                                    // entities.Add(new Entities.Point(currentPosition));
                                    firstPoint = currentPosition;
                                    ClickNum = 1;
                                    break;
                            }
                            break;
                        // # 004 - draw a circle
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
                                    entities.Add(new Entities.Circle(firstPoint, r));
                                    ClickNum = 1;
                                    break;
                            }
                            break;
                        // #005 - draw an ellipse
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
                                    entities.Add(ellipse);
                                    ClickNum = 1;
                                    break;
                            }
                            break;
                        // #012 - draw a rectangle
                        // Rectangle (사각형)
                        case 4:
                            switch (ClickNum)
                            {
                                case 1:
                                    firstPoint = currentPosition;
                                    ClickNum++;
                                    break;
                                case 2:
                                    entities.Add(Methods.Method.PointToRect(firstPoint, currentPosition, out direction));
                                    ClickNum = 1;
                                    break;
                            }
                            break;
                        // #013 - draw a polygon
                        // Polygon (삼각형)
                        case 5:
                            switch (ClickNum)
                            {
                                case 1:
                                    firstPoint = currentPosition;
                                    ClickNum++;
                                    break;
                                case 2:
                                    entities.Add(Methods.Method.GetPolygon(firstPoint, currentPosition, sideQty, inscribed));
                                    ClickNum = 1;
                                    break;
                            }
                            break;
                        // #008 - draw an arc
                        // Arc (아크)
                        case 6:
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
                                    Arc a = Method.GetArcWith3Points(firstPoint, secondPoint, currentPosition);
                                    entities.Add(a);
                                    ClickNum = 1;
                                    break;
                            }
                            break;
                    }
                }

                if (active_modify)
                {
                    if (active_selection)
                        segmentIndex = Method.GetSegmentIndex(entities, currentPosition, CursorRect(currentPosition), out Vector3 clickPoint);

                    switch (ClickNum)
                    {
                        case 1:
                            firstPoint = currentPosition;
                            ClickNum++;
                            break;
                        case 2:
                            switch (Modify1Index)
                            {
                                // Copy object
                                case 0:
                                    Method.Modify1Selection(Modify1Index, entities, firstPoint, currentPosition);
                                    active_selection = false;
                                    break;
                                // Move object
                                case 1:
                                // Rotate object
                                case 2:
                                // Scale object
                                case 3:
                                    Method.Modify1Selection(Modify1Index, entities, firstPoint, currentPosition);
                                    CancelAll();
                                    active_selection = false;
                                    break;
                            }
                            break;
                    }
                }
                drawing.Refresh();
            }
        }

        // #002- vector and draw a point
        // 마우스로 그림 그릴 시 이벤트
        private void drawing_Paint(object sender, PaintEventArgs e)
        {
            // #010 - scroll bar
            // 화면 높이를 밀리미터 단위로 변환하여 설정
            e.Graphics.SetParameters(XScroll, YScroll, ScaleFactor, Pixel_to_Mn(drawing.Height));

            // 펜 객체 생성
            Pen pen = new Pen(Color.Blue, 0.1f);
            Pen extpen = new Pen(Color.Gray, 0.1f);

            // #024 - pan a drawing (ScaleFactor 추가)
            extpen.DashPattern = new float[] { 1.0f / ScaleFactor, 2.0f / ScaleFactor };

            /*
            foreach(EntityObject entity in entities)
            {
                switch (entity.Type)
                {
                    case EntityType.Line:
                        Vector3 p;
                        foreach (EntityObject entity1 in entities)
                        {
                            switch(entity1.Type)
                            {
                                case EntityType.Ellipse:
                                    List<Vector3> intersection = Method.LineEllipseIntersection(entity as Line, entity1 as Ellipse);
                                    foreach (Vector3 v in intersection)
                                        e.Graphics.DrawPoint(new Pen(Color.Red), new Entities.Point(v));
                                    break;
                                case EntityType.Circle:
                                    intersection = Method.LineCircleIntersection(entity as Line, entity1 as Circle);
                                    foreach (Vector3 v in intersection)
                                        e.Graphics.DrawPoint(new Pen(Color.Red), new Entities.Point(v));
                                    break;
                                case EntityType.Arc:
                                    intersection = Method.LineArcIntersection(entity as Line, entity1 as Arc);
                                    foreach (Vector3 v in intersection)
                                        e.Graphics.DrawPoint(new Pen(Color.Red), new Entities.Point(v));
                                    break;
                            }
                        }
                        break;
                    case EntityType.Ellipse:
                        double d = Method.DistancePointToEllipse(entity as Ellipse, currentPosition, out p);
                        e.Graphics.DrawPoint(new Pen(Color.Red), new Entities.Point(p));
                        e.Graphics.DrawLine(new Pen(Color.Gray, 0), new Line(p, currentPosition));
                        Text = d.ToString();
                        break;
                    case EntityType.Circle:
                        d = Method.DistancePointToCircle(entity as Circle, currentPosition, out p);
                        e.Graphics.DrawPoint(new Pen(Color.Red), new Entities.Point(p));
                        e.Graphics.DrawLine(new Pen(Color.Gray, 0), new Line(p, currentPosition));
                        Text = d.ToString();
                        break;
                    case EntityType.Arc:
                        d = Method.DistancePointToArc(entity as Arc, currentPosition, out p);
                        e.Graphics.DrawPoint(new Pen(Color.Red), new Entities.Point(p));
                        e.Graphics.DrawLine(new Pen(Color.Gray, 0), new Line(p, currentPosition));
                        Text = d.ToString();
                        break;
                    case EntityType.LwPolyline:
                        d = Method.DistancePointToLwPolyline(entity as LwPolyline, currentPosition, out p);
                        e.Graphics.DrawPoint(new Pen(Color.Red), new Entities.Point(p));
                        e.Graphics.DrawLine(new Pen(Color.Gray, 0), new Line(p, currentPosition));
                        Text = d.ToString();
                        break;
                }
            }
            */

            // 엔티티 그리기
            if (entities.Count > 0)
            {
                        
                // 포인트 큐의 좌표 엔티티 추출 반복
                foreach (EntityObject ent in entities)
                {
                    // #022 - distance from a point to a line
                    // TEST
                    /*
                    if (ent is Line)
                    {
                        double d = Method.DistanceFromLine(ent as Line, currentPosition, out Vector3 v);
                        e.Graphics.DrawPoint(new Pen(Color.Red, 1), new Entities.Point(v));
                        Text = d.ToString();
                    }
                    */

                    // 엔티티 그리기
                    e.Graphics.DrawEntity(pen, ent);
                }
            }

            // 라인 그리기 (확장)
            if (active_drawing)
            {
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
                    // 아크
                    case 6:
                        switch (ClickNum)
                        {
                            case 2:
                                Entities.Line line = new Entities.Line(firstPoint, currentPosition);
                                e.Graphics.DrawLine(extpen, line);
                                break;
                            case 3:
                                Arc a = Method.GetArcWith3Points(firstPoint, secondPoint, currentPosition);
                                e.Graphics.DrawArc(extpen, a);
                                break;
                        }
                        break;
                }
            }

            if (active_zoom)
            {
                switch (zoomClick)
                {
                    case 2:
                        LwPolyline rect = Methods.Method.PointToRect(PointToCartesian(firstCorner), currentPosition, out direction);
                        e.Graphics.DrawLwPolyline(new Pen(Color.Red, 0), rect);
                        break;
                }
            }

            if (active_modify)
            {
                switch(ClickNum)
                {
                    case 2:
                        e.Graphics.ExtendedOfModify(extpen, Modify1Index, entities, firstPoint, currentPosition);
                        break;
                }
            }
        }

        // #021 - custom cursor
        // #023 - zoom & crop (파라미터 확장)
        // 커서 활성화
        private void ActiveCursor(int index, float size = 5)
        {
            Cursor cursor = Cursors.Default;
            if (index > 0)
                cursor = new Cursor(Method.SetCursor(index, Mn_to_Pixel(size), Color.Red).GetHicon());
            drawing.Cursor = cursor;
        }

        // #021 - custom cursor
        // 밀리미터 단위를 화면 픽셀 값로 변환
        private float Mn_to_Pixel(float pixel)
        {
            return pixel / 25.4f * DPI;
        }

        // #023 - zoom & crop
        private void SetZoomWin(System.Drawing.Point firstCorner, System.Drawing.Point secondCorner)
        {
            Vector3 p1 = PointToCartesian(firstCorner);
            Vector3 p2 = PointToCartesian(secondCorner);

            float minX = Math.Min(p1.ToPointF.X, p2.ToPointF.X);
            float minY = Math.Min(p1.ToPointF.Y, p2.ToPointF.Y);

            float w = Math.Abs(firstCorner.X - secondCorner.X);
            float h = Math.Abs(firstCorner.Y - secondCorner.Y);

            float width = drawing.ClientSize.Width / w;
            float height = drawing.ClientSize.Height / h;

            float min = Math.Min(width, height);

            ScaleFactor *= min;

            float wl = (drawing.ClientSize.Width - w * min) / 2;
            float hl = (drawing.ClientSize.Height - h * min) / 2;

            XScroll = ScaleFactor * minX - Pixel_to_Mn(wl);
            YScroll = -ScaleFactor * minY - Pixel_to_Mn(hl);

            SetScrollBarValues();
        }

        // #023 - zoom & crop
        private void SetZoomInOut(int index)
        {
            float scale = (index == 0) ? 1 / 1.25f : 1.25f;

            ScaleFactor *= scale;

            float width = drawing.ClientSize.Width * scale;
            float height = drawing.ClientSize.Height * scale;

            float wl = (drawing.ClientSize.Width - width) / 2;
            float hl = (drawing.ClientSize.Height - height) / 2;

            XScroll = XScroll * scale - Pixel_to_Mn(wl);
            YScroll = -YScroll * scale + Pixel_to_Mn(hl);

            SetScrollBarValues();
        }

        // #023 - zoom & crop
        private void ZoomEvents(int index)
        {
            switch (index)
            {
                // zoom in
                case 0:
                // zoom out
                case 1:
                    SetZoomInOut(index);
                    break;
                // zoom window
                case 2:
                    active_zoom = true;
                    ActiveCursor(1);
                    break;
            }
        }

        // #023 - zoom & crop
        private void SetScrollBarValues()
        {
            float width = Math.Max(0, drawingSize.Width * ScaleFactor - Pixel_to_Mn(drawing.ClientSize.Width)) + 50 * ScaleFactor;
            float height = Math.Max(0, drawingSize.Height * ScaleFactor - Pixel_to_Mn(drawing.ClientSize.Height)) + 59 * ScaleFactor;

            // TODO
            hScrollBar1.Maximum = (int)width;
            hScrollBar1.Minimum = -(int)(50 * ScaleFactor);

            vScrollBar1.Maximum = (int)(59 * ScaleFactor);
            vScrollBar1.Minimum = -(int)height;

            try
            {
                hScrollBar1.Minimum = (int)Math.Min(XScroll, hScrollBar1.Minimum);
                hScrollBar1.Maximum = (int)Math.Max(XScroll, hScrollBar1.Maximum);
                vScrollBar1.Minimum = (int)Math.Min(YScroll, vScrollBar1.Minimum);
                vScrollBar1.Maximum = (int)Math.Max(YScroll, vScrollBar1.Maximum);

                hScrollBar1.Value = (int)XScroll;
                vScrollBar1.Value = (int)YScroll;
            }
            catch { }
            drawing.Refresh();
        }

        // #011 - draw a polyline
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
                            entities.Add(new LwPolyline(vertexes, true));
                        else
                            entities.Add(new LwPolyline(vertexes, false));
                        break;
                    case 2:
                        entities.Add(new LwPolyline(vertexes, false));
                        break;
                }
            }
            tempPolyline.Vertexes.Clear();
        }

        // #006 - intersection of two lines
        private void CancelAll(int index = 1)
        {
            DrawIndex = -1;
            active_drawing = false;
            active_selection = true;
            ActiveCursor(0, 0);
            ClickNum = 1;
            LwPolylineCloseStatus(index);
            DeSelectAll();
        }

        // #037 - Copy and move
        private void DeSelectAll()
        {
            foreach (EntityObject entity in entities)
                entity.DeSelect();
            drawing.Refresh();
        }

        // 버튼 클릭 이벤트
        private void pointBtn_Click(object sender, EventArgs e)
        {
            DrawIndex = 0;
            active_drawing = true;
            active_modify = false;
            active_selection = true;
            ActiveCursor(1, draw_cursorSize);
        }

        // 버튼 클릭 이벤트
        private void lineBtn_Click(object sender, EventArgs e)
        {
            DrawIndex = 1;
            active_drawing = true;
            active_modify = false;
            active_selection = true;
            ActiveCursor(1, draw_cursorSize);
        }

        // 버튼 클릭 이벤트
        private void circleBtn_Click(object sender, EventArgs e)
        {
            DrawIndex = 2;
            active_drawing = true;
            active_modify = false;
            active_selection = true;
            ActiveCursor(1, draw_cursorSize);
        }

        // 버튼 클릭 이벤트
        private void ellipseBtn_Click(object sender, EventArgs e)
        {
            DrawIndex = 3;
            active_drawing = true;
            active_modify = false;
            active_selection = true;
            ActiveCursor(1, draw_cursorSize);
        }

        // 버튼 클릭 이벤트
        private void RectangleBtn_Click(object sender, EventArgs e)
        {
            DrawIndex = 4;
            active_drawing = true;
            active_modify = false;
            active_selection = true;
            ActiveCursor(1, draw_cursorSize);
        }

        // 버튼 클릭 이벤트
        private void polygonBtn_Click(object sender, EventArgs e)
        {
            DrawIndex = 5;
            active_drawing = true;
            active_modify = false;
            active_selection = true;
            ActiveCursor(1, draw_cursorSize);
        }

        // 버튼 클릭 이벤트
        private void arcBtn_Click(object sender, EventArgs e)
        {
            DrawIndex = 6;
            active_drawing = true;
            active_modify = false;
            active_selection = true;
            ActiveCursor(1, draw_cursorSize);
        }

        // 버튼 클릭 이벤트
        private void allClearBtn_Click(object sender, EventArgs e)
        {
            // Clear all lists containing shapes
            entities.Clear();

            active_drawing = false;
            active_modify = false;

            // Refresh the drawing area to reflect the changes
            drawing.Refresh();
        }

        // 버튼 클릭 이벤트
        private void zoomOutBtn_Click(object sender, EventArgs e)
        {
            // ZoomEvents(0);
        }

        // 버튼 클릭 이벤트
        private void zoomInBtn_Click(object sender, EventArgs e)
        {
            // ZoomEvents(1);
        }

        // 버튼 클릭 이벤트
        private void EditBtn_Click(object sender, EventArgs e)
        {
            active_drawing = false;
            active_zoom = false;
            active_modify = true;
            active_selection = true;
            ActiveCursor(2, edit_cursorSize);
        }

        // 버튼 클릭 이벤트
        private void copyBtn_Click(object sender, EventArgs e)
        {
            Modify1Index = 0;
        }

        // 버튼 클릭 이벤트
        private void moveBtn_Click(object sender, EventArgs e)
        {
            Modify1Index = 1;
        }

        private void rotateBtn_Click(object sender, EventArgs e)
        {
            Modify1Index = 2;
        }

        private void scaleBtn_Click(object sender, EventArgs e)
        {
            Modify1Index = 3;
        }

        // #010 - scroll bar
        // 수평 스크롤 이벤트
        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            XScroll = (sender as HScrollBar).Value;
            drawing.Refresh();
        }

        // #010 - scroll bar
        // 수직 스크롤 이벤트
        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            YScroll = (sender as VScrollBar).Value;
            drawing.Refresh();
        }

        // #025 - mouse wheel event
        private void drawing_MouseWheel(object sender, MouseEventArgs e)
        {
            float cx = drawing.ClientSize.Width / 2.0f;
            float cy = drawing.ClientSize.Height / 2.0f;

            float w = (x1 < cx) ? Math.Min(x1, x2) * 2.0f : Math.Max(x1, x2) * 2.0f;
            float h = (y1 < cy) ? Math.Max(y1, y2) * 2.0f : Math.Min(y1, y2) * 2.0f;

            float scale = (e.Delta < 0) ? 1 / 1.25f : 1.25f;

            ScaleFactor *= scale;

            float width = w * scale;
            float height = h * scale;

            float wl = (w - width) / 2;
            float hl = (h - height) / 2;

            XScroll = XScroll * scale - Pixel_to_Mn(wl);
            YScroll = YScroll * scale + Pixel_to_Mn(hl);

            SetScrollBarValues();
        }
    }
}
