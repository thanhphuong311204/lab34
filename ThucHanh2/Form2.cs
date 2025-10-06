using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ThucHanh2
{
    public class Form2 : Form
    {
        // Chuỗi kết nối - Thay đổi theo máy của bạn
        private string strCon = "Server=localhost;Database=qlsinhvien;Uid=root;Pwd=Tranthanhphuong311204@;"; private SqlConnection sqlCon = null;

        // Controls
        private GroupBox grpTruyVan1, grpTruyVan2, grpTruyVan3, grpTruyVan4;
        private Button btnCount, btnXemThongTin, btnListView, btnXemDS;
        private TextBox txtNhapMaSv, txtTenSV, txtGioiTinh, txtNgaySinh, txtQueQuan, txtMaLop;
        private TextBox txtNhapTenKhoa;
        private ListView lsvList, lsvDanhSach;
        private Label lblKetQua;

        public Form2()
        {
            KhoiTaoGiaoDien();
        }

        private void KhoiTaoGiaoDien()
        {
            this.Text = "Thực hành truy vấn dữ liệu";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScroll = true;

            grpTruyVan1 = new GroupBox
            {
                Text = "1. Truy vấn lấy 1 giá trị - Đếm số lượng sinh viên",
                Location = new Point(20, 20),
                Size = new Size(450, 100),
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            btnCount = new Button
            {
                Text = "Đếm số lượng SV",
                Location = new Point(20, 30),
                Size = new Size(150, 30),
                Font = new Font("Arial", 9)
            };
            btnCount.Click += BtnCount_Click;

            lblKetQua = new Label
            {
                Text = "Kết quả: ",
                Location = new Point(200, 35),
                Size = new Size(230, 20),
                Font = new Font("Arial", 9, FontStyle.Regular)
            };

            grpTruyVan1.Controls.Add(btnCount);
            grpTruyVan1.Controls.Add(lblKetQua);

            // ===== GROUPBOX 2: XEM THÔNG TIN 1 SINH VIÊN =====
            grpTruyVan2 = new GroupBox
            {
                Text = "2. Truy vấn lấy 1 dòng dữ liệu - Xem thông tin sinh viên",
                Location = new Point(500, 20),
                Size = new Size(460, 240),
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            Label lblNhapMaSV = new Label
            {
                Text = "Nhập mã SV:",
                Location = new Point(20, 30),
                Size = new Size(100, 20),
                Font = new Font("Arial", 9, FontStyle.Regular)
            };

            txtNhapMaSv = new TextBox
            {
                Location = new Point(130, 27),
                Size = new Size(150, 25)
            };

            btnXemThongTin = new Button
            {
                Text = "Xem thông tin",
                Location = new Point(300, 25),
                Size = new Size(120, 30),
                Font = new Font("Arial", 9)
            };
            btnXemThongTin.Click += BtnXemThongTin_Click;

            // Các textbox hiển thị thông tin
            Label lblTenSV = new Label { Text = "Tên SV:", Location = new Point(20, 70), Size = new Size(100, 20), Font = new Font("Arial", 9, FontStyle.Regular) };
            txtTenSV = new TextBox { Location = new Point(130, 67), Size = new Size(300, 25), ReadOnly = true };

            Label lblGioiTinh = new Label { Text = "Giới tính:", Location = new Point(20, 100), Size = new Size(100, 20), Font = new Font("Arial", 9, FontStyle.Regular) };
            txtGioiTinh = new TextBox { Location = new Point(130, 97), Size = new Size(300, 25), ReadOnly = true };

            Label lblNgaySinh = new Label { Text = "Ngày sinh:", Location = new Point(20, 130), Size = new Size(100, 20), Font = new Font("Arial", 9, FontStyle.Regular) };
            txtNgaySinh = new TextBox { Location = new Point(130, 127), Size = new Size(300, 25), ReadOnly = true };

            Label lblQueQuan = new Label { Text = "Quê quán:", Location = new Point(20, 160), Size = new Size(100, 20), Font = new Font("Arial", 9, FontStyle.Regular) };
            txtQueQuan = new TextBox { Location = new Point(130, 157), Size = new Size(300, 25), ReadOnly = true };

            Label lblMaLop = new Label { Text = "Mã lớp:", Location = new Point(20, 190), Size = new Size(100, 20), Font = new Font("Arial", 9, FontStyle.Regular) };
            txtMaLop = new TextBox { Location = new Point(130, 187), Size = new Size(300, 25), ReadOnly = true };

            grpTruyVan2.Controls.Add(lblNhapMaSV);
            grpTruyVan2.Controls.Add(txtNhapMaSv);
            grpTruyVan2.Controls.Add(btnXemThongTin);
            grpTruyVan2.Controls.Add(lblTenSV);
            grpTruyVan2.Controls.Add(txtTenSV);
            grpTruyVan2.Controls.Add(lblGioiTinh);
            grpTruyVan2.Controls.Add(txtGioiTinh);
            grpTruyVan2.Controls.Add(lblNgaySinh);
            grpTruyVan2.Controls.Add(txtNgaySinh);
            grpTruyVan2.Controls.Add(lblQueQuan);
            grpTruyVan2.Controls.Add(txtQueQuan);
            grpTruyVan2.Controls.Add(lblMaLop);
            grpTruyVan2.Controls.Add(txtMaLop);


            grpTruyVan3 = new GroupBox
            {
                Text = "3. Truy vấn lấy nhiều dòng - Danh sách sinh viên",
                Location = new Point(20, 130),
                Size = new Size(450, 300),
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            btnListView = new Button
            {
                Text = "Hiển thị danh sách SV",
                Location = new Point(20, 30),
                Size = new Size(180, 30),
                Font = new Font("Arial", 9)
            };
            btnListView.Click += BtnListView_Click;

            lsvList = new ListView
            {
                Location = new Point(20, 70),
                Size = new Size(410, 210),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true
            };
            lsvList.Columns.Add("Mã SV", 80);
            lsvList.Columns.Add("Tên SV", 120);
            lsvList.Columns.Add("Giới tính", 70);
            lsvList.Columns.Add("Ngày sinh", 80);
            lsvList.Columns.Add("Quê quán", 100);
            lsvList.Columns.Add("Mã lớp", 60);

            grpTruyVan3.Controls.Add(btnListView);
            grpTruyVan3.Controls.Add(lsvList);

            // ===== GROUPBOX 4: SỬ DỤNG PARAMETER =====
            grpTruyVan4 = new GroupBox
            {
                Text = "4. Sử dụng Parameter - Danh sách lớp theo khoa",
                Location = new Point(500, 270),
                Size = new Size(460, 300),
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            Label lblNhapTenKhoa = new Label
            {
                Text = "Nhập tên khoa:",
                Location = new Point(20, 30),
                Size = new Size(100, 20),
                Font = new Font("Arial", 9, FontStyle.Regular)
            };

            txtNhapTenKhoa = new TextBox
            {
                Location = new Point(130, 27),
                Size = new Size(180, 25)
            };
            txtNhapTenKhoa.Text = "Công nghệ thông tin";

            btnXemDS = new Button
            {
                Text = "Xem danh sách",
                Location = new Point(320, 25),
                Size = new Size(120, 30),
                Font = new Font("Arial", 9)
            };
            btnXemDS.Click += BtnXemDS_Click;

            lsvDanhSach = new ListView
            {
                Location = new Point(20, 70),
                Size = new Size(420, 210),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true
            };
            lsvDanhSach.Columns.Add("Tên lớp", 250);
            lsvDanhSach.Columns.Add("Mã lớp", 150);

            grpTruyVan4.Controls.Add(lblNhapTenKhoa);
            grpTruyVan4.Controls.Add(txtNhapTenKhoa);
            grpTruyVan4.Controls.Add(btnXemDS);
            grpTruyVan4.Controls.Add(lsvDanhSach);

            // Thêm các GroupBox vào Form
            this.Controls.Add(grpTruyVan1);
            this.Controls.Add(grpTruyVan2);
            this.Controls.Add(grpTruyVan3);
            this.Controls.Add(grpTruyVan4);
        }

        // ===== 1. ĐẾM SỐ LƯỢNG SINH VIÊN =====
        private void BtnCount_Click(object sender, EventArgs e)
        {
            try
            {
                // Mở kết nối
                if (sqlCon == null)
                {
                    sqlCon = new SqlConnection(strCon);
                }
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }

                // Đối tượng thực thi truy vấn
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "select COUNT(*) from SinhVien";

                // Gửi truy vấn vào kết nối
                sqlCmd.Connection = sqlCon;

                // Nhận kết quả
                int soLuongSV = (int)sqlCmd.ExecuteScalar();
                lblKetQua.Text = "Kết quả: Số lượng sinh viên là " + soLuongSV;

                MessageBox.Show("Số lượng sinh viên là: " + soLuongSV, "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== 2. XEM THÔNG TIN 1 SINH VIÊN =====
        private void BtnXemThongTin_Click(object sender, EventArgs e)
        {
            try
            {
                // Mở kết nối
                if (sqlCon == null)
                {
                    sqlCon = new SqlConnection(strCon);
                }
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }

                // Lấy thông tin cần truy vấn
                string maSV = txtNhapMaSv.Text.Trim();

                if (string.IsNullOrEmpty(maSV))
                {
                    MessageBox.Show("Vui lòng nhập mã sinh viên!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Đối tượng thực thi truy vấn
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "select * from SinhVien where MaSV='" + maSV + "'";

                // Gán vào kết nối
                sqlCmd.Connection = sqlCon;

                // Thực thi truy vấn
                SqlDataReader reader = sqlCmd.ExecuteReader();
                if (reader.Read())
                {
                    string tenSV = reader.GetString(1);
                    bool gioiTinhBool = reader.GetBoolean(2);
                    string gioiTinh = gioiTinhBool ? "Nam" : "Nữ";
                    string ngaySinh = reader.GetDateTime(3).ToString("dd/MM/yyyy");
                    string queQuan = reader.GetString(4);
                    string maLop = reader.GetString(5);

                    // Hiển thị kết quả
                    txtTenSV.Text = tenSV;
                    txtGioiTinh.Text = gioiTinh;
                    txtNgaySinh.Text = ngaySinh;
                    txtQueQuan.Text = queQuan;
                    txtMaLop.Text = maLop;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sinh viên có mã: " + maSV, "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Xóa trắng các textbox
                    txtTenSV.Clear();
                    txtGioiTinh.Clear();
                    txtNgaySinh.Clear();
                    txtQueQuan.Clear();
                    txtMaLop.Clear();
                }

                // Đóng đầu đọc
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== 3. HIỂN THỊ DANH SÁCH SINH VIÊN =====
        private void BtnListView_Click(object sender, EventArgs e)
        {
            try
            {
                // Xóa dữ liệu cũ trong ListView
                lsvList.Items.Clear();

                // Mở kết nối
                if (sqlCon == null)
                {
                    sqlCon = new SqlConnection(strCon);
                }
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }

                // Đối tượng thực thi truy vấn
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "select * from SinhVien";

                // Gán vào kết nối
                sqlCmd.Connection = sqlCon;

                // Thực thi truy vấn
                SqlDataReader reader = sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    // Đọc dữ liệu trong database
                    string maSV = reader.GetString(0);
                    string tenSV = reader.GetString(1);
                    bool gioiTinhBool = reader.GetBoolean(2);
                    string gioiTinh = gioiTinhBool ? "Nam" : "Nữ";
                    string ngaySinh = reader.GetDateTime(3).ToString("dd/MM/yyyy");
                    string queQuan = reader.GetString(4);
                    string maLop = reader.GetString(5);

                    // Hiển thị trên listview
                    ListViewItem lvi = new ListViewItem(maSV);
                    lvi.SubItems.Add(tenSV);
                    lvi.SubItems.Add(gioiTinh);
                    lvi.SubItems.Add(ngaySinh);
                    lvi.SubItems.Add(queQuan);
                    lvi.SubItems.Add(maLop);
                    lsvList.Items.Add(lvi);
                }

                // Đóng đầu đọc
                reader.Close();

                MessageBox.Show("Hiển thị danh sách thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== 4. SỬ DỤNG PARAMETER =====
        private void BtnXemDS_Click(object sender, EventArgs e)
        {
            try
            {
                // Xóa dữ liệu cũ
                lsvDanhSach.Items.Clear();

                // Mở kết nối
                if (sqlCon == null)
                {
                    sqlCon = new SqlConnection(strCon);
                }
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }

                // Thông tin khoa cần hiển thị
                string tenKhoa = txtNhapTenKhoa.Text.Trim();
                string maKhoa = "";

                if (tenKhoa == "Công nghệ thông tin")
                {
                    maKhoa = "CNTT";
                }
                else if (tenKhoa == "Cơ khí")
                {
                    maKhoa = "CK";
                }
                else if (tenKhoa == "Điện tử")
                {
                    maKhoa = "DT";
                }
                else if (tenKhoa == "Kinh tế")
                {
                    maKhoa = "KT";
                }
                else
                {
                    MessageBox.Show("Tên khoa không hợp lệ!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Đối tượng truy vấn
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "select * from Lop where MaKhoa=@maKhoa";

                // Tạo parameter
                SqlParameter parMaKhoa = new SqlParameter("@maKhoa", SqlDbType.Char);
                parMaKhoa.Value = maKhoa;
                sqlCmd.Parameters.Add(parMaKhoa);

                // Gán vào kết nối
                sqlCmd.Connection = sqlCon;

                // Thực thi truy vấn
                SqlDataReader reader = sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    // Lấy dữ liệu từ database
                    string maLop = reader.GetString(0);
                    string tenLop = reader.GetString(1);

                    // Hiển thị trên listview
                    ListViewItem lvi = new ListViewItem(tenLop);
                    lvi.SubItems.Add(maLop);
                    lsvDanhSach.Items.Add(lvi);
                }

                // Đóng kết nối
                reader.Close();

                MessageBox.Show("Hiển thị danh sách lớp thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Đóng kết nối khi thoát form
            if (sqlCon != null && sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
            base.OnFormClosing(e);
        }
    }
}