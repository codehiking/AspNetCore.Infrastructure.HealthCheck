namespace AspNetCore.Infrastructure.HealthCheck {
    /// <summary>
    /// Default health enumeration.
    /// </summary>
    public enum Health {
        Up,
        Initializing,
        Down
    }
}