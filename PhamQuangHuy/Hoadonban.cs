using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLcuahangbanmaytinh
{
    public partial class frmhoadonban : Form
    {
        public frmhoadonban()
        {
            InitializeComponent();
        }
        ConnectCSDL co = new ConnectCSDL();
        public void LoadData()
        {
            co.KetNoi();
            dgvDShdb.DataSource = co.GetData("select * from HoaDonBan");
            co.NgatKetNoi();
        }

        private void frmhoadonban_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadData();
            cbomamaytinh.DataSource = co.GetData("select * from ThongTinMayTinh");
            cbomamaytinh.ValueMember = "MaMT";
            cbomamaytinh.DisplayMember = "MaMT";
           
            cbomakh.DataSource = co.GetData("select * from KhachHang");
            cbomakh.ValueMember = "MaKH";
            cbomakh.DisplayMember = "MaKH";

         
           
        }

        private void btnlammoi_Click(object sender, EventArgs e)
        {
            this.txtdongia.Clear();
            this.txtmahoadonban.Clear();
            this.txtmanv.Clear();
            this.txtsodienthoai.Clear();
            this.txtdiachi.Clear();

            this.txtsoluong.Clear();
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            double soluong = double.Parse(txtsoluong.Text);
            double dongia = double.Parse(txtdongia.Text);
            double tongtien = soluong * dongia;
            txtTongtien.Text = tongtien.ToString();
            co.KetNoi();
            string sqlthem = "insert into Hoadonban values ('" + txtmahoadonban.Text + "','" + txtmanv.Text + "','" + cbomakh.SelectedValue + "','" + cbomamaytinh.SelectedValue + "','" + txtsoluong.Text + "','"+mtbNgayban.Text+"','" + txtdiachi.Text + "','" + txtsodienthoai.Text + "','" + txtdongia.Text + "','" + txtTongtien.Text + "')";
            co.ThucThi(sqlthem);
            frmhoadonban_Load(sender, e);
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            DialogResult traloi;
            traloi = MessageBox.Show("B???n c?? ch???c mu???n x??a kh??ng?", "Tr??? l???i",
           MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (traloi == DialogResult.OK)
            {
                string sqlxoa = "delete from Hoadonban where MaHDB = '" + txtmahoadonban.Text + "'";
                co.ThucThi(sqlxoa);
            }
            frmhoadonban_Load(sender, e);

          
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            DialogResult traloi;
            traloi = MessageBox.Show("B???n c?? ch???c mu???n tho??t kh??ng?", "Tr??? l???i",
           MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (traloi == DialogResult.OK)
                Application.Exit();
        }

        private void btnquaylai_Click(object sender, EventArgs e)
        {
            frmMenu tc = new frmMenu();
            tc.Show();
            this.Hide();
        }

        private void dgvDShdb_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtmahoadonban.Text = dgvDShdb.Rows[i].Cells[0].Value.ToString().Trim();
            txtmanv.Text = dgvDShdb.Rows[i].Cells[1].Value.ToString().Trim();
            cbomakh.Text = dgvDShdb.Rows[i].Cells[2].Value.ToString().Trim();
            cbomamaytinh.Text = dgvDShdb.Rows[i].Cells[3].Value.ToString().Trim();
            txtsoluong.Text = dgvDShdb.Rows[i].Cells[4].Value.ToString().Trim();

            txtdiachi.Text = dgvDShdb.Rows[i].Cells[6].Value.ToString().Trim();
            txtsodienthoai.Text = dgvDShdb.Rows[i].Cells[7].Value.ToString().Trim();
            txtdongia.Text = dgvDShdb.Rows[i].Cells[8].Value.ToString().Trim();
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            double soluong = double.Parse(txtsoluong.Text);
            double dongia = double.Parse(txtdongia.Text);
            double tongtien = soluong * dongia;
            txtTongtien.Text = tongtien.ToString();
            co.KetNoi();
            string sqlsua = "update Hoadonban set MaHDB='" + txtmahoadonban.Text + "',MaNV='" + txtmanv.Text + 
                "',MaKH='" + cbomakh.SelectedValue + "',MaMT='" + cbomamaytinh.SelectedValue + 
                "',Soluong='" + txtsoluong.Text + "',Ngayban='" + mtbNgayban.Text + "',Diachi='" + txtdiachi.Text + 
                "',sdt='" + txtsodienthoai.Text + "',Dongia='" + txtdongia.Text + "',Tongtien='" + txtTongtien.Text + "' where MaHDB='" + txtmahoadonban.Text + "'";
            co.ThucThi(sqlsua);
            LoadData();
            
        }
        public void Export(DataTable dt, string sheetName, string title)
        {
            //T???o c??c ?????i t?????ng Excel
            Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbooks oBooks;
            Microsoft.Office.Interop.Excel.Sheets oSheets;
            Microsoft.Office.Interop.Excel.Workbook oBook;
            Microsoft.Office.Interop.Excel.Worksheet oSheet;

            //T???o m???i m???t Excel WorkBook 
            oExcel.Visible = true;
            oExcel.DisplayAlerts = false;
            oExcel.Application.SheetsInNewWorkbook = 1;
            oBooks = oExcel.Workbooks;

            oBook = (Microsoft.Office.Interop.Excel.Workbook)(oExcel.Workbooks.Add(Type.Missing));
            oSheets = oBook.Worksheets;
            oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oSheets.get_Item(1);
            oSheet.Name = sheetName;

            // T???o ph???n ?????u n???u mu???n
            Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "J1");
            head.MergeCells = true;
            head.Value2 = title;
            head.Font.Bold = true;
            head.Font.Name = "Times New Roman";
            head.Font.Size = "18";
            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // T???o ti??u ????? c???t 
            Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");
            cl1.Value2 = "M?? HDB";
            cl1.ColumnWidth = 13.5;

            Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");
            cl2.Value2 = "M?? NV";
            cl2.ColumnWidth = 13.5;

            Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");
            cl3.Value2 = "M?? KH";
            cl3.ColumnWidth = 25.0;

            Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");
            cl4.Value2 = "M?? MT";
            cl4.ColumnWidth = 13.5;

            Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");
            cl5.Value2 = "S??? l?????ng";
            cl5.ColumnWidth = 13.5;

            Microsoft.Office.Interop.Excel.Range cl6 = oSheet.get_Range("F3", "F3");
            cl6.Value2 = "Ng??y b??n";
            cl6.ColumnWidth = 13.5;

            Microsoft.Office.Interop.Excel.Range cl7 = oSheet.get_Range("G3", "G3");
            cl7.Value2 = "?????a ch???";
            cl7.ColumnWidth = 13.5;

            Microsoft.Office.Interop.Excel.Range cl8 = oSheet.get_Range("H3", "H3");
            cl8.Value2 = "sdt";
            cl8.ColumnWidth = 13.5;

            Microsoft.Office.Interop.Excel.Range cl9 = oSheet.get_Range("I3", "I3");
            cl9.Value2 = "????n gi??";
            cl9.ColumnWidth = 13.5;

            Microsoft.Office.Interop.Excel.Range cl10 = oSheet.get_Range("J3", "J3");
            cl10.Value2 = "T???ng ti???n";
            cl10.ColumnWidth = 13.5;


            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "J3");
            rowHead.Font.Bold = true;
            // K??? vi???n
            rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
            // Thi???t l???p m??u n???n
            rowHead.Interior.ColorIndex = 15;
            rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;


            // T???o m???ng ?????i t?????ng ????? l??u d??? to??n b??? d??? li???u trong DataTable,
            // v?? d??? li???u ???????c ???????c g??n v??o c??c Cell trong Excel ph???i th??ng qua object thu???n.
            object[,] arr = new object[dt.Rows.Count, dt.Columns.Count];

            //Chuy???n d??? li???u t??? DataTable v??o m???ng ?????i t?????ng
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                DataRow dr = dt.Rows[r];
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    arr[r, c] = dr[c];
                }
            }

            //Thi???t l???p v??ng ??i???n d??? li???u
            int rowStart = 4;
            int columnStart = 1;

            int rowEnd = rowStart + dt.Rows.Count - 1;
            int columnEnd = dt.Columns.Count;

            // ?? b???t ?????u ??i???n d??? li???u
            Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, columnStart];
            // ?? k???t th??c ??i???n d??? li???u
            Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnEnd];
            // L???y v??? v??ng ??i???n d??? li???u
            Microsoft.Office.Interop.Excel.Range range = oSheet.get_Range(c1, c2);

            //??i???n d??? li???u v??o v??ng ???? thi???t l???p
            range.Value2 = arr;

            // K??? vi???n
            range.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
            // C??n gi???a c???t STT
            Microsoft.Office.Interop.Excel.Range c3 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnStart];
            Microsoft.Office.Interop.Excel.Range c4 = oSheet.get_Range(c1, c3);
            oSheet.get_Range(c3, c4).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

        }

        private void btninhoadon_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgvDShdb.DataSource;
            Export(dt, "Danh sach", "DANH S??CH H??A ????N B??N");
        }

        public void loaddata()
        {
            co.KetNoi();
            dgvDShdb.DataSource = co.GetData("select * from Hoadonban");
            co.NgatKetNoi();
        }

        private void grphoadonban_Enter(object sender, EventArgs e)
        {

        }

    }
}
