using System.Collections.Generic;

namespace PicnicOrm.Dapper.Demo.Models
{
    /// <summary>
    /// </summary>
    public class Car
    {
        #region Constructors

        /// <summary>
        /// </summary>
        public Car()
        {
            Users = new List<User>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// </summary>
        public MakeModel MakeModel { get; set; }

        /// <summary>
        /// </summary>
        public IList<User> Users { get; set; }

        /// <summary>
        /// </summary>
        public int Year { get; set; }

        #endregion
    }
}