﻿using System;

namespace T1098897.Models {
    public class Item {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Value { get; set; }
    }
}