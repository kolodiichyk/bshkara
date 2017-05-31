using System.Globalization;
using System.Threading.Tasks;

namespace Bshkara.Mobile.Localization
{
	public interface ILocalize
	{
		CultureInfo GetCurrentCultureInfo ();

		Task<CultureInfo> SetLocale();

		bool IsRightToLeft { get; }
	}
}

