namespace EmployeeModels
{
    /// <summary>
    /// Represents the database connection settings.
    /// </summary>
    public class DBSettings
    {
        /// <summary>
        /// Gets or sets the database server address.
        /// </summary>
        public string Server { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the port number for the database server.
        /// </summary>
        public string Port { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the database.
        /// </summary>
        public string Database { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the username for database authentication.
        /// </summary>
        public string User { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password for database authentication.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the pool size for database connection.
        /// </summary>
        public string PoolSize { get; set; } = string.Empty;
    }
}
