using System;
using System.Collections.Generic;

namespace Zipper.Core.Models
{
    public class Token
    {
        public int Id { get; set; }
        public IList<int> OccurenceIndexes { get; set; }

        public IList<Tuple<int, int>> OccurenceRanges { get; set; } = new List<Tuple<int, int>>();
        public int Score { get; set; }

        public bool ToBeDeleted { get; set; }
    }
}