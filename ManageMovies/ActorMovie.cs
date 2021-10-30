using System;
using System.Collections.Generic;

namespace ManageMovies
{
    public partial class ActorMovie
    {
        public int Id { get; set; }
        public int MovieSerial { get; set; }

        public virtual Actor IdNavigation { get; set; }
        public virtual Movie MovieSerialNavigation { get; set; }
    }
}
