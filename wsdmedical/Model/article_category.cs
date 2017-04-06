﻿using System;
namespace Model
{
    public class article_category
    {

        public int id { get; set; }

        public int channel_id { get; set; }

        public string title { get; set; }

        public int parent_id { get; set; }
        public int sort_id { get; set; }

        public int class_layer { get; set; }

        public article_category()
        {
            id = 0;
            channel_id = 0;
            title = string.Empty;
            sort_id = 0;
            parent_id = 0;
            class_layer = 0;
        }

    }
}