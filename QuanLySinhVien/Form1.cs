using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class Form1 : Form
    {
        public SqlConnection conn;

        public Form1()
        {
            InitializeComponent();
        }
        public void ketnoi()
        {
            String cauketnoi = "Server = DUCVO; database = QuanLySinhVien; integrated security = true";
            conn = new SqlConnection(cauketnoi);
            conn.Open();
        }
        public void hienthi()
        {
            ketnoi();
            string sql = "SELECT * FROM SINHVIEN";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet, "SinhVien");
            dataGridView1.DataSource = dataSet;
            dataGridView1.DataMember = "SinhVien";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ketnoi();
            hienthi();
            HienThiComBoBox("SELECT * FROM NHOM", comboBoxNhom, "MaNhom", "TenNhom");
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxMSSV.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBoxHoten.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBoxNgaySinh.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            String gtinh = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            if (gtinh == "Nam")
            {
                checkBoxNam.Checked = true;
                checkBoxNu.Checked = false;
            }
            else
            {
                checkBoxNam.Checked = false;
                checkBoxNu.Checked = true;
            }
        }
        public void HienThiComBoBox(string sql, ComboBox cb, string VlShow, string VlHide)
        {
            ketnoi();
            SqlCommand sqlCommand = new SqlCommand(sql, conn);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlDataReader);
            cb.DataSource = dataTable;
            cb.DisplayMember = VlHide;
            cb.ValueMember = VlShow;
        }
        private void buttonThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void buttonThem_Click(object sender, EventArgs e)
        {
            String maSV = textBoxMSSV.Text;
            String hoten = textBoxHoten.Text;
            String ngaysinh = textBoxNgaySinh.Text;
            string nhom = comboBoxNhom.SelectedValue.ToString();
            String gtinh = "Nam";
            if (checkBoxNu.Checked == true)
            {
                gtinh = "Nu";
            }
            string query = "insert into SinhVien values ('" + maSV + "', N'" + hoten + "', " +
                "(CONVERT(date, '" + ngaysinh + "', 105)), N'" + gtinh + "', '" + nhom + "')";
            SqlCommand sqlCommand = new SqlCommand(query, conn);
            sqlCommand.ExecuteNonQuery();
            hienthi();
        }
        private void buttonXoa_Click(object sender, EventArgs e)
        {
            String maSV = textBoxMSSV.Text;
            String sql_xoa = "DELETE FROM SINHVIEN WHERE MSSV = '" + maSV + "' ";
            SqlCommand sqlCommand = new SqlCommand(sql_xoa, conn);
            sqlCommand.ExecuteNonQuery();
            hienthi();
        }
        private void buttonSua_Click(object sender, EventArgs e)
        {
            String maSV = textBoxMSSV.Text;
            String hoten = textBoxHoten.Text;
            String ngaysinh = textBoxNgaySinh.Text;
            string nhom = comboBoxNhom.SelectedValue.ToString();
            String gtinh = "Nam";
            if (checkBoxNu.Checked == true)
            {
                gtinh = "Nu";
            }
            string query = "update SinhVien set HoTen = N'" + hoten + "', NgaySinh = " +
                "(CONVERT(date, '" + ngaysinh + "', 105)), GioiTinh = N'" + gtinh + "',  " +
                "MaNhom = '" + nhom + "' where MSSV = '" + maSV + "'";
            SqlCommand sqlCommand = new SqlCommand(query, conn);
            sqlCommand.ExecuteNonQuery();
            hienthi();
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string chuoi = textBoxTimKiem.Text;
                    ketnoi();
                    string query = "select MSSV as [MSSV], HoTen as [Họ tên], FORMAT " +
                        "(NgaySinh, 'dd-MM-yyyy') as [Ngày sinh], GioiTinh as [Giới tính], " +
                        "TenNhom as [Nhóm] from SinhVien sv join Nhom n on sv.MaNhom = n.MaNhom " +
                        "where MSSV like '%" + chuoi + "%' or HoTen LIKE N'%" + chuoi + "%'";
                    SqlDataAdapter dta = new SqlDataAdapter(query, conn);
                    DataSet ds = new DataSet();
                    dta.Fill(ds, "DS");
                    dataGridView1.DataSource = ds;
                    dataGridView1.DataMember = "DS";
                }
            }
        }

        private void buttonDatLai_Click(object sender, EventArgs e)
        {
            textBoxMSSV.Text = "";
            textBoxHoten.Text = "";
            textBoxNgaySinh.Text = "";
            comboBoxNhom.Text = "";
            checkBoxNam.Checked = false;
            checkBoxNu.Checked = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBoxTimKiem_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

 

