﻿namespace StudIPDownloader.Domain
{
    class Datei
    {
        public string id;
        public string filename;
        public int size;
        public int chdate;
        public Datei(string id, string filename, int size, int chdate)
        {
            this.id = id;
            this.filename = filename;
            this.size = size;
            this.chdate = chdate;
        }
    }
}
