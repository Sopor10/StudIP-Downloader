namespace StudIPDownloader.Domain
{
    class Semester
    {
        public string name;
        public string id;
        public bool active;
        public Semester(bool active)
        {
            this.active = active; ;
        }
        public Semester(string id, string name, bool active)
        {
            this.id = id;
            this.name = name;
            this.active = active;
        }
    }
}
