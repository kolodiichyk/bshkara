namespace Bshkara.Core.Base
{
    /// <summary>
    /// Localized class
    /// </summary>
    public interface ILocalizedProperty
    {
        /// <summary>
        /// English
        /// </summary>
        string En { get; set; }

        /// <summary>
        /// Arabic
        /// </summary>
        string Ar { get; set; }

        /// <summary>
        /// <see cref="Default"/> language
        /// </summary>
        string Default { get; }
    }
}