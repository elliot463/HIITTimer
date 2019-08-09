using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SQLite;

namespace HIITTimer
{ 
    [Table("IntervalTable")]
    public class IntervalTable
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int ProgramID { get; set; }
        public string IntervalName { get; set; }
        public int IntervalLength { get; set; }
        public int IntervalOrder { get; set; }
    }
}
