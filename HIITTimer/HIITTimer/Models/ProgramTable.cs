using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace HIITTimer
{ 
    [Table("ProgramTable")]
    public class ProgramTable
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string ProgramName { get; set; }
        public int Repeats { get; set;  }
        public int DisplayOrder { get; set; }
    }
}
