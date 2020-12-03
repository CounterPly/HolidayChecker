using System;
using System.Collections;
using System.Data;
using OutSystems.HubEdition.RuntimePlatform;

namespace OutSystems.NssHolidayChecker {

	public interface IssHolidayChecker {

		/// <summary>
		/// C#/.NET Extension method to test whether or not a given business day is an observed US Holiday.
		/// 
		/// Rule-based so it works for any year.
		/// </summary>
		/// <param name="ssdateToCheck"></param>
		/// <param name="ssresult"></param>
		void MssIsHoliday(DateTime ssdateToCheck, bool ssresult);

	} // IssHolidayChecker

} // OutSystems.NssHolidayChecker
