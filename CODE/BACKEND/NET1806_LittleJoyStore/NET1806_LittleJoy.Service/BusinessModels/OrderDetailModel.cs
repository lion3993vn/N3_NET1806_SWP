﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET1806_LittleJoy.Service.BusinessModels
{
    public class OrderDetailModel
    {
        public int Id { get; set; }

        public int? OrderId { get; set; }

        public int? ProductId { get; set; }

        public int? Price { get; set; }

        public int? Quantity { get; set; }
    }
}
