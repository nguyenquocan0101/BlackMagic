<p align="center">
  <img src="assets/logo.png" alt="BlackMagic Logo" width="180" style="border-radius: 24px;" />
</p>

<h1 align="center">BlackMagic</h1>

<p align="center">
  <b>Công cụ hỗ trợ quét OCR và giải bài tập/đề thi trực tiếp bằng AI trên màn hình</b>
</p>

---

## 🔮 Giới thiệu
**BlackMagic** là công cụ stealth chuyên dụng chạy nền trên Windows, hỗ trợ người dùng quét nhanh vùng câu hỏi trên màn hình bằng chuột phải, sử dụng Tesseract OCR để đọc văn bản và kết nối trực tiếp với API AI để trả về đáp án ngay trên màn hình overlay trong suốt.

## ✨ Tính năng nổi bật
- **Tích hợp AI Trực Tiếp**: Kết nối trực tiếp đến endpoint OpenAI-compatible (`cx/gpt-6-luna`), trả lời siêu tốc.
- **Quét OCR Bằng Chuột Phải**: Giữ chuột phải kéo vùng câu hỏi cần giải, hệ thống tự động đọc và gửi lên AI.
- **Chỉ Trả Về Chữ Cái Đáp Án**: Tự động nhận diện câu hỏi trắc nghiệm và chỉ đưa ra chữ cái đáp án đúng (`A`, `B`, `C`, `D`), siêu ngắn gọn và an toàn.
- **Tàng Hình (Stealth Mode)**: Màu chữ xám chuẩn (`Color.Gray`), không viền, được bảo vệ với tính năng ẩn khỏi chụp/quay màn hình (`WDA_EXCLUDEFROMCAPTURE`), tương thích tốt với Safe Exam Browser (SEB).
- **Điều Khiển Tiện Lợi**:
  - `Click chuột giữa (con lăn)` / `\` / `Escape`: Xóa câu trả lời và ẩn ngay overlay tức thì.
  - `Ctrl + Up` / `Ctrl + Down`: Tăng / giảm độ mờ (opacity) của chữ overlay.
  - `Mouse Wheel (Con lăn chuột)`: Cuộn xem nội dung nếu câu trả lời nhiều dòng.

> [!IMPORTANT]
> **Lưu ý quan trọng trước khi sử dụng:**  
> Người dùng **cần chạy thử và kiểm tra (test) kỹ lưỡng trước trên các bài kiểm tra cũ hoặc đề thi thử** để làm quen với thao tác kéo chuột phải, đồng thời kiểm tra khả năng nhận diện của tính năng OCR (độ sắc nét, kích thước vùng kéo) và phản hồi của AI trước khi áp dụng vào bài làm thực tế.

## 🚀 Hướng dẫn cài đặt & sử dụng
1. Tải bản phát hành mới nhất tại mục [Releases](https://github.com/nguyenquocan0101/BlackMagic/releases).
2. Giải nén file `.zip`.
3. Chạy file `bb_monitor.exe` với quyền **Administrator** (Run as administrator).
4. Giữ chuột phải và kéo chọn vùng chứa câu hỏi trên màn hình để nhận câu trả lời.
