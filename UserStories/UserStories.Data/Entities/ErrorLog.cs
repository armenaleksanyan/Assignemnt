﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStories.Data.Entities
{
    public class ErrorLog
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public long UserId { get; set; }
        public DateTime RecDate { get; set; }
    }
}
