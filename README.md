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
- **Tích hợp AI Trực Tiếp**: Kết nối trực tiếp đến endpoint OpenAI-compatible (`cx/gpt-6-luna`), không cần backend trung gian.
- **Quét OCR Bằng Chuột Phải**: Giữ chuột phải kéo vùng câu hỏi cần giải, hệ thống tự động đọc và gửi lên AI.
- **Hiển Thị Tinh Gọn**: Màn hình overlay góc trái chỉ hiển thị duy nhất câu trả lời / đáp án của AI, không bị ảnh hưởng khi bạn gõ phím.
- **Tàng Hình (Stealth Mode)**: Overlay được bảo vệ với tính năng ẩn khỏi chụp/quay màn hình (`WDA_EXCLUDEFROMCAPTURE`), độ mờ (opacity) stealth tự nhiên.
- **Điều Khiển Tiện Lợi**:
  - `Mouse Wheel (Con lăn chuột)`: Cuộn xem nội dung nếu câu trả lời nhiều dòng.
  - `\` hoặc `Escape`: Xóa câu trả lời và ẩn ngay overlay.
  - `Nút chuột phụ (XButton2)`: Xóa và ẩn overlay.

## 🚀 Hướng dẫn cài đặt & sử dụng
1. Tải bản phát hành mới nhất tại mục [Releases](https://github.com/nguyenquocan0101/BlackMagic/releases).
2. Giải nén file `.zip`.
3. Chạy file `bb_monitor.exe` với quyền **Administrator** (Run as administrator).
4. Giữ chuột phải và kéo chọn vùng chứa câu hỏi trên màn hình để nhận câu trả lời.
