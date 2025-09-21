# core_w1

1. Xây dựng DI cấu hình:

✓ Sử dụng IConfiguration lưu các thông tin từ tập tinh appSetting.json
✓ Tạo tập tin cấu hình lưu thông tin dung lượng file tối đa và danh sách các
IP bị cấm truy cập

2. Xây dựng Middleware lưu trữ lại các thông tin của Request gồm: URL, thời gian nhận
yêu cầu, IP gửi yêu cầu đến vào tập tin văn bản request.log với cấu trúc tùy chọn
3. Xây dựng DI dạng Scoped chứa danh sách người dùng với các thông tin Username,
Password, Vai trò (1|2|3)
4. Xây dựng Middleware load danh sách User vào DI trên. (Từ tập tin, khởi tạo danh
sách, CSDL, ... tùy lựa chọn của nhóm)
5. Xây dựng trang web (Routing theo ý nhóm, yêu cầu không phù hợp thì trả về trang
404 được nhóm thiết kế) chứa danh sách người dùng dạng table có các thông tin
trên. Danh sách này được đưa lên từ DI ở trên và có phân trang.
Hình thức nộp bài:
- Tập tin word Ten_Nhom.docx chứa nội dung trả lời, chụp hình ảnh minh hoạ, giải
thích. Chụp hình phân công và thống kê phân công.
