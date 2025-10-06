using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ThucHanh2
{
    public partial class Form1 : Form
    {
        private TextBox txtHoTen, txtLop, txtDiaChi;
        private DateTimePicker dtpNgaySinh;
        private Button btnThem, btnSua, btnXoa, btnThoat;
        private DataGridView dgvSinhVien;
        private DataTable dtSinhVien;

        public Form1()
        {
            KhoiTaoGiaoDien();
            KhoiTaoDataTable();
        }

        // ===============================
        // 1. Tạo giao diện
        // ===============================
        private void KhoiTaoGiaoDien()
        {
            this.Text = "Danh sách sinh viên";
            this.Size = new Size(720, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;

            // ====== Tiêu đề ======
            Label lblTieuDe = new Label
            {
                Text = "DANH MỤC SINH VIÊN",
                Font = new Font("Arial", 18, FontStyle.Bold),
                ForeColor = Color.RoyalBlue,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 60
            };
            this.Controls.Add(lblTieuDe);

            // ====== GroupBox Thông tin sinh viên ======
            GroupBox gbThongTin = new GroupBox
            {
                Text = "Thông tin sinh viên:",
                Font = new Font("Arial", 10, FontStyle.Regular),
                Location = new Point(20, 70),
                Size = new Size(660, 120)
            };
            this.Controls.Add(gbThongTin);

            // Label + Textbox Họ tên
            Label lblHoTen = new Label
            {
                Text = "Họ tên:",
                Location = new Point(20, 30),
                AutoSize = true
            };
            txtHoTen = new TextBox
            {
                Location = new Point(80, 25),
                Size = new Size(200, 25)
            };
            gbThongTin.Controls.Add(lblHoTen);
            gbThongTin.Controls.Add(txtHoTen);

            // Label + Textbox Lớp
            Label lblLop = new Label
            {
                Text = "Lớp:",
                Location = new Point(320, 30),
                AutoSize = true
            };
            txtLop = new TextBox
            {
                Location = new Point(370, 25),
                Size = new Size(250, 25)
            };
            gbThongTin.Controls.Add(lblLop);
            gbThongTin.Controls.Add(txtLop);

            // Label + DateTimePicker Ngày sinh
            Label lblNgaySinh = new Label
            {
                Text = "Ngày sinh:",
                Location = new Point(20, 70),
                AutoSize = true
            };
            dtpNgaySinh = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Location = new Point(100, 65),
                Size = new Size(180, 25)
            };
            gbThongTin.Controls.Add(lblNgaySinh);
            gbThongTin.Controls.Add(dtpNgaySinh);

            // Label + Textbox Địa chỉ
            Label lblDiaChi = new Label
            {
                Text = "Địa chỉ:",
                Location = new Point(320, 70),
                AutoSize = true
            };
            txtDiaChi = new TextBox
            {
                Location = new Point(380, 65),
                Size = new Size(240, 25)
            };
            gbThongTin.Controls.Add(lblDiaChi);
            gbThongTin.Controls.Add(txtDiaChi);

            // ====== GroupBox Chức năng ======
            GroupBox gbChucNang = new GroupBox
            {
                Text = "Chức năng:",
                Font = new Font("Arial", 10, FontStyle.Regular),
                Location = new Point(20, 200),
                Size = new Size(660, 70)
            };
            this.Controls.Add(gbChucNang);

            btnThem = TaoButton("Thêm", 50);
            btnSua = TaoButton("Sửa", 180);
            btnXoa = TaoButton("Xóa", 310);
            btnThoat = TaoButton("Thoát", 440);

            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnThoat.Click += BtnThoat_Click;

            gbChucNang.Controls.AddRange(new Control[] { btnThem, btnSua, btnXoa, btnThoat });

            // ====== GroupBox Danh sách sinh viên ======
            GroupBox gbDanhSach = new GroupBox
            {
                Text = "Thông tin chung sinh viên:",
                Font = new Font("Arial", 10, FontStyle.Regular),
                Location = new Point(20, 280),
                Size = new Size(660, 160)
            };
            this.Controls.Add(gbDanhSach);

            dgvSinhVien = new DataGridView
            {
                Location = new Point(10, 25),
                Size = new Size(630, 120),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly = true,
                AllowUserToAddRows = false
            };
            gbDanhSach.Controls.Add(dgvSinhVien);
        }

        // Hàm tạo nút
        private Button TaoButton(string text, int x)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, 25),
                Size = new Size(100, 30),
                Font = new Font("Arial", 10, FontStyle.Regular)
            };
        }

        // ===============================
        // 2. Tạo DataTable
        // ===============================
        private void KhoiTaoDataTable()
        {
            dtSinhVien = new DataTable();
            dtSinhVien.Columns.Add("Họ tên");
            dtSinhVien.Columns.Add("Ngày sinh", typeof(DateTime));
            dtSinhVien.Columns.Add("Lớp");
            dtSinhVien.Columns.Add("Địa chỉ");

            dgvSinhVien.DataSource = dtSinhVien;
        }

        // ===============================
        // 3. Xử lý sự kiện các nút
        // ===============================

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                string.IsNullOrWhiteSpace(txtLop.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Họ tên và Lớp!",
                    "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dtSinhVien.Rows.Add(txtHoTen.Text, dtpNgaySinh.Value, txtLop.Text, txtDiaChi.Text);
            XoaTextBox();
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (dgvSinhVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dòng cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int index = dgvSinhVien.SelectedRows[0].Index;
            dtSinhVien.Rows[index]["Họ tên"] = txtHoTen.Text;
            dtSinhVien.Rows[index]["Ngày sinh"] = dtpNgaySinh.Value;
            dtSinhVien.Rows[index]["Lớp"] = txtLop.Text;
            dtSinhVien.Rows[index]["Địa chỉ"] = txtDiaChi.Text;
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvSinhVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int index = dgvSinhVien.SelectedRows[0].Index;
            dtSinhVien.Rows[index].Delete();
        }

        private void BtnThoat_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có chắc muốn thoát?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
                this.Close();
        }

        // ===============================
        // 4. Tiện ích
        // ===============================
        private void XoaTextBox()
        {
            txtHoTen.Clear();
            txtLop.Clear();
            txtDiaChi.Clear();
            txtHoTen.Focus();
        }
    }
}
