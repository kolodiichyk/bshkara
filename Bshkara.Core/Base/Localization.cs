using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bshkara.Core.Helper;

namespace Bshkara.Core.Base
{
    /// <summary>
    /// Localized field
    /// </summary>
    [ComplexType]
    public class LocalizedName : ILocalizedProperty
    {
        /// <summary>
        /// Localized constructor
        /// </summary>
        public LocalizedName()
        {
        }

        /// <summary>
        /// Localized constructor
        /// </summary>
        public LocalizedName(string en, string ar)
        {
            En = en;
            Ar = ar;
        }

        /// <summary>
        /// English
        /// </summary>
        public string En { get; set; }

        /// <summary>
        /// Arabic
        /// </summary>
        public string Ar { get; set; }

        /// <summary>
        /// <see cref="Bshkara.Core.Base.LocalizedName.Default" /> language
        /// </summary>
        [ScaffoldColumn(false)]
        public string Default => LocalizedHelper.GetDefault(this);

        public override string ToString()
        {
            return Default;
        }
    }
}