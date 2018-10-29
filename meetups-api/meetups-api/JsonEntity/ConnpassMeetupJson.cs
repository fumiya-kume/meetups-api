namespace meetupsApi.JsonEntity
{
    public class ConnpassMeetupJson
    {
        public int results_returned { get; set; }
        public Event[] events { get; set; }
        public int results_start { get; set; }
        public int results_available { get; set; }
    }
}
