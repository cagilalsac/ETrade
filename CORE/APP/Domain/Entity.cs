namespace CORE.APP.Domain
{
    /// <summary>
    /// Abstract base class for all entities.
    /// </summary>
    public abstract class Entity
    {
        // Way 1:
        // Field to store the entity's ID.
        //private int id; // variable, field

        // Method to get the entity's ID.
        //public int GetId() // function, method, behavior
        //{
        //    return id;
        //}

        // Method to set the entity's ID.
        //public void SetId(int id)
        //{
        //    this.id = id;
        //}



        // Way 2:
        /// <summary>
        /// Gets or sets the ID of the entity.
        /// </summary>
        public int Id { get; set; } // Property to get or set the entity's ID.
    }
}
