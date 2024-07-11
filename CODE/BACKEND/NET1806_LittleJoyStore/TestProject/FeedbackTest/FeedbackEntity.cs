using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.FeedbackTest
{
    public class FeedbackEntity
    {
        public int UserId { get; set; }

        public int ProductId { get; set; }

        public string? Comment { get; set; }

        public int? Rating { get; set; }

        public int ResponseCode { get; set; }
        

    }

    public class DeleteFeedbackEntity
    {
        public int Id { get; set;}
        public int UserId { get; set; }
        public int ResponseCode { get; set; }
    }

    public class UpdateFeedbackEntity
    {
        public int UserId { get; set; }

        public int Id { get; set; }

        public string? Comment { get; set; }

        public int? Rating { get; set; }

        public int ResponseCode { get; set; }
    }


}
