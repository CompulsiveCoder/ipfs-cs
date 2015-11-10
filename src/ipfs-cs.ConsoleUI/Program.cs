using System;
using ipfsecho.Core;

namespace ipfsecho.ConsoleUI
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var arguments = new Arguments (args);

			var text = arguments.KeylessArguments [0];

			var hashOnly = arguments.Contains ("h")
				|| arguments.Contains("hash");

			var showPath = arguments.Contains ("p")
				|| arguments.Contains("path");

			var longOutput = arguments.Contains ("l")
				|| arguments.Contains("long");

			var showLink = arguments.Contains ("link");

			if (longOutput)
			{
				if (showLink == false)
					showLink = true;
				if (showPath == false)
					showPath = true;
			}

			/*var echo = new ipfsEcho ();

			var hash = echo.Echo (text);


			if (hashOnly) {
				Console.WriteLine (hash);
			} else {
				Console.WriteLine ("Hash: " + hash);
			}

			if (showLink) {
				var link = "URL: https://ipfs.io/ipfs/" + hash;
				Console.WriteLine (link);
			}

			if (showPath) {
				var path = "Path: /ipfs/" + hash;
				Console.WriteLine (path);
			}*/
		}
	}
}
