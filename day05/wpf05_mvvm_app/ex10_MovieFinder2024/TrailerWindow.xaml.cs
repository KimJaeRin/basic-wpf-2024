using System.Windows;
using ex10_MovieFinder2024.Models;
using Google.Apis.YouTube.v3;
using MahApps.Metro.Controls;
using Google.Apis.Services;
using System.Diagnostics;

namespace ex10_MovieFinder2024
{
    /// <summary>
    /// TrailerWindwo.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TrailerWindow : MetroWindow
    {
        List<YoutubeItem> youtubeItems = null;
        public TrailerWindow()
        {
            InitializeComponent();
        }

        // MainWindow 그리드에서 건택된 영화제목 넘기면서 생성
        // 재정의 생성자
        public TrailerWindow(string movieName) : this()
        {
            // this() -> TrailerWindow() 생성자를 먼저 실행한 뒤
            LblMovieName.Content = $"{movieName} 예고편";
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            youtubeItems = new List<YoutubeItem>();
            SearchYoutubeApi();
           
        }

        private async void SearchYoutubeApi()
        {
            await LoadDaraCollection();
            LsvResult.ItemsSource = youtubeItems;

            LsvResult.SelectedIndex = 0;
        }

        private async Task LoadDaraCollection()
        {
            var service = new YouTubeService(
                new BaseClientService.Initializer()
                {
                    ApiKey = "AIzaSyBtzVD4azXevjZ9uMT8mldqd7ckwvbeQYw",
                    ApplicationName = this.GetType().ToString()
                });
            var req = service.Search.List("snippet");
            req.Q = LblMovieName.Content.ToString(); // 어벤져스 인피니티워 예고편
            req.MaxResults = 10;

            var res = await req.ExecuteAsync(); // Youtube서버에서 요청된 값 실행하고 결과 리턴(비동기)

            //   await this.ShowMessageAsync("검색결과", res.Items.Count.ToString());
            foreach (var item in res.Items)
            {
                if (item.Id.Kind.Equals("youtube#video"))
                {
                    var youtube = new YoutubeItem()
                    {
                        Title = item.Snippet.Title,
                        ChannelTitle = item.Snippet.ChannelTitle,
                        URL = $"https://www.youtube.com/watch?v={item.Id.VideoId}",// 유튜브 플레이 링크
                        Author = item.Snippet.ChannelId,
                    };

                    youtube.Thumbnail = new System.Windows.Media.Imaging.BitmapImage(new Uri(item.Snippet.Thumbnails.Default__.Url, UriKind.RelativeOrAbsolute));
                    youtubeItems.Add(youtube);
                }
                
            }
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           // 한번씩 CefSharp 브라우저에서 메모리 릭 발생
            BrsYoutube.Address = string.Empty;
            BrsYoutube.Dispose();  // 종종 앱 종료시 객체 완전해제하기위해

        }

        private void LsvResult_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(LsvResult.SelectedItem is YoutubeItem) // is => true/false
            {
                var video = LsvResult.SelectedItem as YoutubeItem;
                Debug.WriteLine(video.URL);
                BrsYoutube.Address = video.URL;
            }
        }
    }
}
