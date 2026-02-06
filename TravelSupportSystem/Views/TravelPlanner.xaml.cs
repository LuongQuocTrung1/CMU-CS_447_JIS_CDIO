using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TravelSupportSystem.Core;

namespace TravelSupportSystem.Views
{
    /// <summary>
    /// Interaction logic for TravelPlanner.xaml
    /// </summary>
    ///private int step = 0;

    public partial class TravelPlanner : Window
    {
        private int step = 0;

        public TravelPlanner()
        {
            InitializeComponent();
            BotBubble(GetGreetingByStyle());
            BotBubble("Bạn dự kiến đi du lịch bao nhiêu ngày?");
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            string userText = txtUser.Text.Trim();
            if (string.IsNullOrEmpty(userText) || userText == "Aa") return;

            UserBubble(userText);
            txtUser.Clear();

            if (!IsTravelRelated(userText))
            {
                BotBubble("Xin lỗi, mình là hệ thống hỗ trợ du lịch nên chỉ trả lời các vấn đề liên quan đến du lịch.");
                return;
            }

            switch (step)
            {
                case 0:
                    BotBubble("Ngân sách dự kiến của bạn khoảng bao nhiêu?");
                    step++;
                    break;

                case 1:
                    BotBubble("Mình sẽ gợi ý lịch trình phù hợp tại Đà Nẵng cho bạn nhé 😊");
                    step++;
                    break;

                default:
                    BotBubble("Nếu bạn cần thêm gợi ý ăn uống, khách sạn hoặc địa điểm tham quan, cứ hỏi mình nha!");
                    break;
            }
        }

        private bool IsTravelRelated(string text)
        {
            string[] keywords = { "du lịch", "đi", "khách sạn", "ăn", "chơi", "địa điểm", "Đà Nẵng" };
            return keywords.Any(k => text.ToLower().Contains(k.ToLower()));
        }

        private void BotBubble(string text)
        {
            ChatPanel.Children.Add(CreateBubble(text, "#E4E6EB", HorizontalAlignment.Left));
        }

        private void UserBubble(string text)
        {
            ChatPanel.Children.Add(CreateBubble(text, "#2196F3", HorizontalAlignment.Right, true));
        }

        private Border CreateBubble(string text, string bg, HorizontalAlignment align, bool isUser = false)
        {
            return new Border
            {
                Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(bg)),
                CornerRadius = new CornerRadius(15),
                Padding = new Thickness(10),
                Margin = new Thickness(5),
                HorizontalAlignment = align,
                Child = new TextBlock
                {
                    Text = text,
                    Foreground = isUser ? Brushes.White : Brushes.Black,
                    TextWrapping = TextWrapping.Wrap,
                    MaxWidth = 220
                }
            };
        }

        private string GetGreetingByStyle()
        {
            switch (AppState.TravelStyle)
            {
                case "Tiết kiệm":
                    return "Chào bạn 👋 Mình sẽ giúp bạn lên kế hoạch du lịch Đà Nẵng tiết kiệm nhất.";
                case "Nghỉ dưỡng":
                    return "Xin chào 🌴 Mình sẽ đồng hành cùng bạn trong chuyến nghỉ dưỡng tại Đà Nẵng.";
                case "Phượt":
                    return "Hey 👋 Sẵn sàng khám phá Đà Nẵng theo kiểu phượt chưa?";
                default:
                    return "Xin chào! Mình là trợ lý du lịch Đà Nẵng.";
            }
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            new Home().Show();
            this.Close();
        }
    }
}
