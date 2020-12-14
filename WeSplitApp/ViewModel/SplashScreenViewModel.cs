using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSplitApp.Utils;

namespace WeSplitApp.ViewModel
{
    public class SplashScreenViewModel : ViewModel
    {
        public ObservableCollection<string> FactsList { get; set; }
        public string RandomFact { get; set; }
        public SplashScreenViewModel()
        {
            //Init facts list
            this.FactsList = new ObservableCollection<string>()
            {
                "Bali là hòn đảo du lịch nổi tiếng nhất Indonesia. Đảo được mệnh danh là đảo thiên đường với rất nhiều thắng cảnh, đền, đài và các bãi biển đẹp thơ mộng. Ngoài ra, ở Bali còn có nền văn hóa, nghệ thuật lâu đời rất đặc sắc của người dân bản địa. Nếu có dịp đến Bali, bạn nên ghé thăm các đền, chùa, tham gia các hoạt động lặn biển, chèo thuyền vượt thác, leo núi lửa,…Hoặc đi thăm khu rừng khỉ Monkey Forest, ruộng lúa bậng thang,…rất thú vị.",
                "London nổi tiếng với cung điện Bukingham, chợ Camden, và các đồ trang sức được làm bằng ngọc trai quý hiếm. Ở London có sự giao thoa, kết hợp giữa nghệ thuật, thời trang, ẩm thực và vị bia truyền thống bản địa (gọi là ale). Nếu bạn là 1 người thích tìm hiểu về văn hóa thì không nên bỏ lỡ Tate Modern và nhà hát hoàng gia (Royal Opera House). Nếu bạn yêu thích thời trang thì Oxford Street là khu mua sắm thích hợp. Với những bạn sành ăn thì nên thử qua trà kem ở Harrod’s hoặc cá và khoai tây chiên giòn từ các cửa hàng fish & chips có ở khắp nơi (gọi là chippy)",
                "Paris còn được biết đến như thủ đô hoa lệ, kinh đô ánh sáng với rất nhiều địa điểm vui chơi, giải trí hấp dẫn. Biểu tượng của thủ đô Paris nước Pháp là tháp Eiffel và Khải hoàn môn (Arc de Triomphe). Ngoài ra bạn cũng có thể ghé thăm bảo tàng nghệ thuật, lịch sử Louvre và nhà thờ Đức bà Paris nổi tiếng (Notre Dame). Nếu là tín đồ ăn uống, bạn có thể ghé thăm chợ Marché Biologique Raspail và mua sắm tại chợ Marché aux Puces de Montreuil.",
                "Rome (hay Roma) là thủ đô nước Ý với bề dày lịch sử hơn 2500 năm phong phú. Rome còn là biểu tượng của nước Ý với rất nhiều danh lam thắng cảnh nổi tiếng như đài phun nước Trevi, đấu trường La mã Colosseum, đền Pantheon, quãng trường Venice, Navona, Numa, bảo tàng Vatican và rất nhiều bảo tàng khác nữa. Thành phố này còn nổi tiếng với các khu chợ ngoài trời, quãng trường và các đài phun nước. Bạn có thể thưởng thức hương vị cafe espresso và gelato hoặc mua sắm tại Campo de’Fiori hoặc Via Veneto.",
                "Bạn không thể nào khám phá hết New York chỉ trong 1, 2 lần ghé thăm thành phố này được. Thay vào đó là nên ưu tiên những điểm đến hot nhất như tòa nhà Empire State, tượng Nữ thần tự do, công viên trung tâm Central Park, bảo tàng nghệ thuật Metropolitan, quãng trường thời đại Times Square và rất nhiều địa điểm nổi tiếng khác. New York có hơn 36% cư dân thành phố được sinh ra bên ngoài nước Mỹ và hơn 170 ngôn ngữ được nói trong thành phố. Do đó nền văn hóa nghệ thuật, ẩm thực ở New York cũng rất phong phú, đa dạng và đặc sắc.",
                "Crete là hòn đảo lớn nhất và đông dân nhất của Hy Lạp. Theo truyền thuyết thì đây còn là nơi sinh của Zeus (vua của các vị thần Olympian) và vị vua của nền văn minh Châu Âu hiện đại. Hòn đảo này được ví như 1 viên ngọc ở Địa Trung Hải với rất nhiều thần thoại và giá trị lịch sử khảo cổ học. Đảo Crete có nền văn hóa lâu đời nhất Hy Lạp và Châu Âu, trong đó có nền văn minh Minos. Nếu có dịp đến Crete, bạn có thể ghé thăm cung điện Knossos, tu viện Preveli hoặc cây Olive ngàn năm.",
                "Barcelona được biết đến nhiều hơn với tên tuổi của đội bóng nổi tiếng FB Barcelona. Tuy nhiên, đây là 1 trong 2 thành phố lớn nhất Tây Ban Nha, cùng với thành phố Madrid. Barcelona còn được biết đến nhờ những công trình kiến trúc độc đáo của 2 kiến trúc sư danh tiếng Antoni Gaudí và Salvador Dalí. Một số địa điểm nổi tiếng bạn nên ghé thăm khi đến Barcelona như nhà thờ La Sagrada Familia, bảo tàng nghệ thuật Picasso Barcelona. Hoặc dạo chơi trên con phố nổi tiếng La Rambla và thưởng thức món Tapas đặc trưng ở Barcelona.",
                "Siem Reap nổi tiếng thế giới với quần thể đền Angkor Wat khổng lồ. Đây là quần thể đền đài và di tích lớn nhất và là 1 trong 7 kỳ quan của thế giới. Nổi bật nhất trong quần thể đền Angkor là 3 ngôi đền: Angkor Wat, Angkor Thom và Ta Prohm. Để đi thong thả và đầy đủ các địa điểm trong Angkor, bạn cần ít nhất từ 2,3 ngày để khám phá. Trải nghiệm ngắm bình minh hoặc hoàng hôn cũng là điều thú vị tại đây. Ban đêm, bạn có thể hòa vào không khí sôi động tại khu phố Tây, Pub Street hoặc mua sắm ở 2 khu chợ là Old Market và New Market.",
                "Prague là một trong những thành phố du lịch đẹp nhất Châu Âu. Thành phố là hiện thân cho kiểu kiến trúc thời trung cổ. Đây cũng là thủ đô của cộng hòa Séc với bề dạy văn hóa, lịch sử lâu đời rất phong phú. Bạn có thể dành cả ngày để khám phá lâu đài Praha (Prazsky hrad) hoặc tham quan cây cầu Charles, khu Phố cổ, khu Do Thái,…Thưởng thức bữa tối tại các quán bar và những quầy rựu cổ điển của Séc thường được tìm thấy ở các hầm rượu lâu đời.",
                "Phuket là một trong những hòn đảo du lịch nổi tiếng nhất ở Thái Lan. Đây cũng là hòn đảo lớn nhất ở Thái. Đảo Phuket nổi tiếng với những bài biển dài, đẹp, cát trắng mịn, nước biển trong xanh cùng với những hàng dừa thẳng vút. Hòn đảo là nơi lí tưởng cho mọi hoạt động du lịch từ nghỉ dưỡng, khám phá, cho đến trăng mật lãng mạn. Bạn cũng có thể tham gia các hoạt động thể thao dưới nước như lặn biển, cano, lướt ván. Tất tần tật các dịch vụ du lịch bạn đều có thể tìm thấy trên hòn đảo này."
            };
            
            //Init random fact
            var count = this.FactsList.Count;
            var index = MyRandom.Instance.Next(count);
            this.RandomFact = this.FactsList[index];
        }
    }
}
