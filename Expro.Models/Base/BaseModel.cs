﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{
    public abstract class BaseModel
    {
        public int ID { get; set; }
    }
}
