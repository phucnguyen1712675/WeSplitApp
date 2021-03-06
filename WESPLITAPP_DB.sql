USE [master]
GO
/****** Object:  Database [WESPLITAPP]    Script Date: 12/17/2020 9:55:19 PM ******/
CREATE DATABASE [WESPLITAPP]
-- CONTAINMENT = NONE
-- ON  PRIMARY 
--( NAME = N'WESPLITAPP', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\WESPLITAPP.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
-- LOG ON 
--( NAME = N'WESPLITAPP_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\WESPLITAPP_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
-- WITH CATALOG_COLLATION = DATABASE_DEFAULT
--GO
ALTER DATABASE [WESPLITAPP] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WESPLITAPP].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WESPLITAPP] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WESPLITAPP] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WESPLITAPP] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WESPLITAPP] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WESPLITAPP] SET ARITHABORT OFF 
GO
ALTER DATABASE [WESPLITAPP] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WESPLITAPP] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WESPLITAPP] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WESPLITAPP] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WESPLITAPP] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WESPLITAPP] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WESPLITAPP] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WESPLITAPP] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WESPLITAPP] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WESPLITAPP] SET  ENABLE_BROKER 
GO
ALTER DATABASE [WESPLITAPP] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WESPLITAPP] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WESPLITAPP] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WESPLITAPP] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WESPLITAPP] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WESPLITAPP] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WESPLITAPP] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WESPLITAPP] SET RECOVERY FULL 
GO
ALTER DATABASE [WESPLITAPP] SET  MULTI_USER 
GO
ALTER DATABASE [WESPLITAPP] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WESPLITAPP] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WESPLITAPP] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WESPLITAPP] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [WESPLITAPP] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [WESPLITAPP] SET QUERY_STORE = OFF
GO
USE [WESPLITAPP]
GO
/****** Object:  UserDefinedFunction [dbo].[GETCURRENTPROCEEDS]    Script Date: 12/17/2020 9:55:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GETCURRENTPROCEEDS](@TRIP_ID INT)
RETURNS FLOAT
AS
BEGIN
DECLARE @CURRENTPROCCEDS FLOAT;
SET @CURRENTPROCCEDS = 0;
SET @CURRENTPROCCEDS = @CURRENTPROCCEDS + (SELECT SUM(TRIP_MEMBER.AMOUNTPAID) FROM TRIP_MEMBER WHERE TRIP_ID = @TRIP_ID)
RETURN @CURRENTPROCCEDS
END;
GO
/****** Object:  UserDefinedFunction [dbo].[GETTOTALCOST]    Script Date: 12/17/2020 9:55:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GETTOTALCOST](@TRIP_ID INT)
RETURNS FLOAT
AS
BEGIN
DECLARE @TOTALCOST FLOAT;
SET @TOTALCOST = 0;
SET @TOTALCOST = @TOTALCOST + (SELECT SUM(TRIP_LOCATION.COSTS) FROM TRIP_LOCATION WHERE TRIP_ID = @TRIP_ID)
SET @TOTALCOST = @TOTALCOST + (SELECT SUM(TRIP_COSTINCURRED.COST) FROM TRIP_COSTINCURRED WHERE TRIP_ID = @TRIP_ID)
RETURN @TOTALCOST
END;
GO
/****** Object:  Table [dbo].[COSTINCURRED]    Script Date: 12/17/2020 9:55:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[COSTINCURRED](
	[COST_ID] [int] IDENTITY(1,1) NOT NULL,
	[NAME] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[COST_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LOCATION]    Script Date: 12/17/2020 9:55:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LOCATION](
	[LOCATION_ID] [int] IDENTITY(1,1) NOT NULL,
	[NAME] [nvarchar](255) NOT NULL,
	[ADDRESS] [nvarchar](255) NOT NULL,
	[DESCRIPTION] [nvarchar](255) NULL,
	[TYPE] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[LOCATION_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MEMBER]    Script Date: 12/17/2020 9:55:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MEMBER](
	[MEMBER_ID] [int] IDENTITY(1,1) NOT NULL,
	[NAME] [nvarchar](255) NOT NULL,
	[PHONENUMBER] [nvarchar](11) NOT NULL,
	[GENDER] [bit] NULL,
	[AVATAR] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MEMBER_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TRIP]    Script Date: 12/17/2020 9:55:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TRIP](
	[TRIP_ID] [int] IDENTITY(1,1) NOT NULL,
	[TITTLE] [nvarchar](255) NOT NULL,
	[DESCRIPTION] [nvarchar](255) NULL,
	[THUMNAILIMAGE] [nvarchar](255) NOT NULL,
	[CURRENTPROCEEDS] [float] NOT NULL,
	[TOTALCOSTS] [float] NOT NULL,
	[TOGODATE] [datetime] NULL,
	[RETURNDATE] [datetime] NULL,
	[ISDONE] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[TRIP_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TRIP_COSTINCURRED]    Script Date: 12/17/2020 9:55:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TRIP_COSTINCURRED](
	[COST_ID] [int] NOT NULL,
	[TRIP_ID] [int] NOT NULL,
	[COST] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[COST_ID] ASC,
	[TRIP_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TRIP_IMAGES]    Script Date: 12/17/2020 9:55:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TRIP_IMAGES](
	[TRIP_ID] [int] NOT NULL,
	[IMAGE] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_TRIP_IMAGES] PRIMARY KEY CLUSTERED 
(
	[TRIP_ID] ASC,
	[IMAGE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TRIP_LOCATION]    Script Date: 12/17/2020 9:55:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TRIP_LOCATION](
	[TRIP_ID] [int] NOT NULL,
	[LOCATION_ID] [int] NOT NULL,
	[COSTS] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TRIP_ID] ASC,
	[LOCATION_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TRIP_MEMBER]    Script Date: 12/17/2020 9:55:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TRIP_MEMBER](
	[TRIP_ID] [int] NOT NULL,
	[MEMBER_ID] [int] NOT NULL,
	[AMOUNTPAID] [float] NOT NULL,
	[BYMEMBER_ID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[TRIP_ID] ASC,
	[MEMBER_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[COSTINCURRED] ON 

INSERT [dbo].[COSTINCURRED] ([COST_ID], [NAME]) VALUES (1, N'Chỗ ở')
INSERT [dbo].[COSTINCURRED] ([COST_ID], [NAME]) VALUES (2, N'Di chuyển')
INSERT [dbo].[COSTINCURRED] ([COST_ID], [NAME]) VALUES (3, N'Khác')
SET IDENTITY_INSERT [dbo].[COSTINCURRED] OFF
GO
SET IDENTITY_INSERT [dbo].[LOCATION] ON 

INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (1, N'Lạc tiên giới', N'1/3 Đường Lâm Sinh, Phường 5, TP Đà Lạt', N'Chiêu đãi du khách bằng phong cảnh núi rừng nguyên sơ, bên trên có khinh khí cầu, bên dưới là hồ nước tĩnh lặng', 0)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (2, N'Bắc Thang Lên Hỏi Ông Trời', N'Thôn Túy Sơn, Xã Xuân Thọ, Đà Lạt', N'Là điểm đến “sống ảo” mới toanh của Đà Lạt', 0)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (3, N'Nông trại Sunny Farm', N'Dốc Số 7, Trại Mát, P. 11, Tp. Đà Lạt', N'Nnấc thang lên thiên đường, quán cà phê nhỏ xinh ở Sunny Farm chính là điểm nhấn thu hút du khách. Từ vị trí của Sunny Farm bạn có thể phóng tầm mắt chiêm ngưỡng vẻ đẹp của khu Trại Mát', 0)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (4, N'Bảo tàng tranh 3D Art in us', N'02-04, Đường số 9, KDC Himlam, Phường Tân Hưng, Quận 7, TP HCM', N'Là không gian để bạn bung tỏa hết sức sáng tạo và cảm xúc để tạo nên thế giới kỳ ảo của riêng mình qua từng bức ảnh chụp', 0)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (5, N'World Of Heineken', N'Bitexco Financial Tower, 2 Hải Triều, Bến Nghé, Hồ Chí Minh', N'Khám phá lịch sử phát triển hãng Heineken – hãng bia nổi tiếng thế giới', 0)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (6, N'Vietopia', N'Khu dân cư Him Lam, quận 7', N'Là mô hình công viên vui chơi kiểu mới, tạo môi trường cho trẻ trải nghiệm các ngành nghề một cách chân thật nhất', 0)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (7, N'Du lịch Tre Việt', N'25 Phan Văn Đáng, Phú Hữu, Nhơn Trạch, Đồng Nai', N'Tại đây với nhiều trò chơi hấp như chèo thuyền kayak, thuyền thúng, du thuyền bamboo, đạp xe trên sông hay vượt chướng ngại vật,…', 0)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (8, N'Khu du lịch Bửu Long', N'Huỳnh Văn Nghệ, Kp4, Bửu Long, Thành phố Biên Hòa, Đồng Nai', N'Là khu vui chơi lớn nhất ở Nha Trang có diện tích hơn 200,000 m2 đạt tiêu chuẩn quốc tế', 0)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (9, N'Viện Hải Dương học Nha Trang', N'01 Cầu Đá, Trần Phú, TP. Nha Trang', N'Địa điểm này hấp dẫn các em nhỏ bổ sung những kiến thức về các loài sinh vật biển và chiêm ngưỡng đủ các loại động vật biển khác nhau', 0)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (10, N'Vinpearl', N'Đảo Hòn Tre, phường Vĩnh Nguyên, thành phố Nha Trang, Khánh Hòa, Việt Nam', N'Nghỉ dưỡng, hòa mình với không khí biển', 0)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (11, N'Mù Cang Chải', N'Mù Cang Chải, Yên Bái ', N'Được giới trẻ hội tụ về đây rầm rộ để được đắm chìm trong sắc màu lấp lánh của mùa vàng trên núi', 0)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (12, N'Điện Biên', N'Điện Biên', N'Là 1 trong những địa điểm du lịch Tây Bắc tuyệt đẹp bởi hào khí chiến thắng', 0)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (13, N'Tháp Eiffel', N'Tháp Eiffel, Avenue Anatole France, Pa ri, Pháp', N'Là một công trình kiến trúc bằng thép nằm bên cạnh sông Seine.', 1)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (14, N'Quảng trường Concorde', N'Quảng trường Concorde, Pa ri, Pháp', N'Là một một trong những quảng trường nổi tiếng của nước Pháp nằm ở đại lộ Champs- Élysées bên bờ sông Seine', 1)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (15, N'Đại lộ Champs-Élysées', N'Đại lộ Champs-Élysées, Pa ri, Pháp', N'Là đại lộ lớn và nổi tiếng nhất của thành phố Paris, nối hai quảng trường Concorde và Etoile với nhiều cửa hàng thời trang cao cấp', 1)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (16, N'Đài tưởng niệm', N'Bảo tàng quốc gia gần 9/11 Memorial & Museum, Greenwich Street, Thành phố New York, Tiểu bang New York, Hoa Kỳ', N'Nơi đặt đài kỷ niệm và bảo tàng tưởng nhớ sự kiện khủng bố 11/9.', 1)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (17, N'Broadway', N'Broadway Theatre, Broadway, Thành phố New York, Tiểu bang New York, Hoa Kỳ', N'Con phố trải trải dài suốt dọc khu Manhattan. Một trong những khu mua sắm đông đúc nhất của nó là SoHo', 1)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (18, N'Núi Phú Sỹ', N'Phú Sĩ, Kitayama, Fujinomiya, Shizuoka, Nhật Bản', N'Luôn thiêng liêng, sẽ che chở, bảo vệ cho sự bình an và phồn thịnh', 1)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (19, N'Tháp Tokyo Tower', N'Tháp Tokyo, 4 Chome-2-8 Shibakoen, Minato, Tôkyô, Nhật Bản', N'Cảm giác choáng ngợp, bất ngờ trước một bức tranh đầy sắc màu của những ánh đèn nhấp nháy, phản ánh sự phồn hoa và hiện đại.', 1)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (20, N'Đền Kinkaku-ji ở Kyoto', N'Kinkaku-ji, 1 Kinkakujichō, Kita Ward, Kyōto, Nhật Bản', N'Được ôm trọn bởi thiên nhiên xanh thẳm, mát rượi, đền Kinkaku-ji ở Kyoto là một công trình kiến trúc nổi tiếng vô cùng.', 1)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (21, N'Cung điện Gyeongbokgung', N'161 Sajik-ro, Sejongno, Jongno-gu, Seoul, Hàn Quốc', N'Là cung điện lớn nhất và quan trọng nhất của Hàn Quốc', 1)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (22, N'Cung điện Changdeokgung', N'99 Yulgok-ro, Gwonnong-dong, Jongno-gu, Seoul, Hàn Quốc', N'Là cung điện lớn thứ 2 ở Seoul. Cung điện Changdeokgung nổi tiếng với một khu vườn bí mật lớn nơi có vô số ngôi đền linh thiêng', 1)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (23, N'Khu làng cổ Bukcheon Hanok', N'Gye-dong, Jongno-gu, Seoul, Hàn Quốc', N'Nằm giữa 2 cung điện Gyeongbokgung và Cung điện Changdeokgung là ngôi làng cổ nổi tiếng mang tên Bukcheon Hanok đẹp như tranh vẽ', 1)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (24, N'Asiatique The Riverfront', N'Charoenkrung Soi 72-76, Charoenkrung Rd, Wat Phrayakrai Dist', N'Là một khu phức hợp giải trí và mua sắm lớn bên cạnh sông Chao Phraya ở Bangkok', 1)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (25, N'Maeklong Railway Market', N'Kasem Sukhum Alley, Mae Klong, Samut Songkhram, 75000', N'Họp chợ trên đường ray xe lửa ư? Nghe có vẻ khó tin và đầy nguy hiểm nhưng đó là cách hoạt động của khu chợ trời nổi tiếng nhất Thái Lan', 1)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (26, N'Open House Central Embassy', N'Tầng 6, Central Embassy, 1031 Ploenchit Road, Pathumwan, Bangkok', N'Là một không gian mở cung cấp nhiều dịch vụ như nhà hàng, mua sắm, ăn uống.', 1)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (27, N'Tượng chúa Giêsu Kito Vua', N'Thùy Vân, Phường 2, Tp. Vũng Tàu, Bà Rịa – Vũng Tàu', N'Biểu tượng của thành phố biển, là một bức tượng Chúa Giêsu được đặt trên đỉnh Núi Nhỏ của thành phố Vũng Tàu', 0)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (28, N'Biển Long Hải', N'Nằm cách thành phố Vũng Tàu 12km', N'Là điểm đến không thể bỏ qua khi tới du lịch Vũng Tàu bởi bức tranh thiên nhiên hoang sơ tuyệt đẹp sẽ ', 0)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (29, N'Bãi biển Bãi Cháy', N'Bãi Cháy, Hạ Long, Quảng Ninh', N'Là bãi biển nhân tạo với chiều dài gần 1km, bãi biển rộng với cát trắng mịn, nước trong.', 0)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (30, N'Sun World Halong Complex', N'Số 9 Đường Hạ Long, Bãi Cháy, Hạ Long, Quảng Ninh.', N'Là tổ hợp vui chơi giải trí đẳng cấp quốc tế với tổng diện tích lên tới 214 ha', 0)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (31, N'Du lịch sinh thái dưới nước Cồn Quy', N'Xã Tân Thạch và Quới Sơn, huyện Châu Thành, tỉnh Bến Tre, Việt Nam', N'Nằm dọc theo con sông Tiền và cách trung tâm thành phố Bến Tre 23km là Cồn Quy. Đây là một trong những điểm đến nổi tiếng nhất khi nhắc đến Bến Tre', 0)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (32, N'Cồn Phụng', N'Cồn Phụng, Tân Thạch, Châu Thành, Bến Tre', N'Được bình chọn là khu du lịch tiêu biểu ở Đồng bằng sông Cửu Long,', 0)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (33, N'Cồn Phú Đa', N'Cồn Phú Đa, Vĩnh Bình, Chợ Lách, Bến Tre', N'Nổi bật với cảnh trí thiên nhiên đẹp, thiên nhiên trong lành trong hệ thống toàn bộ cồn nổi ở Bến Tre', 0)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (34, N'Thủ đô Athens', N'Athens, Hy Lạp', N'Là thành phố cổ lớn nhất của Hy Lạp với niên đại khoảng 3000 năm.', 1)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (35, N'Santorini ', N'Santorini , A-ten, Hy Lạp', N'Là một phần của quần đảo Cyclades và còn có tên gọi khác là Thira', 1)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (36, N'Thessaloniki', N'Thessaloniki, Hy Lạp', N'Là thành phố lớn thứ hai của đất nước này', 1)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (37, N'Đường sách Nguyễn Văn Bình', N'Nguyễn Văn Bình, quận 1, tp. HCM', N'Quy tụ các nhà xuất bản lớn nhất cả nước với những đầu sách hấp dẫn', 0)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (38, N'Lost Escape Room', N'Lầu 5 – Diamond Plaza – 34 Lê Duẩn – Quận 1 – Tp.Hồ Chí Minh', N'Là trò chơi nhập vai thực tế. Với nhiều chủ đề cho bạn lựa chọn', 0)
INSERT [dbo].[LOCATION] ([LOCATION_ID], [NAME], [ADDRESS], [DESCRIPTION], [TYPE]) VALUES (39, N'Snow Town', N' Căn hộ CBD Premium, 125 Đồng Văn Cống, Phường Thạnh Mỹ Lợi, Quận 2, Hồ Chí Minh', N'Cùng chìm đắm sắc trắng tinh khôi với những hoạt động đầy thú vị như trượt tuyết, nặn người tuyết hay chơi ném tuyết', 0)
SET IDENTITY_INSERT [dbo].[LOCATION] OFF
GO
SET IDENTITY_INSERT [dbo].[MEMBER] ON 

INSERT [dbo].[MEMBER] ([MEMBER_ID], [NAME], [PHONENUMBER], [GENDER], [AVATAR]) VALUES (1, N'Nguyễn Văn A', N'0123456789', 0, N'\\Image\\Member\\avatar01.jpg')
INSERT [dbo].[MEMBER] ([MEMBER_ID], [NAME], [PHONENUMBER], [GENDER], [AVATAR]) VALUES (2, N'Nguyễn Văn B', N'0123456788', 1, N'\\Image\\Member\\avatar02.jpg
')
INSERT [dbo].[MEMBER] ([MEMBER_ID], [NAME], [PHONENUMBER], [GENDER], [AVATAR]) VALUES (3, N'Ngô Nha Trang', N'0123456787', 0, N'\\Image\\Member\\avatar03.jpg
')
INSERT [dbo].[MEMBER] ([MEMBER_ID], [NAME], [PHONENUMBER], [GENDER], [AVATAR]) VALUES (4, N'Nguyễn Thành Vĩnh Phúc', N'0123456786', 1, N'\\Image\\Member\\avatar04.jpg
')
INSERT [dbo].[MEMBER] ([MEMBER_ID], [NAME], [PHONENUMBER], [GENDER], [AVATAR]) VALUES (5, N'Võ Thiện Tín', N'0123456785', 1, N'\\Image\\Member\\avatar05.jpg
')
INSERT [dbo].[MEMBER] ([MEMBER_ID], [NAME], [PHONENUMBER], [GENDER], [AVATAR]) VALUES (6, N'Võ Văn Hân', N'0123456755', 1, N'\\Image\\Member\\avatar06.jpg
')
INSERT [dbo].[MEMBER] ([MEMBER_ID], [NAME], [PHONENUMBER], [GENDER], [AVATAR]) VALUES (7, N'Đặng Thị Thúy Uyên', N'0123452873', 0, N'\\Image\\Member\\avatar07.jpg
')
INSERT [dbo].[MEMBER] ([MEMBER_ID], [NAME], [PHONENUMBER], [GENDER], [AVATAR]) VALUES (8, N'Kỳ Tuấn Khang', N'0123783445', 1, N'\\Image\\Member\\avatar08.jpg
')
INSERT [dbo].[MEMBER] ([MEMBER_ID], [NAME], [PHONENUMBER], [GENDER], [AVATAR]) VALUES (9, N'Trần Quang Huy', N'0729837199', 1, N'\\Image\\Member\\avatar09.jpg
')
INSERT [dbo].[MEMBER] ([MEMBER_ID], [NAME], [PHONENUMBER], [GENDER], [AVATAR]) VALUES (10, N'Trần Thị Kiều My', N'0987298374', 0, N'\\Image\\Member\\avatar10.jpg
')
SET IDENTITY_INSERT [dbo].[MEMBER] OFF
GO
SET IDENTITY_INSERT [dbo].[TRIP] ON 

INSERT [dbo].[TRIP] ([TRIP_ID], [TITTLE], [DESCRIPTION], [THUMNAILIMAGE], [CURRENTPROCEEDS], [TOTALCOSTS], [TOGODATE], [RETURNDATE], [ISDONE]) VALUES (1, N'Chuyến đi Đà Lạt đầu tiên', N'Chuyến đi đầu tiên của cả nhà', N'\\Image\\Trip\\1\\1.jpg', 2150000, 2150000, CAST(N'2018-10-10T00:00:00.000' AS DateTime), CAST(N'2018-10-15T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[TRIP] ([TRIP_ID], [TITTLE], [DESCRIPTION], [THUMNAILIMAGE], [CURRENTPROCEEDS], [TOTALCOSTS], [TOGODATE], [RETURNDATE], [ISDONE]) VALUES (2, N'Hồ Chí Minh thành phố anh hùng', N'Chuyến đi đầu tiên đến Tp.HCM', N'\\Image\\Trip\\2\\1.jpg', 2400000, 2400000, CAST(N'2019-09-11T00:00:00.000' AS DateTime), CAST(N'2019-09-15T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[TRIP] ([TRIP_ID], [TITTLE], [DESCRIPTION], [THUMNAILIMAGE], [CURRENTPROCEEDS], [TOTALCOSTS], [TOGODATE], [RETURNDATE], [ISDONE]) VALUES (3, N'Huyện Long Thành không có bò sữa', N'Huyện của điểm dừng chân vinamilk', N'\\Image\\Trip\\3\\1.png', 1789000, 1850000, CAST(N'2021-11-11T00:00:00.000' AS DateTime), CAST(N'2021-11-15T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[TRIP] ([TRIP_ID], [TITTLE], [DESCRIPTION], [THUMNAILIMAGE], [CURRENTPROCEEDS], [TOTALCOSTS], [TOGODATE], [RETURNDATE], [ISDONE]) VALUES (4, N'Thành phố Vũng Tàu mặn chát', N'Đến đây tắm biển nghỉ ngơi', N'\\Image\\Trip\\4\\1.jpg', 1800000, 1900000, CAST(N'2020-09-10T00:00:00.000' AS DateTime), CAST(N'2020-09-25T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[TRIP] ([TRIP_ID], [TITTLE], [DESCRIPTION], [THUMNAILIMAGE], [CURRENTPROCEEDS], [TOTALCOSTS], [TOGODATE], [RETURNDATE], [ISDONE]) VALUES (5, N'Vùng núi Tây Bắc mộng mơ', N'Đến đây trốn đô thị', N'\\Image\\Trip\\5\\1.jpg', 1460000, 1450000, CAST(N'2018-08-10T00:00:00.000' AS DateTime), CAST(N'2018-08-15T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[TRIP] ([TRIP_ID], [TITTLE], [DESCRIPTION], [THUMNAILIMAGE], [CURRENTPROCEEDS], [TOTALCOSTS], [TOGODATE], [RETURNDATE], [ISDONE]) VALUES (6, N'Thành phố lãng mạng paris', N'Đi ra nước ngoài lần đầu tiên', N'\\Image\\Trip\\6\\1.jpg', 2600000, 2550000, CAST(N'2016-08-10T00:00:00.000' AS DateTime), CAST(N'2016-08-15T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[TRIP] ([TRIP_ID], [TITTLE], [DESCRIPTION], [THUMNAILIMAGE], [CURRENTPROCEEDS], [TOTALCOSTS], [TOGODATE], [RETURNDATE], [ISDONE]) VALUES (7, N'Tour đi nước ngoài', N'Đi nhật bản và hàn quốc', N'\\Image\\Trip\\7\\1.jpg', 3800000, 3800000, CAST(N'2021-01-10T00:00:00.000' AS DateTime), CAST(N'2021-01-20T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[TRIP] ([TRIP_ID], [TITTLE], [DESCRIPTION], [THUMNAILIMAGE], [CURRENTPROCEEDS], [TOTALCOSTS], [TOGODATE], [RETURNDATE], [ISDONE]) VALUES (8, N'Đi thăm NewYork', N'Đi nước ngoài chơi', N'\\Image\\Trip\\9\\15399205.jpg', 2420000, 2150000, CAST(N'2020-05-20T00:00:00.000' AS DateTime), CAST(N'2020-05-28T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[TRIP] ([TRIP_ID], [TITTLE], [DESCRIPTION], [THUMNAILIMAGE], [CURRENTPROCEEDS], [TOTALCOSTS], [TOGODATE], [RETURNDATE], [ISDONE]) VALUES (9, N'Đất nước chùa vàng Thái Lan', N'Đi thăm những ngôi chùa cổ kính', N'\\Image\\Trip\\10\\1.jpg', 1950000, 1950000, CAST(N'2017-07-10T00:00:00.000' AS DateTime), CAST(N'2017-07-15T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[TRIP] ([TRIP_ID], [TITTLE], [DESCRIPTION], [THUMNAILIMAGE], [CURRENTPROCEEDS], [TOTALCOSTS], [TOGODATE], [RETURNDATE], [ISDONE]) VALUES (10, N'Đi Biển ở Nha Trang xinh đẹp', N'Đi nghỉ dưỡng biển lần 2', N'\\Image\\Trip\\11\\1.jpg', 2200000, 2200000, CAST(N'2020-10-10T00:00:00.000' AS DateTime), CAST(N'2020-10-25T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[TRIP] ([TRIP_ID], [TITTLE], [DESCRIPTION], [THUMNAILIMAGE], [CURRENTPROCEEDS], [TOTALCOSTS], [TOGODATE], [RETURNDATE], [ISDONE]) VALUES (11, N'Thành phố Hạ Long ', N'Tham quan kỳ quan thế giới', N'\\Image\\Trip\\12\\1.jpg', 1916000, 1900000, CAST(N'2021-02-02T00:00:00.000' AS DateTime), CAST(N'2021-02-10T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[TRIP] ([TRIP_ID], [TITTLE], [DESCRIPTION], [THUMNAILIMAGE], [CURRENTPROCEEDS], [TOTALCOSTS], [TOGODATE], [RETURNDATE], [ISDONE]) VALUES (12, N'Thành phố kẹo dừa Bến Tre', N'Thăm nhà bạn lần đầu', N'\\Image\\Trip\\13\\1.jpg', 1450000, 1450000, CAST(N'2021-03-03T00:00:00.000' AS DateTime), CAST(N'2021-03-05T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[TRIP] ([TRIP_ID], [TITTLE], [DESCRIPTION], [THUMNAILIMAGE], [CURRENTPROCEEDS], [TOTALCOSTS], [TOGODATE], [RETURNDATE], [ISDONE]) VALUES (13, N'Thành phố Hy Lạp cổ đại', N'Thành phố với bề dày lịch sử thú vị', N'\\Image\\Trip\\14\\1.jpg', 2550000, 2550000, CAST(N'2021-04-10T00:00:00.000' AS DateTime), CAST(N'2021-04-20T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[TRIP] ([TRIP_ID], [TITTLE], [DESCRIPTION], [THUMNAILIMAGE], [CURRENTPROCEEDS], [TOTALCOSTS], [TOGODATE], [RETURNDATE], [ISDONE]) VALUES (14, N'Đi chơi Sài Gòn', N'Đi thăm lại trường cũ', N'\\Image\\Trip\\15\\1.jpg', 2800000, 2800000, CAST(N'2021-06-10T00:00:00.000' AS DateTime), CAST(N'2021-06-15T00:00:00.000' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[TRIP] OFF
GO
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (1, 1, 200000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (1, 2, 300000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (1, 3, 300000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (1, 4, 300000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (1, 5, 300000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (1, 6, 200000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (1, 7, 350000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (1, 8, 200000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (1, 9, 250000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (1, 10, 250000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (1, 11, 300000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (1, 12, 300000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (1, 13, 200000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (1, 14, 350000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (2, 1, 200000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (2, 2, 300000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (2, 3, 300000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (2, 4, 300000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (2, 5, 200000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (2, 6, 200000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (2, 7, 350000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (2, 8, 200000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (2, 9, 250000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (2, 10, 200000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (2, 11, 300000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (2, 12, 200000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (2, 13, 200000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (2, 14, 350000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (3, 3, 300000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (3, 6, 350000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (3, 7, 350000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (3, 13, 350000)
INSERT [dbo].[TRIP_COSTINCURRED] ([COST_ID], [TRIP_ID], [COST]) VALUES (3, 14, 350000)
GO
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (1, N'\\Image\\Trip\\1\\List\\1554722331_du-lich-da -lat-ngam-hoa-3.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (1, N'\\Image\\Trip\\1\\List\\3.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (1, N'\\Image\\Trip\\1\\List\\4.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (1, N'\\Image\\Trip\\1\\List\\du-lich-da-lat-cuoi-tuan-cover.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (2, N'\\Image\\Trip\\2\\List\\1200px-Saigon_skyline_night_view.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (2, N'\\Image\\Trip\\2\\List\\foody-mobile-t-2-jpg-900-635651430361525210.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (2, N'\\Image\\Trip\\2\\List\\Think-Vietnam-HoChiMinh-480125692-TonyNg-copy.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (2, N'\\Image\\Trip\\2\\List\\VNMSGN03.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (3, N'\\Image\\Trip\\3\\List\\2.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (3, N'\\Image\\Trip\\3\\List\\3.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (3, N'\\Image\\Trip\\3\\List\\dji0009-1556209146004450410195.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (4, N'\\Image\\Trip\\4\\List\\1_Top_5_Beaches_in_Nha_Trang_-_By_Simon_Dannhauer.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (4, N'\\Image\\Trip\\4\\List\\download.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (4, N'\\Image\\Trip\\4\\List\\nha-trang-beaches.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (5, N'\\Image\\Trip\\5\\List\\1583723559.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (5, N'\\Image\\Trip\\5\\List\\download.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (5, N'\\Image\\Trip\\5\\List\\ki-su-tay-bac-6.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (5, N'\\Image\\Trip\\5\\List\\ng-tay-bac.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (6, N'\\Image\\Trip\\6\\List\\covid-paris-france-daniels-23.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (6, N'\\Image\\Trip\\6\\List\\Marais-Paris-Notre-Dame-Cathedral.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (6, N'\\Image\\Trip\\6\\List\\marquee-destinations-paris-640x640.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (6, N'\\Image\\Trip\\6\\List\\Paris-like-a-Parisian_568x464.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (7, N'\\Image\\Trip\\7\\List\\MG_1_1_New_York_City-1.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (7, N'\\Image\\Trip\\7\\List\\new-york-main-data.webp
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (7, N'\\Image\\Trip\\8\\List\\iStock_000057240506_Large.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (7, N'\\Image\\Trip\\8\\List\\japan.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (7, N'\\Image\\Trip\\8\\List\\shutterstock_1017748132-L.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (8, N'\\Image\\Trip\\9\\List\\4500.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (8, N'\\Image\\Trip\\9\\List\\HK_1_N3.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (8, N'\\Image\\Trip\\9\\List\\image.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (8, N'\\Image\\Trip\\9\\List\\SK-hero.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (9, N'\\Image\\Trip\\10\\List\\02b0a5fb1de5d443e66f38a9b636eec9.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (9, N'\\Image\\Trip\\10\\List\\6a5b75257fb544cdaa3c22a644bdc368.jpeg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (9, N'\\Image\\Trip\\10\\List\\77705127.cms
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (9, N'\\Image\\Trip\\10\\List\\shutterstock_1566472765.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (10, N'\\Image\\Trip\\11\\List\\20200430_153047_ijfi.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (10, N'\\Image\\Trip\\11\\List\\nu-cuoi-viet-du-lich-vung-tau-09.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (10, N'\\Image\\Trip\\11\\List\\Tam-bien-Vung-Tau-11-1595679828.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (11, N'\\Image\\Trip\\12\\List\\du-lich-ha-long.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (11, N'\\Image\\Trip\\12\\List\\hinh-anh-du-lich-ha-long.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (12, N'\\Image\\Trip\\13\\List\\1_204056_02.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (12, N'\\Image\\Trip\\13\\List\\Ben_Tre_city.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (12, N'\\Image\\Trip\\13\\List\\Du-lich-Ben-Tre.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (13, N'\\Image\\Trip\\14\\List\\1560501522-product-slide-0.jpeg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (13, N'\\Image\\Trip\\14\\List\\hy-lap-1.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (13, N'\\Image\\Trip\\14\\List\\images.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (14, N'\\Image\\Trip\\15\\List\\HCM-3.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (14, N'\\Image\\Trip\\15\\List\\kinh-nghiem-du-lich-sai-gon-00.jpg
')
INSERT [dbo].[TRIP_IMAGES] ([TRIP_ID], [IMAGE]) VALUES (14, N'\\Image\\Trip\\15\\List\\Sai-Gon-1-1588932059.jpg
')
GO
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (1, 1, 800000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (1, 2, 500000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (1, 3, 450000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (2, 4, 500000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (2, 5, 800000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (2, 6, 500000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (3, 7, 450000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (3, 8, 500000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (4, 9, 800000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (4, 10, 500000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (5, 11, 450000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (5, 12, 500000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (6, 13, 800000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (6, 14, 500000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (6, 15, 500000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (7, 16, 450000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (7, 17, 500000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (7, 18, 800000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (7, 19, 500000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (7, 20, 500000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (8, 21, 450000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (8, 22, 500000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (8, 23, 800000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (9, 24, 500000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (9, 25, 450000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (9, 26, 500000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (10, 27, 800000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (10, 28, 500000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (10, 29, 450000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (11, 30, 500000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (11, 31, 800000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (12, 32, 500000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (12, 33, 450000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (13, 34, 500000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (13, 35, 800000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (13, 36, 500000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (14, 37, 450000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (14, 38, 500000)
INSERT [dbo].[TRIP_LOCATION] ([TRIP_ID], [LOCATION_ID], [COSTS]) VALUES (14, 39, 800000)
GO
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (1, 1, 537000, 2)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (1, 2, 537000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (1, 3, 537000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (1, 4, 539000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (2, 5, 480000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (2, 6, 480000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (2, 7, 480000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (2, 8, 480000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (2, 9, 480000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (3, 1, 463000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (3, 2, 463000, 3)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (3, 3, 400000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (3, 10, 463000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (4, 4, 450000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (4, 5, 450000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (4, 6, 450000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (4, 7, 450000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (5, 8, 480000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (5, 9, 480000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (5, 10, 500000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (6, 1, 500000, 2)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (6, 2, 500000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (6, 3, 500000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (6, 4, 500000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (6, 5, 600000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (7, 6, 900000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (7, 7, 900000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (7, 8, 900000, 9)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (7, 9, 1100000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (8, 1, 710000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (8, 2, 1000000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (8, 10, 710000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (9, 3, 390000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (9, 4, 390000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (9, 5, 390000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (9, 6, 390000, 8)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (9, 7, 390000, 8)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (10, 8, 733000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (10, 9, 733000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (10, 10, 734000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (11, 1, 633000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (11, 2, 633000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (11, 3, 650000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (12, 3, 725000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (12, 4, 725000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (13, 6, 830000, 7)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (13, 7, 830000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (13, 8, 890000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (14, 1, 650000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (14, 2, 650000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (14, 9, 650000, NULL)
INSERT [dbo].[TRIP_MEMBER] ([TRIP_ID], [MEMBER_ID], [AMOUNTPAID], [BYMEMBER_ID]) VALUES (14, 10, 850000, NULL)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__LOCATION__5BE22BC37A0D84CE]    Script Date: 12/17/2020 9:55:20 PM ******/
ALTER TABLE [dbo].[LOCATION] ADD UNIQUE NONCLUSTERED 
(
	[ADDRESS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__LOCATION__D9C1FA00195A9C10]    Script Date: 12/17/2020 9:55:20 PM ******/
ALTER TABLE [dbo].[LOCATION] ADD UNIQUE NONCLUSTERED 
(
	[NAME] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TRIP] ADD  DEFAULT ((0)) FOR [CURRENTPROCEEDS]
GO
ALTER TABLE [dbo].[TRIP] ADD  DEFAULT ((0)) FOR [TOTALCOSTS]
GO
ALTER TABLE [dbo].[TRIP_LOCATION] ADD  DEFAULT ((0)) FOR [COSTS]
GO
ALTER TABLE [dbo].[TRIP_MEMBER] ADD  DEFAULT ((0)) FOR [AMOUNTPAID]
GO
ALTER TABLE [dbo].[TRIP_COSTINCURRED]  WITH CHECK ADD FOREIGN KEY([COST_ID])
REFERENCES [dbo].[COSTINCURRED] ([COST_ID])
GO
ALTER TABLE [dbo].[TRIP_COSTINCURRED]  WITH CHECK ADD FOREIGN KEY([TRIP_ID])
REFERENCES [dbo].[TRIP] ([TRIP_ID])
GO
ALTER TABLE [dbo].[TRIP_IMAGES]  WITH CHECK ADD  CONSTRAINT [FK__TRIP_IMAG__TRIP___5070F446] FOREIGN KEY([TRIP_ID])
REFERENCES [dbo].[TRIP] ([TRIP_ID])
GO
ALTER TABLE [dbo].[TRIP_IMAGES] CHECK CONSTRAINT [FK__TRIP_IMAG__TRIP___5070F446]
GO
ALTER TABLE [dbo].[TRIP_LOCATION]  WITH CHECK ADD FOREIGN KEY([LOCATION_ID])
REFERENCES [dbo].[LOCATION] ([LOCATION_ID])
GO
ALTER TABLE [dbo].[TRIP_LOCATION]  WITH CHECK ADD FOREIGN KEY([TRIP_ID])
REFERENCES [dbo].[TRIP] ([TRIP_ID])
GO
ALTER TABLE [dbo].[TRIP_MEMBER]  WITH CHECK ADD FOREIGN KEY([BYMEMBER_ID])
REFERENCES [dbo].[MEMBER] ([MEMBER_ID])
GO
ALTER TABLE [dbo].[TRIP_MEMBER]  WITH CHECK ADD FOREIGN KEY([MEMBER_ID])
REFERENCES [dbo].[MEMBER] ([MEMBER_ID])
GO
ALTER TABLE [dbo].[TRIP_MEMBER]  WITH CHECK ADD FOREIGN KEY([TRIP_ID])
REFERENCES [dbo].[TRIP] ([TRIP_ID])
GO
USE [master]
GO
ALTER DATABASE [WESPLITAPP] SET  READ_WRITE 
GO