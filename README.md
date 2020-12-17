# **WeSplit App DA02**

# Điểm đề nghị: 12đ
- 10đ Hoàn thành đẩy đủ yêu cầu đề
- 2đ Cộng thêm

### Thông tin nhóm
| MSSV     |           Họ và tên    |              Email           |
|:--------:|:----------------------:|:----------------------------:|
| 1712675  | Nguyễn Thành Vĩnh Phúc | 1712675@student.hcmus.edu.vn |
| 1712818  | Võ Thiện Tín           | 1712818@student.hcmus.edu.vn |
| 1712830  | Ngô Nha Trang          | 1712830@student.hcmus.edu.vn |

### Các chức năng làm được (100%)
 - SplashScreen (100% = 0.5đ)
    - Hiện thị thông tin chào mừng khi khởi chạy
    - Mỗi lần hiện thị ngẫu nhiên một thông tin thú vị về một địa điểm du lịch
    - Cho phép chọn check “Không hiện hộp thoại này mỗi khi khởi động”. Từ nay về sau đi thẳng vào màn hình HomeScreen luôn
 - HomeScreen (100% = 4đ)
    - Liệt kê danh sách các chuyến đi, phân chia theo đã đi và dự định/sắp đi.
    - Xem chi tiết các chuyến đi: 
        - Thông tin chuyến đi, xem các ảnh của chuyến đi dạng carousel
        - Danh sách thành viên, thông tin tiền thu kèm biểu đồ phần trăm. Ngoài ra còn cho biết thành viên nào ứng trước tiền cho thành viên nào, Thành viên nào còn chưa/đã trả đủ
        - Danh sách các địa điểm dừng, các chi phí khác, thông tin tiền chi ra từng địa điểm kèm biểu đồ
 - SearchScreen (100% = 2đ ) : Tìm kiếm chuyến đi theo tên chuyến đi, theo tên thành viên tham gia.
- CreateJourneyScreen (100% = 1đ) : Cho phép trưởng nhóm tạo mới một chuyến đi với các thông tin sau
    - Tên chuyến đi, thông tin chuyến đi
    - Thêm các thành viên kèm tiền thu, có thêm thành viên nào ứng tiền cho thành viên nào
    - Thêm các điểm dừng kèm chi phí, ngoài ra còn có các chi phí khác
- UpdateJourneyScreen (100% = 2.5đ) : Cập nhập thông tin chuyến đi với các thông tin sau
    - Tên chuyến đi, thông tin chuyến đi
    - Danh sách thành viên kèm tiền thu
    - Danh sách điểm dừng kèm chi phí, bao gồm các chi phí khác
### Các đặc điểm đặc sắc ( +2 đ)
 - Ngoài tìm kiếm theo tên chuyến đi, thành viên, thì còn có tìm kiếm theo tên điểm dừng có trong chuyến đi
 - Có MemberListScreen (màn hình danh sách thành viên)
    - Thêm/sửa cập nhật thông tin các thành viên
    - Tìm kiếm thành viên theo tên
 - Có LocationListScreen (màn hình danh sách các điểm dừng)
    - Thêm/sửa cập nhật thông tin các điểm dừng
    - Tìm kiếm điểm dừng theo tên
 - Có thêm chức năng SettingScreen
    - Có thực hiện phân trang trên cả HomeScreen/ MemberListScreen/ LocationListScreen và lưu lại thông tin phân trang khi tắt app
    - Có thực hiện xắp sếp (sort) trên các danh sách, lưu lại thông tin sort khi tắt app
        - HomeScreen sort theo: Tăng/giảm dần theo Tên/ngày đi/ngày về
        - MemberListScreen sort theo: Tăng/giảm dần theo tên thành viên
        - LocationListScreen sort theo: Tăng/giảm dần theo tên điểm dừng
    - Chỉnh lại hiển thị SplashScreen hay không và lưu lại khi tắt app
 - Có AboutUsScreen: giới thiệu/ kèm thông tin thành viên nhóm
 - Có học sử dụng package hỗ trợ thêm tạo giao diện

### Con đường bất hạnh 
- Check dữ liệu người dùng nhập vào
- Ciệc kết hợp logic Search/Sort/Phân trang với nhau diễn tra tuần tự.
### Link youtube demo

### ghi chú quyền
- Thầy có quyền sử dùng video demo , nhưng sourceCode thì nhóm em sẽ không public ra ạ