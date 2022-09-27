namespace Domain
{
    public class Question
    {
        public string Topic { get; set; } = string.Empty;
        public int Index { get; set; }
        public string Link { get; set; } = string.Empty;

        public Question(string topic, int index, string link)
        {
            Topic = topic;
            Index = index;
            Link = link;
        }
    }
}