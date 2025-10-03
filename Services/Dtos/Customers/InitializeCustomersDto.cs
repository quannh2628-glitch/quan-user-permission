using System.ComponentModel.DataAnnotations;

namespace ABPPermission.Services.Dtos.Customers;

public class InitializeCustomersDto
{
    [Range(1, 100000, ErrorMessage = "Số lượng khách hàng phải từ 1 đến 100,000")]
    public int Count { get; set; } = 1000;

    [Required(ErrorMessage = "Danh sách tên không được để trống")]
    public string FirstNames { get; set; } = "An,Bình,Châu,Dũng,Duy,Giang,Hà,Hải,Hạnh,Hiếu,Hoa,Hòa,Hoài,Hùng,Hương,Huy,Hữu,Khánh,Khải,Khoa,Kiên,Lan,Lâm,Lân,Lệ,Liên,Liễu,Loan,Long,Mai,Mạnh,Minh,My,Nga,Ngân,Ngọc,Nguyên,Nguyễn,Nhã,Nhật,Nhung,Oanh,Phát,Phong,Phúc,Phương,Quang,Quân,Quý,Quyên,Quỳnh,Sang,Sáng,Sơn,Sỹ,Tâm,Tài,Tân,Thảo,Thanh,Thành,Thắng,Thi,Thiên,Thiện,Thịnh,Thoa,Thùy,Thủy,Thúy,Tiến,Toàn,Trang,Trí,Trinh,Trọng,Trúc,Trung,Tú,Tuấn,Tường,Tuyết,Uyên,Vân,Văn,Việt,Vinh,Vy,Xuân,Yến,Anh,Ánh,Ân,Ẩn,Đạt,Đăng,Đào,Đình,Đông,Đức";

    [Required(ErrorMessage = "Danh sách họ không được để trống")]
    public string LastNames { get; set; } = "Nguyễn,Trần,Lê,Phạm,Hoàng,Phan,Vũ,Võ,Đặng,Bùi,Đỗ,Hồ,Ngô,Dương,Lý,Phùng,Đinh,Tô,Trịnh,Đào,Hoàng,Phan,Vũ,Võ,Đặng,Bùi,Đỗ,Hồ,Ngô,Dương,Lý,Phùng,Đinh,Tô,Trịnh,Đào,Hoàng,Phan,Vũ,Võ,Đặng,Bùi,Đỗ,Hồ,Ngô,Dương,Lý,Phùng,Đinh,Tô,Trịnh,Đào,Hoàng,Phan,Vũ,Võ,Đặng,Bùi,Đỗ,Hồ,Ngô,Dương,Lý,Phùng,Đinh,Tô,Trịnh,Đào";

    public string Cities { get; set; } = "Hà Nội,Hồ Chí Minh,Đà Nẵng,Hải Phòng,Cần Thơ,An Giang,Bà Rịa - Vũng Tàu,Bạc Liêu,Bắc Giang,Bắc Kạn,Bắc Ninh,Bến Tre,Bình Định,Bình Dương,Bình Phước,Bình Thuận,Cà Mau,Cao Bằng,Đắk Lắk,Đắk Nông,Điện Biên,Đồng Nai,Đồng Tháp,Gia Lai,Hà Giang,Hà Nam,Hà Tĩnh,Hải Dương,Hậu Giang,Hòa Bình,Hưng Yên,Khánh Hòa,Kiên Giang,Kon Tum,Lai Châu,Lâm Đồng,Lạng Sơn,Lào Cai,Long An,Nam Định,Nghệ An,Ninh Bình,Ninh Thuận,Phú Thọ,Phú Yên,Quảng Bình,Quảng Nam,Quảng Ngãi,Quảng Ninh,Quảng Trị,Sóc Trăng,Sơn La,Tây Ninh,Thái Bình,Thái Nguyên,Thanh Hóa,Thừa Thiên Huế,Tiền Giang,Trà Vinh,Tuyên Quang,Vĩnh Long,Vĩnh Phúc,Yên Bái";

    public string EmailDomains { get; set; } = "gmail.com,yahoo.com,hotmail.com,outlook.com,live.com,viettel.vn,vnpt.vn,fpt.vn";
}
