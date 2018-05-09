using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListWordsToLearn.Common.DB.Model
{
    public class WordDetailModel : BaseModel
    {
        public string NameWordPl { get; set; }
        public string NameWordEn { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
