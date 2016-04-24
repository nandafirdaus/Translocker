using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Translocker.Utils
{
	public class IOUtils
	{
		#if __IOS__
		string resourcePrefix = "Translocker.iOS.";
		#endif
		#if __ANDROID__
		string resourcePrefix = "Translocker.Droid.";
		#endif
		#if WINDOWS_PHONE
		string resourcePrefix = "Translocker.WinPhone.";
		#endif

		public IOUtils ()
		{
			
		}

		public string readFileFromSharedProject(string fileName) {
			// note that the prefix includes the trailing period '.' that is required
			var assembly = typeof(IOUtils).GetTypeInfo().Assembly;
			Stream stream = assembly.GetManifestResourceStream
				(resourcePrefix + fileName);

			string text = "";
			using (var reader = new System.IO.StreamReader (stream)) {
				text = reader.ReadToEnd ();
			}

			return text;
		}
	}
}

